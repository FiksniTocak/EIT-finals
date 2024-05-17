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

namespace BibliotekaA02
{
    public partial class Form2 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=BibliotekaA02;Integrated Security=true");
        SqlCommand cmd = new SqlCommand();
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                try
                {
                    cmd.Connection = conn;
                    string s = comboBox1.Text;
                    var delovi = s.Split(' ');
                    string ime = delovi[0];
                    string prezime = delovi[1];
                    int g1 = (int)numericUpDown1.Value;
                    int godina = 2023 - g1;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT YEAR(NaCitanju.DatumUzimanja) as Godina, COUNT(NaCitanju.KnjigaID) as Broj" +
                                    " from NaCitanju INNER JOIN Knjiga on NaCitanju.KnjigaID=Knjiga.KnjigaID INNER JOIN Napisali" +
                                    " ON Knjiga.KnjigaID=Napisali.KnjigaID INNER JOIN Autor ON Napisali.AutorID=Autor.AutorID" +
                                    " where (Autor.Prezime=@p) AND (Autor.Ime=@i) AND YEAR(NaCitanju.DatumUzimanja) BETWEEN @god and 2023" +
                                    " GROUP BY YEAR (NaCitanju.DatumUzimanja)";

                    conn.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@i", ime);
                    cmd.Parameters.AddWithValue("@p", prezime);
                    cmd.Parameters.AddWithValue("@god", godina);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    conn.Close();
                    chart1.Series["Prikaz"].Points.Clear();
                    foreach (DataRow item in dt.Rows) {
                       // MessageBox.Show(item[0].ToString(), item[1].ToString());
                        chart1.Series["Prikaz"].Points.AddXY(item[0].ToString(), item[1].ToString());
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                    conn.Close();
                }
            }

            else {
                MessageBox.Show("Izaberite citaoca");
            }
            }

            /*
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandText = "select {fn concat(dbo.Citalac.Ime,dbo.Citalac.Prezime)} as Citalac, YEAR (dbo.Na_Citanju.DatumIzdavanja) AS Godina, COUNT (dbo.Na_Citanju.DatumIzdavanja)" +
                    " AS BrojIznajmljenih, COUNT (dbo.Na_Citanju.DatumVracanja) AS BrojVracenih" +
                    " from dbo.Citalac INNER JOIN dbo.Na_Citanju ON dbo.Na_Citanju.CitalacID=dbo.Citalac.CitalacID " +
                    " where (dbo.Citalac.Ime=@ime) AND (dbo.Citalac.Prezime=@prezime) AND (YEAR(dbo.Na_Citanju.DatumIzdavanja) between @g1 and @g2)" +
                    " group by {fn concat(dbo.Citalac.Ime,dbo.Citalac.Prezime)}, YEAR (dbo.Na_Citanju.DatumIzdavanja)";
                conn.Open();
                cmd.Parameters.AddWithValue("@ime", ime);
                cmd.Parameters.AddWithValue("@prezime", prezime);
                cmd.Parameters.AddWithValue("@g1", g1);
                cmd.Parameters.AddWithValue("@g2", g2);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
                chart1.Series["BrojIznajmljenih"].Points.Clear();
                chart1.Series["BrojVracenih"].Points.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    chart1.Series["BrojIznajmljenih"].Points.AddXY(item[1].ToString(), item[2].ToString());
                    chart1.Series["BrojVracenih"].Points.AddXY(item[1].ToString(), item[3].ToString());

                }
            }
        
            else
            {
                MessageBox.Show("Morate izabrati citaoca");
            }*/



 

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT distinct ime,prezime from Autor";
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string s = dr[0].ToString() + " " + dr[1].ToString() ;
                comboBox1.Items.Add(s);
            }
            conn.Close();
        }
    }
}
