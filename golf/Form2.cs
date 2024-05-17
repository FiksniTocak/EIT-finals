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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dohvatanje broja sati i pretvaranje u minute
            int vreme1 = 60*Convert.ToInt32(numericUpDown1.Value);
            int vreme2 = 60*Convert.ToInt32(numericUpDown2.Value);

            //SQL upit koji uzima u obzir zahtevani opseg trajanja partije 
            string query = "SELECT p.PartijaID, t.Teren, p.Datum, DATEDIFF(MINUTE, p.VremePocetka, p.VremeZavrsetka)  FROM Partija p, Teren t " +
                        "WHERE p.TerenID = t.TerenID AND DATEDIFF(MINUTE, p.VremePocetka, p.VremeZavrsetka) BETWEEN " + 
                            vreme1.ToString() + " AND " + vreme2.ToString() + ";";
            SqlCommand comm = new SqlCommand(query, Form1.databaseConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);

            DataSet ds = new DataSet();
            try
            {

                Form1.databaseConnection.Open();
                adapter.Fill(ds, "Partije");
                dataGridView1.DataSource = ds.Tables["Partije"].DefaultView;
                dataGridView1.Columns[0].HeaderText = "PartijaID"; dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].HeaderText = "Teren"; dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Datum"; dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].HeaderText = "Trajanje"; dataGridView1.Columns[3].Width = 60;

                chart1.Titles.Clear();
                chart1.Titles.Add("");
                chart1.DataSource = ds;
                chart1.Series["Partije"].XValueMember = ds.Tables[0].Columns[1].ColumnName;
                chart1.Series["Partije"].YValueMembers = ds.Tables[0].Columns[3].ColumnName;


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

        private void Form2_Load(object sender, EventArgs e)
        {
            //inicijalno punjene datagridview sa svim podacima, uz određivanje trajanja u minutama
            string query = "SELECT p.PartijaID, t.Teren, p.Datum, DATEDIFF(MINUTE, p.VremePocetka, p.VremeZavrsetka) FROM Partija p, Teren t " +
                        "WHERE p.TerenID = t.TerenID;" ;
            SqlCommand comm = new SqlCommand(query, Form1.databaseConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);

            DataSet ds = new DataSet();
            try
            {

                Form1.databaseConnection.Open();
                adapter.Fill(ds, "Partije");
                dataGridView1.DataSource = ds.Tables["Partije"].DefaultView;
                dataGridView1.Columns[0].HeaderText = "PartijaID"; dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].HeaderText = "Teren"; dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Datum"; dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].HeaderText = "Trajanje"; dataGridView1.Columns[3].Width = 60;

                dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending);
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);


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
    }
}
