using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDacunteTp2
{
    public partial class frmRegistrarse : Form
    {
        public frmRegistrarse()
        {
            InitializeComponent();
        }

        clsBaseDatos Base = new clsBaseDatos();

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmIniciarSesion v = new frmIniciarSesion();
            v.Show();
            this.Hide();
        }

        private void frmRegistrarse_Load(object sender, EventArgs e)
        {

        }

        

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;
            Base.ValidarUsuario(nombreUsuario , contraseña);
            Base.RegistrarUsuario(nombreUsuario, contraseña);
        }
    }
}
