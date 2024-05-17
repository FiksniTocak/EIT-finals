using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proba_Baze_Forma
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.richTextBox1.SelectedText = Properties.Resources.HelpDoc;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

// ovde skoro da nema ničega, ali se u konstruktoru (a moglo je i u Form_Load-u) u richTxB ubacuje dokument
// u pitanju je običan text fajl koji se nalazi u Resources delu projekta, i kada se bilduje - ugrađuje se
// u sam EXE fajl - kao i slike koje idu uz ovaj projekat