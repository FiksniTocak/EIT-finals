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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void NapuniCombo()
        {


            string query = "SELECT PecarosID, Ime, Prezime FROM Pecaros ORDER BY PecarosID; ";
                     
            
            SqlCommand commandDatabase = new SqlCommand(query, Form1.databaseConnection);
            SqlDataReader reader;
            try
            {

                Form1.databaseConnection.Open();

                reader = commandDatabase.ExecuteReader();

                while (reader.Read())
                {
                    string[] row = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString() };
                    comboBox1.Items.Add(row[0] + " - " + row[1] + " " + row[2]);
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Form1.databaseConnection.Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            NapuniCombo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pecID = comboBox1.Text;
            pecID = pecID.Substring(0, pecID.IndexOf(" "));

            string dt1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dt2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            string query = "SELECT v.Naziv, COUNT(v.Naziv) AS broj FROM Ulov u, Vrsta_Ribe v" +
                " WHERE v.VrstaID = u.VrstaID AND u.PecarosID = " + pecID +
                " AND u.Datum BETWEEN '" + dt1 + "' AND '" + dt2 + 
                "' GROUP BY v.Naziv ORDER BY v.Naziv ASC;";
            SqlCommand comm = new SqlCommand(query, Form1.databaseConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);

            DataSet ds = new DataSet();
            try
            {
                
                Form1.databaseConnection.Open();
                adapter.Fill(ds, "Rezultat");
                dataGridView1.DataSource = ds.Tables["Rezultat"].DefaultView;
                dataGridView1.Columns[0].HeaderText = "Vrsta";
                dataGridView1.Columns[1].HeaderText = "Broj";
            

                chart1.Titles.Clear();
                chart1.Titles.Add("");  
                chart1.DataSource = ds;
                chart1.Series["Rezultat"].XValueMember = ds.Tables[0].Columns[0].ColumnName;
                chart1.Series["Rezultat"].YValueMembers = ds.Tables[0].Columns[1].ColumnName;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Form1.databaseConnection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
