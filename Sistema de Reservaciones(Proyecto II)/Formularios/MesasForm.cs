using Sistema_de_Reservaciones_Proyecto_II_.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    public partial class MesasForm : Form
    {
        ButtonManager buttonManager = new ButtonManager();
        private int? tagSeleccionado;
        public static int idmenu;
        int mesa;
        string silla;

        public MesasForm()
        {
            InitializeComponent();
            ocultarTab();
            this.Text = "Mesas";
            flpDesayuno.Visible = false;
            flpAlmuerzo.Visible = false;
            flpPostres.Visible = false;
            flpBebidas.Visible = false;
            lbCantidad.Text = "1";
            PData.IdMenuChanged += ActualizarPrecio;
            buttonManager.CargarBotonesDesdeJson(flpDesayuno, new List<string> { "Desayuno"});
            buttonManager.CargarBotonesDesdeJson(flpAlmuerzo, new List<string> { "Almuerzo" });
            buttonManager.CargarBotonesDesdeJson(flpPostres, new List<string> { "Postre" });
            buttonManager.CargarBotonesDesdeJson(flpBebidas, new List<string> { "Bebida" });
        }
        private void ActualizarPrecio()
        {
            PData.ActualizarLabelPrecio(lbPrecio); // Actualizar el precio en el Label
        }

        private void MesasForm_Load(object sender, EventArgs e)
        {

        }
        private void ocultarTab()
        {
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.Appearance = TabAppearance.FlatButtons;
        }
        private void siguienteTab()
        {
            if (tabControl1.SelectedIndex < tabControl1.TabCount - 1)
            {
                tabControl1.SelectedIndex++;
            }
        }
        private void tabAnterior()
        {
            if (tabControl1.SelectedIndex > 0)
            {
                tabControl1.SelectedIndex--;
            }
        }

        //Tab Menu
        private void iconButton1_Click(object sender, EventArgs e)
        {
            tabAnterior();
        }


        private void btnAtrasSillas_Click(object sender, EventArgs e)
        {
            siguienteTab();
        }

        private void btnDesayunos_Click(object sender, EventArgs e)
        {
            if (flpDesayuno.Visible == false) { flpDesayuno.Visible = true; }
            flpAlmuerzo.Visible = false;
            flpPostres.Visible = false;
            flpBebidas.Visible = false;
        }

        private void btnAlmuerzos_Click_1(object sender, EventArgs e)
        {
            if (flpAlmuerzo.Visible == false) { flpAlmuerzo.Visible = true; }
            flpDesayuno.Visible = false;
            flpPostres.Visible = false;
            flpBebidas.Visible = false;
        }

        private void btnPostres_Click_1(object sender, EventArgs e)
        {
            if (flpPostres.Visible == false) { flpPostres.Visible = true; }
            flpDesayuno.Visible = false;
            flpAlmuerzo.Visible = false;
            flpBebidas.Visible = false;
        }

        private void btnBebidas_Click_1(object sender, EventArgs e)
        {
            if (flpBebidas.Visible == false) { flpBebidas.Visible = true; }
            flpDesayuno.Visible = false;
            flpAlmuerzo.Visible = false;
            flpPostres.Visible = false;
        }

        //MESAS 
        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            siguienteTab();
            mesa = 1;
        }
        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            siguienteTab();
            mesa = 2;
        }

        //SILLAS

        private void iconButton7_Click(object sender, EventArgs e)
        {
            siguienteTab();
            silla = "1";
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            siguienteTab();
            silla = "2";
        }
        private void btnSilla3_Click(object sender, EventArgs e)
        {
            siguienteTab();
            silla = "3";
        }

        private void btnSilla4_Click(object sender, EventArgs e)
        {
            siguienteTab();
            silla = "4";
        }

        //OTROS
        private void btnTomarOrden_Click(object sender, EventArgs e)
        {

        }

        private void tbFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = tbFiltro.Text.Trim().ToLower();

            // Llama al método para filtrar botones en cada FlowLayoutPanel
            FiltrarBotones(flpDesayuno, filtro);
            FiltrarBotones(flpAlmuerzo, filtro);
            FiltrarBotones(flpBebidas, filtro);
            FiltrarBotones(flpPostres, filtro);
        }
        private void FiltrarBotones(FlowLayoutPanel panel, string filtro)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Button boton)
                {
                    // Compara el texto del botón con el filtro
                    boton.Visible = boton.Text.ToLower().Contains(filtro);
                }
            }
        }

            private void btnMas_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lbCantidad.Text, out int currentValue))
            {
                currentValue++; // Incrementar el valor.
                lbCantidad.Text = currentValue.ToString(); // Asignar el nuevo valor.
            }
            else
            {
                // Manejar casos donde el texto no sea un número válido.
                MessageBox.Show("El valor actual no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lbCantidad.Text = "1"; // Restaurar al valor predeterminado.
            }
        }

        private void btnMenos_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lbCantidad.Text, out int currentValue))
            {
                if (currentValue > 1) // Asegurarse de que no baje de 1.
                {
                    currentValue--; // Decrementar el valor.
                    lbCantidad.Text = currentValue.ToString(); // Asignar el nuevo valor.
                }
                else
                {
                    MessageBox.Show("El valor no puede ser menor que 1.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Manejar casos donde el texto no sea un número válido.
                MessageBox.Show("El valor actual no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lbCantidad.Text = "1"; // Restaurar al valor predeterminado.
            }
        }
    }
}
