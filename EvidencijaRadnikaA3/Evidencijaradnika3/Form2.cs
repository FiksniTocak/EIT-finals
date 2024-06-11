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
using System.Configuration;
using System.Windows.Forms.DataVisualization.Charting;

namespace Evidencijaradnika3
{
    public partial class Form2 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source = Profesorski-R3; Initial Catalog = Radnici ; Integrated Security = True ");
        SqlCommand cmd = new SqlCommand();
        public Form2()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int godina = (int)numericUpDown1.Value;
                cmd.Connection = conn;
                cmd.CommandText = "select distinct YEAR(DatumAngazovanja) as Godina " +
                    ", COUNT(distinct ProjekatID) as BrojProjekata, COUNT(RadnikID) " +
                    " as BrojRadnika from Angazman " +
                    "group by YEAR(DatumAngazovanja) " +
                    "having YEAR(DatumAngazovanja) > 2023-@god";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@god", godina);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
                chart1.Series["Series1"].Points.Clear();

                foreach (DataRow item in dt.Rows)
                {
                    chart1.Series["Series1"].IsValueShownAsLabel = true;
                    chart1.Series["Series1"].Points.AddXY(item[0].ToString(), item[2]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
