using CRUDSQLite.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDSQLite.Forms
{
    public partial class frmUser : Form
    {
        private User Usuario { get; }
        public frmUser(User user)
        {
            Usuario = user;
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            ShowUserData();
        }
        private void ShowUserData()
        {
            lblName.Text = Usuario.Nome;
            lblNascimento.Text = Usuario.Nascimento;
            lblSexo.Text = Usuario.Genero;
            txtObs.Text = Usuario.Obs;
        }

    }
}
