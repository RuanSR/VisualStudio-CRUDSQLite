﻿using System;
using CRUDSQLite.Classes;
using System.Windows.Forms;
using CRUDSQLite.Forms;
using CRUDSQLite.Classes.DB;
using CRUDSQLite.Model;

namespace CRUDSQLite
{
    public partial class mainForm : Form
    {
        private readonly UserRepository userRepo;
        private User Usuario { get; set; }

        //CONSTRUTOR\\
        public mainForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
        }

        //CONTROLES\\
        private void SobreMenu_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                new Init();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Tab2Componets();
            }
        }
        private void TxtPesquisa_Enter(object sender, EventArgs e)
        {
            //txtPesquisa = DisableHint(txtPesquisa);
        }
        private void TxtPesquisa_Leave(object sender, EventArgs e)
        {
            
        }
        private void FocusEnter(object sender, EventArgs e)
        {
            //TextBox txt = (TextBox)sender;
            //switch (txt.Name)
            //{
            //    case "txtNome":
            //        txtNome = DisableHint(txtNome);
            //        break;
            //    case "txtRU":
            //        txtRU = DisableHint(txtRU);
            //        break;
            //    case "txtObs":
            //        txtObs = DisableHint(txtObs);
            //        break;
            //}
        }
        private void FocusLeave(object sender, EventArgs e)
        {
            //TextBox txt = (TextBox)sender;
            //switch (txt.Name)
            //{
            //    case "txtNome":

            //        txtNome = VerificaTxt(txt, "Nome completo...");
            //        break;
            //    case "txtRU":
            //        txtRU = VerificaTxt(txt, "Registro Único, até 10 caracteres");
            //        break;
            //    case "txtObs":
            //        txtObs = VerificaTxt(txt, "Descreva alguma observação (Opcional)");
            //        break;
            //}
        }
        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Usuario != null)
                {
                    UpdateUsuario();
                    userRepo.UpdateUser(Usuario);
                    Tab2Componets();
                    LoadUsers();
                    tabControl1.SelectTab(0);
                    Usuario = null;
                    MessageBox.Show("Atualizado com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Usuario = new User(txtNome.Text, txtNascimento.Text, txtRU.Text, cbSexo.Text, txtObs.Text);
                    userRepo.NewUser(Usuario);
                    LoadUsers();
                    Tab2Componets();
                    tabControl1.SelectTab(0);
                    Usuario = null;
                    MessageBox.Show("Adicionado com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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
        private void gridViewUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int Id = int.Parse(gridViewUsers.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                var userDB = userRepo.GetUser(Id);

                if (userDB == null)
                {
                    throw new Exception("Erro ao identificar usuario.");
                }

                if (gridViewUsers.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                    Usuario = userDB;
                    EditarUsuario(userDB);
                }
                else if (gridViewUsers.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    if (MessageBox.Show("Realmente deseja excluir este usuário da base de dados?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        userRepo.DeleteData(Id);
                        LoadUsers();
                        MessageBox.Show("Usuário removido com sucesso!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter usuario! Detalhes: {ex.Message}");
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
        private void btnNovo_Click(object sender, EventArgs e)
        {
            Tab2Componets();
        }
        //METODOS\\
        private void LoadUsers()
        {
            var dados = new UserRepository().GetUser();
            gridViewUsers.DataSource = dados;
        }
        private void EditarUsuario(User user)
        {
            txtNome.Text = user.Nome;
            txtNascimento.Text = user.Nascimento;
            txtRU.Text = user.RU;
            cbSexo.SelectedItem = user.Genero;
            txtObs.Text = user.Obs;
            tabControl1.SelectedIndex = 1;
        }
        private void Tab2Componets()
        {
            txtNome.Text = string.Empty;
            txtNascimento.Text = string.Empty;
            txtRU.Text = string.Empty;
            txtObs.Text = string.Empty;
            cbSexo.Items.Clear();
            cbSexo.Items.Add("M");
            cbSexo.Items.Add("F");
            Usuario = null;
        }
        private void UpdateUsuario()
        {
            Usuario.Nome = txtNome.Text;
            Usuario.Nascimento = txtNascimento.Text;
            Usuario.RU = txtRU.Text;
            Usuario.Genero = cbSexo.Text;
            Usuario.Obs = txtObs.Text;
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            var dados = new UserRepository().GetUser(txtPesquisa.Text);
            gridViewUsers.DataSource = dados;
        }
    }
}
