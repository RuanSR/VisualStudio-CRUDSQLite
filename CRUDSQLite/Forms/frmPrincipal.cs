using System;
using CRUDSQLite.Classes;
using System.Windows.Forms;

namespace CRUDSQLite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Init();
        }
    }
}
