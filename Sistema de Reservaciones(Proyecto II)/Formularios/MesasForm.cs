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
        MesasManager mesaManager = new MesasManager();
        MostrarDatos datos = new MostrarDatos();
 
        int silla;
        int idOrden;
        public MesasForm()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(tabPage3);
            this.Text = "Mesas";
            flpDesayuno.Visible = false;
            flpAlmuerzo.Visible = false;
            flpPostres.Visible = false;
            flpBebidas.Visible = false;

            flpMesasLibres.Visible = false;
            flpMesasOcupadas.Visible = false;
            flpMesasReservadas.Visible = false;
            lbCantidad.Text = "1";
            CargarMesas();
            AsignarEventoClickAMesasLibres();
            AsignarEventoClickAMesasOcupadas();
            AsignarEventoClickAMesasReservadas();
            PData.IdMenuChanged += ActualizarPrecio;
            buttonManager.CargarBotonesDesdeJson(flpDesayuno, new List<string> { "Desayuno"});
            buttonManager.CargarBotonesDesdeJson(flpAlmuerzo, new List<string> { "Almuerzo" });
            buttonManager.CargarBotonesDesdeJson(flpPostres, new List<string> { "Postre" });
            buttonManager.CargarBotonesDesdeJson(flpBebidas, new List<string> { "Bebida" });
        }
        private void CargarMesas()
        {
            mesaManager.CargarMesasDesdeJson(flpMesasLibres, new List<string> { "Libre" });
            mesaManager.CargarMesasDesdeJson(flpMesasOcupadas, new List<string> { "Ocupada" });
            mesaManager.CargarMesasDesdeJson(flpMesasReservadas, new List<string> { "Reservada" });
        }
        private void ClearImput()
        {
            lbCantidad.Text = "1";
            lbPrecio.Text = "0.00 ";
        }

        private void Orden()
        {
            ConfigurarColumnas();
            bool existePendiente = datos.ExisteOrdenPendiente(PMesa.Id, silla);
            if (existePendiente)
            {
                MessageBox.Show(
                    "No se puede abrir una nueva orden porque ya existe una orden pendiente para esta silla.",
                    "Orden Pendiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return; // Salir del método si hay una orden pendiente
            }
            idOrden = datos.ObtenerOrdenActiva(PMesa.Id, silla);

            if (idOrden == 0)
            {
                var result = MessageBox.Show(
                "¿Está seguro de que desea crear una nueva orden?",
                "Confirmar creación de orden",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Crear nueva orden si no hay activa
                    idOrden = datos.CrearNuevaOrden(PMesa.Id, silla);
                    MessageBox.Show($"Nueva orden creada con ID: {idOrden}");
                }
            }
        }
        private void ConfigurarColumnas()
        {
            // Desactiva la generación automática de columnas
            dgvOrdenes.AutoGenerateColumns = false;

            // Limpia cualquier columna previa
            dgvOrdenes.Columns.Clear();

            // Configura columnas manualmente
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Orden", DataPropertyName = "Orden", Width = 50, Visible = false }); // Oculta esta columna
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", DataPropertyName = "Producto", Width = 76 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", DataPropertyName = "Cantidad", Width = 40 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Precio", DataPropertyName = "Precio", Width = 30 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", DataPropertyName = "Total", Width = 30 });
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
            tabControl1.TabPages.Add(tabPage3);
            tabControl1.TabPages.Remove(tabPage1);
            PMesa.Id = 1;
            siguienteTab();
            lbMesa.Text = "Mesa "+PMesa.Id;
        }
        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabPage3);
            tabControl1.TabPages.Remove(tabPage1);
            PMesa.Id = 2;
            siguienteTab();
            lbMesa.Text = "Mesa 2";
        }

        //SILLAS

        private void btnSilla1_Click(object sender, EventArgs e)
        {
            silla = 1;
            Orden();
            MostrarDatos objDetallesOrdenes = new MostrarDatos();
            dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(PMesa.Id, silla);
        }

        private void btnSilla2_Click(object sender, EventArgs e)
        {
            silla = 2;
            Orden();
            MostrarDatos objDetallesOrdenes = new MostrarDatos();
            dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(PMesa.Id, silla);
        }

        private void btnSilla3_Click_1(object sender, EventArgs e)
        {
            silla = 3;
            Orden();
            MostrarDatos objDetallesOrdenes = new MostrarDatos();
            dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(PMesa.Id, silla);
        }

        private void btnSilla4_Click_1(object sender, EventArgs e)
        {
            silla = 4;
            Orden();
            MostrarDatos objDetallesOrdenes = new MostrarDatos();
            dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(PMesa.Id, silla);
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
            tabControl1.TabPages.Add(tabPage1);
            tabControl1.TabPages.Remove(tabPage3);
            tabAnterior();
        }

        private void btnTomarOrden_Click(object sender, EventArgs e)
        {
            decimal precio = PData.ObtenerPrecioProducto(PData.idmenu);
            int idOrden = datos.ObtenerOrdenActiva(PMesa.Id, silla);
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
            dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(PMesa.Id, silla);
            ClearImput();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro de que desea facturar la orden "+idOrden+ "?",
                                     "Facturar orden",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                MostrarDatos objDetallesOrdenes = new MostrarDatos();
                objDetallesOrdenes.ActualizarOrden(idOrden);
                dgvOrdenes.DataSource = objDetallesOrdenes.MostrarDetallesOrdenes(PMesa.Id, silla);
            }
            ClearImput();
        }
        // --------------------------MESAS-----------------------------------
        //--------------------------LIBRES-----------------------------------
        private void AsignarEventoClickAMesasLibres()
        {
            foreach (Control control in flpMesasLibres.Controls)
            {
                if (control is CustomMesa mesa)
                {
                    // Asignar el evento Click al control CustomMesa
                    mesa.Click += Boton_ClickLibres;
                }
            }
        }
        private void Boton_ClickLibres(object sender, EventArgs e)
        { 
            if (sender is CustomMesa mesa)
            {
                PMesa.Id = mesa.Id;
                DialogResult result = MessageBox.Show(
                "¿Estás seguro de ocupar esta mesa?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                // Verificar la respuesta del usuario
                if (result == DialogResult.Yes)
                {
                    datos.OcuparMesa(PMesa.Id);
                    MessageBox.Show("La mesa ha sido ocupada.");
                }
                else
                {
                    // Acción si el usuario hace clic en "No"
                    MessageBox.Show("Operación cancelada.");
                }
                flpMesasLibres.Controls.Clear();
                flpMesasOcupadas.Controls.Clear();
                flpMesasReservadas.Controls.Clear();
                CargarMesas();
                AsignarEventoClickAMesasLibres();
                AsignarEventoClickAMesasOcupadas();
                AsignarEventoClickAMesasReservadas();
            }
        }


        //-----------------------------OCUPADAS---------------------------
        private void AsignarEventoClickAMesasOcupadas()
        {
            foreach (Control control in flpMesasOcupadas.Controls)
            {
                if (control is CustomMesa mesa)
                {
                    // Asignar el evento Click al control CustomMesa
                    mesa.Click += Boton_ClickOcupadas;
                }
            }
        }
        private void Boton_ClickOcupadas(object sender, EventArgs e)
        {
            if (sender is CustomMesa mesa)
            {
                PMesa.Id = mesa.Id;
                tabControl1.TabPages.Add(tabPage3);
                tabControl1.TabPages.Remove(tabPage1);
                siguienteTab();

            }
        }

        //-----------------------------RESERVADAS--------------------------
        private void AsignarEventoClickAMesasReservadas()
        {
            foreach (Control control in flpMesasReservadas.Controls)
            {
                if (control is CustomMesa mesa)
                {
                    // Asignar el evento Click al control CustomMesa
                    mesa.Click += Boton_ClickReservadas;
                }
            }
        }
        private void Boton_ClickReservadas(object sender, EventArgs e)
        {
            if (sender is CustomMesa mesa)
            {
                PMesa.Id = mesa.Id;
                DialogResult result = MessageBox.Show(
                "¿Estás seguro de ocupar esta mesa?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                // Verificar la respuesta del usuario
                if (result == DialogResult.Yes)
                {
                    datos.OcuparMesa(PMesa.Id);
                    MessageBox.Show("La mesa ha sido ocupada.");
                }
                else
                {
                    // Acción si el usuario hace clic en "No"
                    MessageBox.Show("Operación cancelada.");
                }
                flpMesasLibres.Controls.Clear();
                flpMesasOcupadas.Controls.Clear();
                flpMesasReservadas.Controls.Clear();
                CargarMesas();
                AsignarEventoClickAMesasLibres();
                AsignarEventoClickAMesasOcupadas();
                AsignarEventoClickAMesasReservadas();
            }
        }

        //-------------------------------------------------------------------
        private void btnMesasLibres_Click(object sender, EventArgs e)
        {
            if (flpMesasLibres.Visible == false) { flpMesasLibres.Visible = true; }
            flpMesasOcupadas.Visible = false;
            flpMesasReservadas.Visible = false;
        }

        private void btnMesasOcupadas_Click(object sender, EventArgs e)
        {
            if (flpMesasOcupadas.Visible == false) { flpMesasOcupadas.Visible = true; }
            flpMesasLibres.Visible = false;
            flpMesasReservadas.Visible = false;
        }

        private void btnMesasReservadas_Click(object sender, EventArgs e)
        {
            if (flpMesasReservadas.Visible == false) { flpMesasReservadas.Visible = true; }
            flpMesasLibres.Visible = false;
            flpMesasOcupadas.Visible = false;
        }
    }
}
