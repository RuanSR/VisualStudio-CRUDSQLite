using System;
using CRUDSQLite.Classes;
using System.Windows.Forms;
using System.Drawing;
using CRUDSQLite.Forms;
using CRUDSQLite.Classes.DB;
using CRUDSQLite.Model;
using System.Linq.Expressions;

namespace CRUDSQLite
{
    public partial class mainForm : Form
    {
        private readonly UserRepository userRepo;
        //LOAD && CONSTRUTOR\\
        public mainForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
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
                gridViewUsers = new UserRepository().GetData(gridViewUsers);
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
            txtRU = EnableHint(txtRU, "Registro Único, até 10 caracteres");
            txtObs = EnableHint(txtObs, "Descreva alguma observação (Opcional)");
            cbSexo.Items.Clear();
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
                User usuario = new User(txtNome.Text, txtNascimento.Text, txtRU.Text, cbSexo.Text, txtObs.Text);

                userRepo.NewUser(usuario);
                MessageBox.Show("Adicionado com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                Tab2Componets();
                tabControl1.SelectTab(0);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void LoadUsers()
        {
            gridViewUsers.Rows.Clear();
            gridViewUsers = new UserRepository().GetData(gridViewUsers);
        }

        private void gridViewUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int Id = int.Parse(gridViewUsers.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                var user = userRepo.GetUser(Id);

                if (user == null)
                {
                    throw new Exception("Erro ao identificar usuario.");
                }

                if (gridViewUsers.Columns[e.ColumnIndex].Name == "btnEdit")
                {

                }
                else if (gridViewUsers.Columns[e.ColumnIndex].Name == "btnDelete")
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao entrar na conta! Detalhes: {ex.Message}");
            }
        }

        private void gridViewUsers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int Id = int.Parse(gridViewUsers.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                var user = userRepo.GetUser(Id);

                if (user == null)
                {
                    throw new Exception("Erro ao identificar usuario.");
                }

                frmUser frm = new frmUser(user);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao entrar na conta! Detalhes: {ex.Message}");
            }
        }
    }
}
