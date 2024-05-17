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

namespace golf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string connectionString = "Data Source=localhost;Initial Catalog=Golf;Integrated Security=True";
        public static SqlConnection databaseConnection;
        private void Form1_Load(object sender, EventArgs e)
        {
            // Priprema konekcije odmah po učivatanju aplikacije
            databaseConnection = new SqlConnection(connectionString);
            // inicijalno se očitavaju podaci i puni listbox
            OsveziBox();
        }

        public string OgradiStr(string x, int duzina)
        {
            int duz = x.Length;
            if (duz < duzina) return x.PadRight(duzina);
            else return x.Substring(0, duzina);

        }

        // ova funkcija služi za inicijalno učitavanje podataka u listbox, i kasnije za osvežavanje istog
        private void OsveziBox()
        {

            // test baza ima u sadrzaju dva puta upisan Apatin u tabeli Grad!
            string query = "SELECT t.TerenID, t.Teren, t.Adresa, t.KontaktTelefon, g.Grad " +
                " FROM Teren t, Grad g WHERE t.GradID = g.GradID ORDER BY TerenID;";
            string query2 = "SELECT Grad FROM Grad ORDER BY Grad ASC;";
            SqlCommand commandDatabase = new SqlCommand(query, databaseConnection);
            SqlCommand commandDatabase2 = new SqlCommand(query2, databaseConnection);

            commandDatabase.CommandTimeout = 60; commandDatabase2.CommandTimeout = 60;

            SqlDataReader reader;

            listBox1.Items.Clear();

            try
            {

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        string[] row = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),
                               reader[3].ToString(),reader[4].ToString() };
                        listBox1.Items.Add(OgradiStr(row[0], 5) + " " + OgradiStr(row[1], 33) + " " +
                               OgradiStr(row[2], 20) + " " + OgradiStr(row[3], 14) + " " + OgradiStr(row[4], 10) );
                    }
                }

                reader.Close();
                reader = commandDatabase2.ExecuteReader();
                
                // inicijalno punjenje terena u combobox
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


            // secka se na 5 delova, fiksan format, i pakuje u textboxove - uz "trimovanje"
            // trim je odsecanje suvišnih razmaka pre i posle stringa
 
            textBox4.Text = x.Substring(0, 5).Trim();  // umesto ComboBoxa
            textBox1.Text = x.Substring(6, 33).Trim();
            textBox2.Text = x.Substring(40, 20).Trim();
            textBox3.Text = x.Substring(61, 14).Trim();
            comboBox1.Text = " ";
            comboBox1.SelectedText = x.Substring(76, 10).Trim();
             
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // pokupi se red iz listboxa i smešta u x
            string x = listBox1.SelectedItem.ToString();
            PopuniTBoxove(x);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }
        private Boolean LocirajZapisPoSifri()
        {
                string x = textBox4.Text.ToString().Trim();
                int ind = -1;
                for (int i = 0; i < listBox1.Items.Count; i++)
                    if (x.Equals(listBox1.Items[i].ToString().Substring(0, 5).Trim()))
                    { ind = i; break; }

                if (ind > -1)
                {
                    listBox1.SetSelected(ind, true);
                    x = listBox1.SelectedItem.ToString();
                    PopuniTBoxove(x);
                    return true;
                }
                else MessageBox.Show("Nema podataka za teren sa unetom šifrom!");
                return false;
        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LocirajZapisPoSifri();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            string query = "DELETE FROM Teren WHERE TerenID="+ textBox4.Text + ";";
            SqlCommand cmd = new SqlCommand(query, databaseConnection);
            cmd.CommandTimeout = 60;

            if (!LocirajZapisPoSifri()) return;

            DialogResult res = MessageBox.Show("Da li ste sigurni?", "Želite da obrišete tekući zapis!", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
            if (res == DialogResult.Cancel) return;
 
            try {
                databaseConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                databaseConnection.Close();
                if (rowsAffected == 1)
                {
                    OsveziBox();
                    MessageBox.Show("Uspešno obrisan zapis!");
                    
                }
                else MessageBox.Show("GREŠKA! Zapis nije obrisan!");
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
    }
}
