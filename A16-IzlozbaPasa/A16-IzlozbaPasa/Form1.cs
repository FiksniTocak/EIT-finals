using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace A16_IzlozbaPasa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // projekat ne koristi frejmove, već tabControl - nekoliko tabova. Zato je sve u jednom fajlu

        //Koriste se globalno na nivou projekta
        private string connectionString = @"Data Source=localhost;Initial Catalog=IzlozbaPasa3;Integrated Security=True;";
        private static SqlConnection databaseConnection;

        // očitavanje baze, i inicijalno punjenje comboboxova
        private void OcitajBazu()
        {
            databaseConnection = new SqlConnection(connectionString);

            // tri upita, za svaki combo po jedan, prema zahtevu zadatka

            string query1 = "SELECT PasID, Ime  FROM Pas ORDER BY PasID ASC;";
            string query2 = "SELECT IzlozbaID, Mesto, Datum FROM Izlozba ORDER BY IzlozbaID ASC;";
            string query3 = "SELECT KategorijaID, Naziv FROM Kategorija ORDER BY KategorijaID ASC;";

            SqlCommand commandDatabase1 = new SqlCommand(query1, databaseConnection);
            SqlCommand commandDatabase2 = new SqlCommand(query2, databaseConnection);
            SqlCommand commandDatabase3 = new SqlCommand(query3, databaseConnection);

            SqlDataReader reader;

            //popunjavanje combobox-eva iz baze, tri odvojena upita definisana malopre
            try
            {

                databaseConnection.Open();

                reader = commandDatabase1.ExecuteReader();

                while (reader.Read())
                {
                    string[] row = { reader[0].ToString(), reader[1].ToString() };
                    comboBox1.Items.Add(row[0] + " - " + row[1]); //odvajaju se informacije u redu
                    comboBox1.SelectedIndex = 0;
                }

                reader.Close();
                reader = commandDatabase2.ExecuteReader();   // koristi se isti Reader

                //// reciklira se prethodni kod za 2. komandu
                while (reader.Read())
                {
                    string[] row = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString() };
                    comboBox2.Items.Add(row[0] + " - " + row[1] + " - " + row[2]);
                    comboBox2.SelectedIndex = 0;
                }
                reader.Close(); reader = commandDatabase3.ExecuteReader();

                //... i još jednom, ali za 3. komandu
                while (reader.Read())
                {
                    string[] row = { reader[0].ToString(), reader[1].ToString() };
                    comboBox3.Items.Add(row[0] + " - " + row[1]);
                    comboBox3.SelectedIndex = 0;
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
            OcitajBazu();   // aktivira popunjavanje combobox-ova pri startu aplikacije
            PostaviTab2();  // popuni info na drugom tabu

            richTextBox1.SelectAll();
            richTextBox1.SelectedText = Properties.Resources.HelpDocs;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // iz combobox-ova se izdvajaju selektovane vrednosti, prvi deo informacije
            string strPasID = comboBox1.Text;
            //podstring, od nulte pozicije do prvog pojavljivanja razmaka!! Bitan mehanizam!
            //izdvaja ID psa iz comboBox1 stringa, isto vazi i u sledećim redovima
            strPasID = strPasID.Substring(0, strPasID.IndexOf(" "));
            string strIzlozbaID = comboBox2.Text;
            strIzlozbaID = strIzlozbaID.Substring(0, strIzlozbaID.IndexOf(" "));
            string strKategorijaID = comboBox3.Text;
            strKategorijaID = strKategorijaID.Substring(0, strKategorijaID.IndexOf(" "));

            // proverava se da li već postoji red u tabeli Prijava
            string query1 = "SELECT PasID FROM Prijava WHERE PasID = " + strPasID
                        + " AND IzlozbaID = '" + strIzlozbaID + "';";
            string query2 = "INSERT INTO Prijava ( PasID, IzlozbaID, KategorijaID ) VALUES ("
                    + strPasID + ", '" + strIzlozbaID + "', " + strKategorijaID + ");";

            SqlCommand commandDatabase1 = new SqlCommand(query1, databaseConnection);
            SqlCommand commandDatabase2 = new SqlCommand(query2, databaseConnection);

            SqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase1.ExecuteReader();
                if (!reader.HasRows)  // pod uslovom da nema već unetog psa + izložbe !!
                // (if je OK ako prvi selekt ne daje ni jedan jedini pasID, tj. nije ranije unošen taj pas
                {
                    reader.Close();
                    commandDatabase2.ExecuteNonQuery();  // može unos !
                    MessageBox.Show("Uspešan unos!");
                }
                else
                    MessageBox.Show("Neuspešan unos!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Neuspešna izmena!");
            }
            finally
            {
                databaseConnection.Close();
            }
        }

        private void PostaviTab2()
        {
            // popunjavanje comboboxa na drugom tabu - u vezi Izlozbi. Mora da postoji izlozba u tabeli Rezultat!

            string query1 = "SELECT I.IzlozbaID, Mesto, Datum FROM Izlozba I WHERE " +
                    "EXISTS ( SELECT R.IzlozbaID FROM Rezultat R WHERE I.IzlozbaID = R.IzlozbaID) " +
                    "ORDER BY IzlozbaID ASC;";

            // prebrojavaju se psi u Rezultat tabeli, izgleda da ovo nisam iskoristio
            //string query2 = "SELECT COUNT(PasID) FROM Rezultat;";

            // standardni konekcioni pristup
            SqlCommand commandDatabase1 = new SqlCommand(query1, databaseConnection);
            SqlDataReader reader;
            try
            {

                databaseConnection.Open();

                reader = commandDatabase1.ExecuteReader();

                // popunjava se kombo box na 2. tabu, isti mehanizam kao na prethodnom tabu
                while (reader.Read())
                {
                    string[] row = { reader[0].ToString(), reader[1].ToString(), reader[2].ToString() };
                    comboBox4.Items.Add(row[0] + " - " + row[1] + " - " + row[2]);
                    comboBox4.SelectedIndex = 0;
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {


            //----------------------------------------------------

            // popunjavanje DesignView i Chart komponentu! Koristimo ds i adapter, beskonekcioni pristup.

            // prvo izvlačimo šifru iz combobox4!
            string strIzlozbaID = comboBox4.Text;
            strIzlozbaID = strIzlozbaID.Substring(0, strIzlozbaID.IndexOf(" "));


            string q = "SELECT COUNT(DISTINCT PasID) FROM Prijava WHERE IzlozbaID = '" + strIzlozbaID + "';";

            SqlCommand cm = new SqlCommand(q, databaseConnection);
            SqlDataReader reader; databaseConnection.Open();
            reader = cm.ExecuteReader(); reader.Read();
            label9.Text = reader[0].ToString(); reader.Close(); databaseConnection.Close();

            // o ovome valja razmišljati... za analizu je, i radi - što je najbitnije.
            string query = "SELECT k.KategorijaID, k.Naziv, COUNT(r.PasID) AS br_pasa FROM rezultat r, kategorija k " +
                "WHERE r.IzlozbaID = '" + strIzlozbaID + "' AND r.kategorijaID = k.kategorijaID " +
                "GROUP BY k.KategorijaID, k.Naziv ORDER BY k.KategorijaID, k.Naziv ASC; ";
            SqlCommand comm = new SqlCommand(query, databaseConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);

            DataSet ds = new DataSet();

            try
            {
                // ovo je sve ručno postavljeno, "Takmicenje" su proizvoljan naziv tabele koji se kasnije koristi
                databaseConnection.Open();
                adapter.Fill(ds, "Takmicenje");
                dataGridView1.DataSource = ds.Tables["Takmicenje"].DefaultView;
                dataGridView1.Columns[0].HeaderText = "Šifra";
                dataGridView1.Columns[1].HeaderText = "Naziv kategorije";
                dataGridView1.Columns[2].HeaderText = "Broj pasa";

                // suma broja pasa u 3. koloni gridview1, nazvanoj "br_pasa" u prethodnom SQL upitu
                label10.Text = Convert.ToString(ds.Tables[0].Compute("Sum(br_pasa)", String.Empty));

                // za chart je u Properties postavljen SERIES na Takmicenje i tamo se podešava tip charta
                // i ponešto još od atributa, a ovde u kodu se naznačava sadržaj X i Y ose na osnovu DS
                chart1.Titles.Clear();
                chart1.Titles.Add("KATEGORIJE"); // bez naslova neće da radi, ne znam zašto
                chart1.DataSource = ds;
                chart1.Series["Takmicenje"].XValueMember = ds.Tables[0].Columns[1].ColumnName;
                chart1.Series["Takmicenje"].YValueMembers = ds.Tables[0].Columns[2].ColumnName;
                
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

        // briše grid, briše chart, vraća pogled na 1. tab!
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            chart1.DataSource = null;
            chart1.Series["Takmicenje"].Points.Clear();
            tabControl1.SelectedTab = tabPage1;
        }

        // ničemu ne služi
        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        // ovim se realizuje reakcija na klik na poslednji tab - i izlazi se iz aplikacije
        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Izlaz")
            {
                this.Close();
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
