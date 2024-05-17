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
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=BibliotekaA02;Integrated Security=true");
        SqlCommand cmd = new SqlCommand();
        public Form1()
        {
            InitializeComponent();
        }
        private void ucitaj() {

            try
            {
                cmd.Connection = conn;
                listView1.Items.Clear();
                cmd.CommandText = "select*from Autor";
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem red = new ListViewItem(dr[0].ToString());
                    red.SubItems.Add(dr[1].ToString());
                    red.SubItems.Add(dr[2].ToString());
                    red.SubItems.Add(((DateTime)dr[3]).ToString("dd.MM.yyyy"));
                    listView1.Items.Add(red);
                   
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
      


        private void Form1_Load(object sender, EventArgs e)
        {
            ucitaj();
            //ispisPostojeceg();


        }
        private void resetuj() {
            txtSifra.Text ="";
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtRodjen.Text = "";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        int id = -1;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //list view skupi podatke pa ListView.SelectedItem udju pa textbox
            foreach (ListViewItem el in listView1.SelectedItems)
            {
                id = Convert.ToInt32(el.SubItems[0].Text);
                txtSifra.Text = el.SubItems[0].Text;
                txtIme.Text = el.SubItems[1].Text;
                txtPrezime.Text = el.SubItems[2].Text;
                txtRodjen.Text = el.SubItems[3].Text;

            }

        }
        private int proveri() {
            int i = 0;
            if (txtSifra.Text != "") {
                int sifra = Convert.ToInt32(txtSifra.Text);
                cmd.Connection = conn;
                cmd.CommandText = "select AutorID from Autor where AutorID=@sifra";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@sifra",sifra);
                conn.Open();
                i= (int)cmd.ExecuteScalar();
            }
            conn.Close();
            return i;
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form2 f1 = new Form2();
            this.Hide();
            f1.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (id != 0)
                {
                    if (proveri() != 0 && txtSifra.Text != "" && txtIme.Text != "" && txtPrezime.Text != "" && txtRodjen.Text != "")
                    {
                        cmd.Connection = conn;
                        MessageBox.Show(id.ToString());
                       
                        cmd.CommandText = "delete from Autor where AutorID= @i";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@i", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Uspesno obrisan autor");
                        resetuj();
                        conn.Close();
                        ucitaj();
                    }

                }
                else {

                    MessageBox.Show("Ne posotoji autor sa zadatim id-jem");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {

                conn.Close();
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }
    }
}
