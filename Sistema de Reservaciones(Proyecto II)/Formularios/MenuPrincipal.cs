using Sistema_de_Reservaciones_Proyecto_II_.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            AbrirFormHijo(new ClientesForm());
        }


        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            if (UserCache.Current.TipoUsuario == "4")
            {
                
            }
            else
            {
                
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro de que desea cerrar sesión?",
                                     "Cerrar Sesión",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                LoginForm loginform = new LoginForm();
                loginform.Show();
                this.Close();
            }
        }

        private void AbrirFormHijo(object formhijo)
        {
            if (this.PanelPrincipal.Controls.Count> 0)
                this.PanelPrincipal.Controls.RemoveAt(0);
            Form fh = formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.PanelPrincipal.Controls.Add(fh);
            this.PanelPrincipal.Tag = fh;
            fh.Show();
        }

        private void btnClientes_MouseEnter(object sender, EventArgs e)
        {

        }
    }
}
