using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

// combo box je u projektu uklonjen, zamenjen je tex boxom jer je bizarna uloga combo-a. Nebitno

namespace A06_Automobili
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // MySQL
        // dobro je kao public kad se koristi iz drugih formi, uniformo za povezivanje sa DBMS i db konekcija
        public string connectionString = "Data Source=localhost;Initial Catalog=Polovni_Automobili;Integrated Security=True";
        public static SqlConnection databaseConnection;

        // biće potrebno za memorisanje odabrane šifre MODELA, i dohvatljivo iz bilo koje funkcije
        private string tekuca_sifra = String.Empty;

        // pomoćna funkcija koja služi da odseče suvišan tekst iz stringa
        // bitna je zbog seckanja stringova kako bi se upakovali u listbox po zahtevima zadatka
        public string OgradiStr(string x, int duzina)
        {
            int duz = x.Length;
            if (duz < duzina) return x.PadRight(duzina);
            else return x.Substring(0, duzina);

        }

        // ova funkcija služi za inicijalno učitavanje podataka u listbox, i kasnije za osvežavanje istog
        private void OsveziBox()
        {
            // konekcioni pristup, šablon, prostudirati SQL upit prema uslovima zadatka... radi!
            string query = "SELECT m.ModelID, m.Naziv, p.Naziv FROM Model m, Proizvodjac p " +
                "WHERE m.ProizvodjacID = p.ProizvodjacID ORDER BY m.Naziv;";
            SqlCommand commandDatabase = new SqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            SqlDataReader reader;

            listBox1.Items.Clear();  // čisti se sadržaj listboxa

            try
            {

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // uobičajeno punjenje redova listboksa, uz formiranje stringova sa razmacima 
                        // i prvom sekcijom fiksiranom na 6 karaktera (za ID modela)!
                        string[] row = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString() };
                        listBox1.Items.Add(OgradiStr(row[0], 6) + " " + row[1]+", " + row[2]);
                    }
                }
                else
                {
                    MessageBox.Show("Nema rezultata");
                }


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

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectedText = Properties.Resources.HelpDocs;
            // Priprema konekcije
            databaseConnection = new SqlConnection(connectionString);
            OsveziBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // da li u listboksu postoji string sa zadatom šifrom...

            int i, pos=-1, sifra=-1;
            Int32.TryParse(textBox2.Text, out sifra);  // izvlači se broj iz boksa, nula ako ne može
            
            for (i = 0; i < listBox1.Items.Count; i++)  //prolaz kroz listboks, poredi se sa prvom kolonom
                if (sifra == Convert.ToInt32(listBox1.Items[i].ToString().Substring(0, 6)) )
                    pos = i;
            if (pos < 0)  // nije nadjena odgovarajuća šifra u listboksu
            {
                listBox1.ClearSelected();   //ukloni selekciju
                                            //comboBox1.Items.Clear();
                textBox3.Clear(); // pobriši boksove
                textBox1.Clear();
                tekuca_sifra = String.Empty; // izbriši tekuću šifru
            }
            else   // nađena je šifra u boksu
            {
                listBox1.SetSelected(pos, true);
                string autom = listBox1.Items[listBox1.SelectedIndex].ToString();

                tekuca_sifra = autom.Substring(0, 6);

                autom = autom.Substring(7);

                string[] aut = autom.Split(','); // deli string na dva dela, odvojenih razmakom
                                                 // ------- ODUSTAO OD COMBO BOX-a, koristimo normalan tbox br. 3
                                                 // tb3 je za naziv firme, a tb1 je za model - tb 2 je desno i u njega se unosi šifra
                                                 //comboBox1.Items.Clear();
                textBox3.Clear();
                textBox1.Clear();
                aut[1] = aut[1].TrimStart(' '); //sklanja uvodne razmake u stringu
                                                //comboBox1.Items.Add(aut[1]);
                                                //comboBox1.SelectedIndex = 0;
                textBox3.Text = aut[1];
                textBox1.Text = aut[0];

            }
      
        }
        private void IzmenaBaze()
        {

            string strProizSif = String.Empty;

            string query1 = "SELECT p.ProizvodjacID FROM Model m, Proizvodjac p WHERE " +
                "m.ModelID = " + tekuca_sifra + " AND m.ProizvodjacID = p.ProizvodjacID;";

            string query2 = "UPDATE Model SET Naziv = '"+textBox1.Text+"' WHERE ModelID = '"+ tekuca_sifra + "';";
            

            SqlCommand comdb1 = new SqlCommand(query1, databaseConnection);
            SqlCommand comdb2 = new SqlCommand(query2, databaseConnection);
               
            SqlDataReader reader;

             

            try
            {
                databaseConnection.Open(); reader = comdb1.ExecuteReader();
                reader.Read(); strProizSif = reader[0].ToString();
                reader.Close(); //databaseConnection.Close();

                //databaseConnection.Open();
                comdb2.ExecuteNonQuery();
                //databaseConnection.Close();

                string query3 = "UPDATE Proizvodjac SET Naziv = '" + textBox3.Text +
                     "' WHERE ProizvodjacID = '" + strProizSif + "';"; //korisceno - comboBox1.Items[0].ToString()
                SqlCommand comdb3 = new SqlCommand(query3, databaseConnection);
                comdb3.ExecuteNonQuery();
                databaseConnection.Close();

                OsveziBox();
                int index = listBox1.FindString(tekuca_sifra.ToString());
                if (index != -1) listBox1.SetSelected(index, true);

            }

            catch (Exception ex)
            {
                MessageBox.Show("Neuspešna operacija!");
            }
            finally
            {
                databaseConnection.Close();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            IzmenaBaze();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string god_od = numericUpDown1.Value.ToString();
            string god_do = numericUpDown2.Value.ToString();
            string km = textBox4.Text;

            string query = "SELECT p.Naziv, count(v.VoziloID) FROM Proizvodjac p, Vozilo v, Model m " +
                "WHERE v.ModelID = m.ModelID AND m.ProizvodjacID = p.ProizvodjacID " +
                "AND v.GodinaProizvodnje BETWEEN " + god_od + " AND " + god_do +
                " AND v.PredjenoKM < " + km + " GROUP BY p.Naziv ORDER BY p.Naziv ASC;";

            SqlCommand comm = new SqlCommand(query, databaseConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);

            DataSet dsAutomobili = new DataSet();


            try
            {

                // ovo je sve ručno postavljeno, "Automobili" su proizvoljan naziv tabele koji se kasnije koristi
                databaseConnection.Open();
                adapter.Fill(dsAutomobili, "Automobili");
                dataGridView1.DataSource = dsAutomobili.Tables["Automobili"].DefaultView;
                dataGridView1.Columns[0].Width = 160;
                dataGridView1.Columns[1].Width = 180;
                dataGridView1.Columns[0].HeaderText = "Proizvodjac";
                dataGridView1.Columns[1].HeaderText = "Broj";
        
                // za chart je u Properties postavljen SERIES na Automobili i tamo se podešava tip charta
                // i ponešto još od šminkanja, a ovde u kodu se naznačava sadržaj X i Y ose na osnovu DS
                chart1.Titles.Clear();
                chart1.Titles.Add("Automobili"); 
                chart1.DataSource = dsAutomobili;
                chart1.Series["Automobili"].XValueMember = dsAutomobili.Tables[0].Columns[0].ColumnName;
                chart1.Series["Automobili"].YValueMembers = dsAutomobili.Tables[0].Columns[1].ColumnName;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Loše su uneti podaci!");
            }
            finally
            {
                databaseConnection.Close();  // dbc promenljiva je definisana u Form1, public
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
