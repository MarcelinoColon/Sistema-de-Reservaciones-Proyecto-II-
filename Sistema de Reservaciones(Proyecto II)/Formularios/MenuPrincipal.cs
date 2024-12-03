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
using FontAwesome.Sharp;

namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    public partial class MenuPrincipal : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        public MenuPrincipal()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1200, 800);
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelBotones.Controls.Add(leftBorderBtn);
            IniciarTemporizadorFechaHora();
        }
        private void IniciarTemporizadorFechaHora()
        {
            Timer timer = new Timer();
            timer.Interval = 1000; // Intervalo de 1 segundo (1000 ms)
            timer.Tick += ActualizarFechaHoraLabel; // Asigna el método que actualizará el Label
            timer.Start();
        }
        private void ActualizarFechaHoraLabel(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(163, 180, 209);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //Current Child Form Icon
                iconoMenuActual.IconChar = currentBtn.IconChar;
                iconoMenuActual.IconColor = color;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(153, 180, 209);
                currentBtn.ForeColor = Color.Black;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Black;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PanelPrincipal.Controls.Add(childForm);
            PanelPrincipal.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            Titulo.Text = childForm.Text;
        }
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(255, 255, 255);
            public static Color color2 = Color.FromArgb(0, 0, 0);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconoMenuActual.IconChar = IconChar.Home;
            iconoMenuActual.IconColor = RGBColors.color2;
            Titulo.Text = "Inicio";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void btnReservaciones_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new ReservacionesForm());
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new MenuForm());
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new MesasForm());
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new CajaForm());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new ReportesForm());
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new ClientesForm());
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new EmpleadosForm());
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
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
    }
}
