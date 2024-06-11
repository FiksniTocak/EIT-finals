using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evidencijaradnika3
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source = Profesorski-R3; Initial Catalog = Radnici ; Integrated Security = True ");
        SqlCommand cmd = new SqlCommand();
        public Form1()
        {
            InitializeComponent();
        }
        private void ucitaj()
        {
            try
            {
                cmd.Connection = conn;
                listView1.Items.Clear();
                cmd.CommandText = "select * from Projekat";
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem red = new ListViewItem(dr[0].ToString());
                    red.SubItems.Add(dr[1].ToString());
                    red.SubItems.Add(((DateTime)dr[2]).ToString("dd.MM.yyyy"));
                    red.SubItems.Add(dr[3].ToString());
                    red.SubItems.Add(dr[4].ToString());
                    red.SubItems.Add(dr[5].ToString());
                    listView1.Items.Add(red);
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem el in listView1.SelectedItems)
            {
                txtSifra.Text = el.SubItems[0].Text;
                txtNaziv.Text = el.SubItems[1].Text;
                txtPocetak.Text = el.SubItems[2].Text;
                txtBudzet.Text = el.SubItems[3].Text;
                if (el.SubItems[4].Text == "True")
                {
                    cbZavrsen.Checked = true;
                }
                else
                {
                    cbZavrsen.Checked = false;
                }
                rtOpis.Text = el.SubItems[5].Text;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ucitaj();
        }
        private int proveri()
        {
            int i = 0;
            var god = txtPocetak.Text.Split('.');
            int godina = int.Parse(god[2]);
            string zavrsen = "";
            if (cbZavrsen.Checked)
            {
                zavrsen = "True";
            }
            else
            {
                zavrsen = "False";
            }
            if(2023 - godina < 5 || zavrsen == "False")
            {
                MessageBox.Show("Nije moguce brisanje projekta!");
                i = -1;
            }
            else
            {
                i = 0;
            }
            return i;
        }
        private void resetuj()
        {
            txtSifra.Clear();
            txtNaziv.Clear();
            txtPocetak.Clear();
            txtBudzet.Clear();
            rtOpis.Clear();
            cbZavrsen.Checked = false;
        }
        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if(txtSifra.Text != "" && txtNaziv.Text != "" && txtPocetak.Text != ""
                && txtBudzet.Text != "" && rtOpis.Text != "")
            {
                int sifra = int.Parse(txtSifra.Text);
                if (proveri() == 0)
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "delete from Projekat where ProjekatID=@sifra";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@sifra", sifra);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Uspesno obrisan projekat!");

                        string putanja = @"C:\Users\admin\Desktop\" + "log_" + 
                            (DateTime.Now.ToString("dd_MM_yyyy") + ".txt").ToString();
                        string sadrzaj = sifra + " " + txtNaziv.Text + "\n";
                        File.AppendAllText(putanja, sadrzaj);

                        ucitaj();
                        resetuj();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Nije moguce brisanje projekta!");
                }
                
            }
            else
            {
                MessageBox.Show("Nisu popunjena sva polja!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
        
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }
}
