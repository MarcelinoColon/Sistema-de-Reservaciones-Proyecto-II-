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
        private PanelManager panelManager;
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
            panelManager = new PanelManager();
        }
        private void OnPanelClick(panelProductos panel)
        {
            tagSeleccionado = (int?)panel.Tag; // El panel Tag es de tipo object, necesitamos convertirlo a int?
            MesasForm.idmenu = tagSeleccionado.Value;
        }
        private void LoadPaneles()
        {
            flowLayoutPanel1.Controls.Clear();
            var paneles = panelManager.GetPaneles();

            foreach (var panel in paneles)
            {
                panelProductos panelDinamico = new panelProductos(panel.Tag, panel.Descripcion, panel.Precio, panel.ImagenPath);
                panelDinamico.Click += (sender, e) => OnPanelClick(panelDinamico);  // Manejo de clic en el panel

                // Agregar el manejador de clic a los controles dentro del panel
                panelDinamico.pictureBox.Click += (sender, e) => panelDinamico.OnPanelClick();  // Al hacer clic en PictureBox, invocar el clic del panel
                panelDinamico.labelDescripcion.Click += (sender, e) => panelDinamico.OnPanelClick();   // Al hacer clic en Label, invocar el clic del panel

                flowLayoutPanel1.Controls.Add(panelDinamico);
            }
        }
        private void MesasForm_Load(object sender, EventArgs e)
        {
            LoadPaneles();
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
            string filterText = tbFiltro.Text.ToLower();

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is panelProductos panel)
                {
                    // Filtrar en el texto de la descripcion del panel
                    bool matchesFilter = panel.labelDescripcion.Text.ToLower().Contains(filterText);

                    // Mostrar u ocultar el panel según el filtro
                    panel.Visible = matchesFilter;
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
