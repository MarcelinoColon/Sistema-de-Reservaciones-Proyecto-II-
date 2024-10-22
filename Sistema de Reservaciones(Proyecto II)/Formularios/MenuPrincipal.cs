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
            btnClientes.BorderStyle = BorderStyle.FixedSingle;
            btnClientes.Cursor = Cursors.Hand;
        }

        private void btnClientes_MouseLeave(object sender, EventArgs e)
        {
            btnClientes.BorderStyle = BorderStyle.Fixed3D;
            btnClientes.Cursor = Cursors.Default;
        }

        private void btnCerrarSesion_MouseEnter(object sender, EventArgs e)
        {
            btnCerrarSesion.BorderStyle = BorderStyle.FixedSingle;
            btnCerrarSesion.Cursor = Cursors.Hand;
        }

        private void btnCerrarSesion_MouseLeave(object sender, EventArgs e)
        {
            btnCerrarSesion.BorderStyle = BorderStyle.Fixed3D;
            btnCerrarSesion.Cursor = Cursors.Default;
        }

        private void btnReservaciones_MouseEnter(object sender, EventArgs e)
        {
            btnReservaciones.BorderStyle = BorderStyle.FixedSingle;
            btnReservaciones.Cursor = Cursors.Hand;
        }

        private void btnReservaciones_MouseLeave(object sender, EventArgs e)
        {
            btnReservaciones.BorderStyle = BorderStyle.Fixed3D;
            btnReservaciones.Cursor = Cursors.Default;
        }

        private void btnReportes_MouseEnter(object sender, EventArgs e)
        {
            btnReportes.BorderStyle = BorderStyle.FixedSingle;
            btnReportes.Cursor = Cursors.Hand;
        }

        private void btnReportes_MouseLeave(object sender, EventArgs e)
        {
            btnReportes.BorderStyle = BorderStyle.Fixed3D;
            btnReportes.Cursor = Cursors.Default;
        }

        private void btnMenu_MouseEnter(object sender, EventArgs e)
        {
            btnMenu.BorderStyle = BorderStyle.FixedSingle;
            btnMenu.Cursor = Cursors.Hand;
        }

        private void btnMenu_MouseLeave(object sender, EventArgs e)
        {
            btnMenu.BorderStyle = BorderStyle.Fixed3D;
            btnMenu.Cursor = Cursors.Default;
        }

        private void btnMesas_MouseEnter(object sender, EventArgs e)
        {
            btnMesas.BorderStyle = BorderStyle.FixedSingle;
            btnMesas.Cursor = Cursors.Hand;
        }

        private void btnMesas_MouseLeave(object sender, EventArgs e)
        {
            btnMesas.BorderStyle = BorderStyle.Fixed3D;
            btnMesas.Cursor = Cursors.Default;
        }

        private void btnEmpleados_MouseEnter(object sender, EventArgs e)
        {
            btnEmpleados.BorderStyle = BorderStyle.FixedSingle;
            btnEmpleados.Cursor = Cursors.Hand;
        }

        private void btnEmpleados_MouseLeave(object sender, EventArgs e)
        {
            btnEmpleados.BorderStyle = BorderStyle.Fixed3D;
            btnEmpleados.Cursor = Cursors.Default;
        }
    }
}
