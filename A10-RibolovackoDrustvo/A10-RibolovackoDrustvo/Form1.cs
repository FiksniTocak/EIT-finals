using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace A10_RibolovackoDrustvo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        public string connectionString = "Data Source = localhost; Initial Catalog = Ribolovacko_Drustvo; Integrated Security = True";
        public static SqlConnection databaseConnection;

        public string OgradiStr(string x, int duzina)
        {
            int duz = x.Length;
            if (duz < duzina) return x.PadRight(duzina);
            else return x.Substring(0, duzina);

        }
        private void OsveziBox()
        {
 
            // test baza ima u sadrzaju dva puta upisan Apatin u tabeli Grad!
            string query = "SELECT p.PecarosID, p.Ime, p.Prezime, p.Adresa, g.Grad, p.Telefon "+ 
                " FROM Pecaros p, Grad g WHERE p.GradID = g.GradID ORDER BY PecarosID;";
            string query2 = "SELECT Grad FROM Grad ORDER BY Grad;";
            SqlCommand commandDatabase = new SqlCommand(query, databaseConnection);
            SqlCommand commandDatabase2 = new SqlCommand(query2, databaseConnection);

            commandDatabase.CommandTimeout = 60; commandDatabase2.CommandTimeout = 60;

            SqlDataReader reader;


            try
            {

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        string[] row = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                               reader[3].ToString(),reader[4].ToString(),reader[5].ToString() };
                        listBox1.Items.Add(OgradiStr(row[0], 6) + " " + OgradiStr(row[1], 14) + " " + 
                               OgradiStr(row[2], 14) + " " + OgradiStr(row[3], 25) + " " + OgradiStr(row[4], 14) + " " 
                               + OgradiStr(row[5], 18));
                    }
                }
              
                reader.Close();
                reader = commandDatabase2.ExecuteReader();
                while (reader.Read())
                    comboBox1.Items.Add(reader[0].ToString());

                string x = listBox1.Items[0].ToString();
                listBox1.SetSelected(0, true);
                PopuniTBoxove(x);   //inicijalno punjenje boxova sa prvim redom u listboxu

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                databaseConnection.Close();
            }
        }

        private void PopuniTBoxove(string x)
        {
            // pokupi se red iz listboxa i smešta u x
            

            // secka se na 6 delova, fiksan format, i pakuje u textboxove - uz "trimovanje"
            // trim je odsecanje suvišnih razmaka pre i posle stringa
            textBox1.Text = x.Substring(0, 6).Trim();
            textBox2.Text = x.Substring(7, 14).Trim();
            textBox3.Text = x.Substring(22, 14).Trim();
            textBox4.Text = x.Substring(37, 25).Trim();
            comboBox1.Text = " ";
            comboBox1.SelectedText = x.Substring(63, 14).Trim();
            textBox5.Text = x.Substring(78, 14).Trim();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Priprema konekcije odmah po učivatanju aplikacije
            databaseConnection = new SqlConnection(connectionString);
            // inicijalno se očitavaju podaci i puni listbox
            OsveziBox();
        }

        private void IzmenaBaze()
        {

            try { 

                 string gid;
                 string query = "SELECT GradID FROM Grad WHERE Grad = '" + comboBox1.Text + "';";
                 SqlCommand commandDatabase = new SqlCommand(query, databaseConnection);
                 SqlDataReader reader;
                 databaseConnection.Open();
                 reader = commandDatabase.ExecuteReader(); reader.Read();
                 gid = reader[0].ToString();
                reader.Close();

                 string query2 = "UPDATE Pecaros SET Ime = '" + textBox2.Text
                        + "',Prezime = '" + textBox3.Text
                        + "',Adresa = '" + textBox4.Text
                        + "',GradID = '" + gid +
                        "' WHERE PecarosID = " + textBox1.Text + ";";

                 SqlCommand commandDatabase2 = new SqlCommand(query2, databaseConnection);
 
                commandDatabase2.ExecuteNonQuery();

                MessageBox.Show("Uspešna izmena!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - Neuspešna izmena!");
            }
            finally
            {
                databaseConnection.Close();
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // pokupi se red iz listboxa i smešta u x
            string x = listBox1.SelectedItem.ToString();
            PopuniTBoxove(x);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            IzmenaBaze();
            listBox1.Items.Clear();
            comboBox1.Items.Clear();
            OsveziBox();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
