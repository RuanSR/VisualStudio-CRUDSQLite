using System;
using CRUDSQLite.Classes;
using System.Windows.Forms;
using System.Drawing;

namespace CRUDSQLite
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                new Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Erro!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            EnableHintPesquisa();
        }

        private void EnableHintPesquisa()
        {
            txtPesquisa.Text = "Pesquisar...";
            txtPesquisa.ForeColor = Color.Silver;
        }
        private void DisableHintPesquisa()
        {
            txtPesquisa.Text = string.Empty;
            txtPesquisa.ForeColor = Color.Black;
        }
        private void TxtPesquisa_Enter(object sender, EventArgs e)
        {
            DisableHintPesquisa();
        }
        private void TxtPesquisa_Leave(object sender, EventArgs e)
        {
            EnableHintPesquisa();
        }
    }
}
