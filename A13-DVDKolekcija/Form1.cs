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
    public partial class Form1 : Form
    {

      
        // stavljeno kao public jer se koristi iz drugih formi, uniformo za povezivanje sa DBMS i db konekcija
        public string connectionString = "Data Source=localhost;Initial Catalog=DVD_Kolekcija;Integrated Security=True";
        public static SqlConnection databaseConnection;

         
        // ova funkcija služi za inicijalno učitavanje podataka u listbox, i kasnije za osvežavanje istog
        private void OsveziBox()
        {
            // upit za dohvatanje tripleta kolona iz tabele Producent, sortirano po imenu
            // ostatak je šablon za konekcioni pristup bazi
            string query = "SELECT ProducentID, Ime, Email FROM Producent ORDER BY Ime;";
            SqlCommand commandDatabase = new SqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            SqlDataReader reader;

            
            try
            {
                
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                  
                        string[] row = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString() };
                        listBox1.Items.Add(OgradiStr(row[0], 6) + " " + OgradiStr(row[1], 30) + " " + OgradiStr(row[2], 25) + " ");
                    }
                }
                PopuniNajmanjim(); // inicijalno popunjavanje texboxova

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

        private void PopuniNajmanjim ()
        {
            int min = Convert.ToInt32 (listBox1.Items[0].ToString().Substring(0,6));
            int i, minpoz=0; // inicijalna pozicija minimalnog ID-ja, tj, pocetak liste
            for (i=1; i<listBox1.Items.Count;i++)
            {
                int tekuci = Convert.ToInt32(listBox1.Items[i].ToString().Substring(0, 6));
                if (tekuci < min) { min = tekuci; minpoz = i; }
            }
            listBox1.SetSelected(minpoz, true);
            string x = listBox1.SelectedItem.ToString();
            PopuniTBoxove(x);

        }


        // secka se na tri dela, fiksan format, i pakuje u textboxove - uz "trimovanje"
        // trim je odsecanje suvišnih razmaka pre i posle stringa
        private void PopuniTBoxove(string x)
        {
            textBox1.Text = x.Substring(0, 6).Trim();
            textBox2.Text = x.Substring(7, 30).Trim();
            textBox3.Text = x.Substring(38, 25).Trim();
        }

        // aktivira se prilikom reagovanja na pritisak dugmeta, isto kao i malopre - samo update
        // string upita se efikasno formira zbrajanjem više linija koda, zbog preglednosti
        // a to je takođe i efikasan način da se umetnu podaci iz komponenata
        private void IzmenaBaze() {
            
            string query = "UPDATE Producent SET Ime = '" + textBox2.Text 
                        + "',Email = '" + textBox3.Text +"' WHERE ProducentID = "+textBox1.Text+";";
            
            SqlCommand commandDatabase = new SqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
             
            try
            {

                databaseConnection.Open();
                commandDatabase.ExecuteNonQuery();

                databaseConnection.Close();
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

        public Form1()
        {
            InitializeComponent();
        }

        // pomoćna funkcija koja služi da odseče suvišan tekst iz stringa
        // bitna je zbog seckanja stringova kako bi se upakovali u listbox po zahtevima zadatka
        public string OgradiStr(string x, int duzina)
        {
            int duz = x.Length;
            if (duz < duzina) return x.PadRight(duzina);
            else return x.Substring(0, duzina);

        }

        // klikom na dugme vrši se izmena baze (pogledaj gore), briše listbox, i osvežava se listbox
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            IzmenaBaze();
            listBox1.Items.Clear();
            OsveziBox();
        }

        // obrada događaja - kada se promeni selektovani red u listboxu na klik
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // pokupi se red iz listboxa i smešta u x
            string x = listBox1.SelectedItem.ToString();

            PopuniTBoxove(x);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Priprema konekcije odmah po učivatanju aplikacije
            databaseConnection = new SqlConnection(connectionString);
            // inicijalno se očitavaju podaci i puni listbox
            OsveziBox();
            
        }

        // otvaranje nove forme klikom na 2. dugme levo, komponenta je ToolStrip!
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        // 4. dugme levo je prosto zatvaranje aplikacije
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 3. dugme otvara 3. formu
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }
    }
}
