using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proba_Baze_Forma
{
    public partial class Form2 : Form
    {        
        public Form2()
        {
            InitializeComponent();
        }

        // aktivacija dugmeta Prikaži, selektuje potrebne podatke (2 kolone) i puni dataGridView i Chart
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT p.ime, COUNT(r.FilmID) FROM producirao r, producent p"; 
            query += " WHERE p.ProducentID = r.ProducentID GROUP BY p.ime ORDER BY p.ime;";

            SqlCommand comm = new SqlCommand(query, Form1.databaseConnection);
            SqlDataAdapter adapter  = new SqlDataAdapter(comm);

            DataSet dsFilmovi = new DataSet();


            try
            {

                // ovo je sve ručno postavljeno, "Filmovi" su proizvoljan naziv tabele koji se kasnije koristi
                Form1.databaseConnection.Open();
                adapter.Fill(dsFilmovi, "Filmovi");
                dataGridView1.DataSource = dsFilmovi.Tables["Filmovi"].DefaultView;
                dataGridView1.Columns[0].HeaderText = "Producent";
                dataGridView1.Columns[1].HeaderText = "Broj filmova";

                // za chart je u Properties postavljen SERIES na Filmovi i tamo se podešava tip charta
                // i ponešto još od šminkanja, a ovde u kodu se naznačava sadržaj X i Y ose na osnovu DS
                chart1.Titles.Add("Filmovi"); 
                chart1.DataSource = dsFilmovi;
                chart1.Series["Filmovi"].XValueMember = dsFilmovi.Tables[0].Columns[0].ColumnName;
                chart1.Series["Filmovi"].YValueMembers = dsFilmovi.Tables[0].Columns[1].ColumnName;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Form1.databaseConnection.Close();  // dbc promenljiva je definisana u Form1, public
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
