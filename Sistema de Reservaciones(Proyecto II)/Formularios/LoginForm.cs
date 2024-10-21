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
using System.Data.SqlClient;
using Sistema_de_Reservaciones_Proyecto_II_.Formularios;

namespace Sistema_de_Reservaciones_Proyecto_II_
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PanelLogin.BackColor = Color.FromArgb(100, 0, 0, 0);
            try
            {
                DBGeneral.ObtenerConexion();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from Usuario where nombre_usuario='" + tbUsuario.Text + "' and contrasena='" + tbContraseña.Text + "'", DBGeneral.ObtenerConexion());
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string usuario = reader["nombre_usuario"].ToString();
                    string contrasena = reader["contrasena"].ToString();
                    string tipoUsuario = reader["id_tipo_usuario"].ToString();

                    UserCache.Initialize(usuario, contrasena, tipoUsuario);
                    MenuPrincipal menuprincipal = new MenuPrincipal();
                    menuprincipal.Show();
                    this.Hide();
                    MessageBox.Show("Bienvenido "+ usuario);
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña incorrecto");
                }
            }
            catch(Exception ex )
            {
              MessageBox.Show(ex.Message);
            }
        }
    }
}
