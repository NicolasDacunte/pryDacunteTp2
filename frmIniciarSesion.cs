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
    public partial class frmIniciarSesion : Form
    {
        public frmIniciarSesion()
        {
            InitializeComponent();
        }

        private void frmIniciarSesion_Load(object sender, EventArgs e)
        {

        }

        clsBaseDatos Base = new clsBaseDatos();

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            frmRegistrarse v = new frmRegistrarse();
            v.Show();
            this.Hide();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            String usuario = txtUsuario.Text;
            String Contraseña = txtContraseña.Text;
            Base.RegistroLogInicioSesion();

            if (Base.estadoConexion == "Usuario EXISTE")
            {
                Base.RegistroLogInicioSesion();
                MessageBox.Show("Inicio de sesión exitoso");
                // Aquí puedes abrir el formulario principal de tu aplicación
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas");
            }
        }
    }
}
