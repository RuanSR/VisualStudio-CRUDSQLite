using System;
using CRUDSQLite.Classes;
using System.Windows.Forms;
using System.Drawing;
using CRUDSQLite.Forms;
using CRUDSQLite.Classes.DB;

namespace CRUDSQLite
{
    public partial class mainForm : Form
    {
        DBManager dbManager;
        //LOAD && CONSTRUTOR\\
        public mainForm()
        {
            InitializeComponent();
        }
        //MENUBAR\\
        private void SobreMenu_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                new Init();
                gridViewUsers = new DBManager().GetData(gridViewUsers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Tab1Componnts();
                Tab2Componets();
            }
        }
        //TAB1\\
        public void Tab1Componnts()
        {
            txtPesquisa = EnableHint(txtPesquisa,"Pesquisar...");
        }
        private void TxtPesquisa_Enter(object sender, EventArgs e)
        {
            txtPesquisa = DisableHint(txtPesquisa);
        }
        private void TxtPesquisa_Leave(object sender, EventArgs e)
        {
            Tab1Componnts();
        }
        //TAB2\\
        private void Tab2Componets()
        {
            txtNome = EnableHint(txtNome, "Nome completo...");
            //txtNascimento = EnableHint(txtNascimento, "00/00/0000");
            txtRU = EnableHint(txtRU, "Registro Único, até 10 caracteres");
            txtObs = EnableHint(txtObs, "Descreva alguma observação (Opcional)");
            cbSexo.Items.Add("M");
            cbSexo.Items.Add("F");
        }

        //UTIL\\
        private void FocusEnter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            switch (txt.Name)
            {
                case "txtNome":
                    txtNome = DisableHint(txtNome);
                    break;
                case "txtRU":
                    txtRU = DisableHint(txtRU);
                    break;
                case "txtObs":
                    txtObs = DisableHint(txtObs);
                    break;
            }
        }
        private void FocusLeave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            switch (txt.Name)
            {
                case "txtNome":

                    txtNome = VerificaTxt(txt, "Nome completo...");
                    break;
                case "txtRU":
                    txtRU = VerificaTxt(txt, "Registro Único, até 10 caracteres");
                    break;
                case "txtObs":
                    txtObs = VerificaTxt(txt, "Descreva alguma observação (Opcional)");
                    break;
            }
        }
        private TextBox EnableHint(TextBox txt, string hint)
        {
            txt.Text = hint;
            txt.ForeColor = Color.Silver;
            return txt;
        }
        private TextBox DisableHint(TextBox txt)
        {
            txt.Text = string.Empty;
            txt.ForeColor = Color.Black;
            return txt;
        }
        private TextBox VerificaTxt(TextBox txt, string hint)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {
                txt = EnableHint(txt, hint);
            }
            return txt;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                dbManager = new DBManager();
                if (txtNome.Name != string.Empty &&
                    txtNascimento.Text != string.Empty &&
                    txtRU.Text != string.Empty &&
                    cbSexo.Text != string.Empty)
                {
                    dbManager.InsertData(txtNome.Text, txtNascimento.Text,txtRU.Text,char.Parse(cbSexo.Text),txtObs.Text);
                    MessageBox.Show("Adicionado com sucesso!", "OK",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    LoadUsers();
                    tabControl1.SelectTab(0);
                }
                else
                {
                    MessageBox.Show("Preencha tudo coretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void LoadUsers()
        {
            gridViewUsers.Rows.Clear();
            gridViewUsers = new DBManager().GetData(gridViewUsers);
        }
    }
}
