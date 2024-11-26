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
    public partial class MesasForm : Form
    {

        private int? tagSeleccionado;
        public static int idmenu;
        public MesasForm()
        {
            InitializeComponent();
            ocultarTab();
            this.Text = "Mesas";
            flowLayoutPanel1.Visible = false;
            pnAlmuerzo.Visible = false;
            pnPostres.Visible = false;
            pnBebidas.Visible = false;
            lbCantidad.Text = "1";

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
            if (flowLayoutPanel1.Visible == false) { flowLayoutPanel1.Visible = true; }
            pnAlmuerzo.Visible = false;
            pnPostres.Visible = false;
            pnBebidas.Visible = false;
        }

        private void btnAlmuerzos_Click_1(object sender, EventArgs e)
        {
            if (pnAlmuerzo.Visible == false) { pnAlmuerzo.Visible = true; }
            flowLayoutPanel1.Visible = false;
            pnPostres.Visible = false;
            pnBebidas.Visible = false;
        }

        private void btnPostres_Click_1(object sender, EventArgs e)
        {
            if (pnPostres.Visible == false) { pnPostres.Visible = true; }
            flowLayoutPanel1.Visible = false;
            pnAlmuerzo.Visible = false;
            pnBebidas.Visible = false;
        }

        private void btnBebidas_Click_1(object sender, EventArgs e)
        {
            if (pnBebidas.Visible == false) { pnBebidas.Visible = true; }
            flowLayoutPanel1.Visible = false;
            pnAlmuerzo.Visible = false;
            pnPostres.Visible = false;
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            siguienteTab();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            siguienteTab();
        }


        private void btnTomarOrden_Click(object sender, EventArgs e)
        {

        }

        private void tbFiltro_TextChanged(object sender, EventArgs e)
        {

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
