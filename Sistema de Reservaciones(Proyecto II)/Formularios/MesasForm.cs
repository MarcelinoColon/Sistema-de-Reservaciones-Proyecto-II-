using Sistema_de_Reservaciones_Proyecto_II_.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    public partial class MesasForm : Form
    {
        ButtonManager buttonManager = new ButtonManager();
        MostrarDatos datos = new MostrarDatos();
        int mesa;
        int silla;
        int idOrden;
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
        private void MostrarDetallesOrdenes(int Vmesa, int Vsilla)
        {
            MostrarDatos objDetallesOrdenes = new MostrarDatos();
            dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(Vmesa, Vsilla);
        }
        private void Orden()
        {
            idOrden = datos.ObtenerOrdenActiva(mesa, silla);

            if (idOrden == 0)
            {
                // Crear nueva orden si no hay activa
                idOrden = datos.CrearNuevaOrden(mesa, silla);
                MessageBox.Show($"Nueva orden creada con ID: {idOrden}");
            }
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
            mesa = 1;
            siguienteTab();
            lbMesa.Text = "Mesa 1";
        }
        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            mesa = 2;
            siguienteTab();
            lbMesa.Text = "Mesa 2";
        }

        //SILLAS

        private void btnSilla1_Click(object sender, EventArgs e)
        {
            silla = 1;
            Orden();
            MostrarDetallesOrdenes(silla, mesa);
        }

        private void btnSilla2_Click(object sender, EventArgs e)
        {
            silla = 2;
            Orden();
            MostrarDetallesOrdenes(silla, mesa);
        }

        private void btnSilla3_Click_1(object sender, EventArgs e)
        {
            silla = 3;
            Orden();
            MostrarDatos objDetallesOrdenes = new MostrarDatos();
            dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(mesa, silla);
        }

        private void btnSilla4_Click_1(object sender, EventArgs e)
        {
            silla = 4;
            Orden();
            MostrarDatos objDetallesOrdenes = new MostrarDatos();
            dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(mesa, silla);
        }

        //OTROS


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
            // Intentar convertir el texto del Label a un número entero.
            if (int.TryParse(lbCantidad.Text, out int currentValue))
            {
                if (currentValue < 9) // Verificar si el valor actual es menor que 9.
                {
                    currentValue++; // Incrementar el valor.
                    lbCantidad.Text = currentValue.ToString(); // Asignar el nuevo valor.
                }
                else
                {
                    // Mostrar un mensaje si el valor alcanza el límite.
                    MessageBox.Show("El valor no puede ser mayor que 9.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            tabAnterior();
        }

        private void btnTomarOrden_Click(object sender, EventArgs e)
        {
            decimal precio = PData.ObtenerPrecioProducto(PData.idmenu);
            int idOrden = datos.ObtenerOrdenActiva(mesa, silla);
            if (idOrden > 0)
            {
                datos.AgregarDetalleOrden(idOrden, PData.idmenu, int.Parse(lbCantidad.Text), precio);
                MessageBox.Show("Producto agregado a la orden.");
            }
            else
            {
                MessageBox.Show("No se pudo obtener la orden activa.");
            }
            MostrarDatos objDetallesOrdenes = new MostrarDatos();
            dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(mesa, silla);
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {

        }
    }
}
