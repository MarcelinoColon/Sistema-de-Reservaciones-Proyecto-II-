using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_de_Reservaciones_Proyecto_II_.Clases;

namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    public partial class CajaForm : Form
    {
        MostrarDatos objOrdenes = new MostrarDatos();
        public CajaForm()
        {
            InitializeComponent();
            this.Text = "Caja";
        }

        private void CajaForm_Load(object sender, EventArgs e)
        {
            MostrarOrdenes();
            MostrarMesas();
            MostrarMenu();
            cbProducto.DataSource = null;

            IniciarTemporizadorFechaHora();
        }
        private void MostrarOrdenes()
        {
            MostrarDatos objOrdenes = new MostrarDatos();
            dgvOrdenes.DataSource = objOrdenes.MostrarOrdenes();
        }
        private void MostrarMesas()
        {
            MostrarDatos objMesa = new MostrarDatos();
            cbMesa.DataSource = objMesa.MostrarMesas();
            cbMesa.DisplayMember = "numero_mesa";
            cbMesa.ValueMember = "id_mesa";
        }
        private void MostrarMenu()
        {
            MostrarDatos objMenu = new MostrarDatos();
            cbMenu.DataSource = objMenu.MostrarMenu("SELECT DISTINCT tipo_producto FROM Menu");
            cbMenu.DisplayMember = "tipo_producto";
            cbMenu.ValueMember = "tipo_producto";
        }

        private void cbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMenu.SelectedValue != null)
            {
                string tipoProductoSeleccionado = cbMenu.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(tipoProductoSeleccionado))
                {
                    MostrarDatos objMenu = new MostrarDatos();

                    switch (tipoProductoSeleccionado)
                    {
                        case "Desayuno":
                            cbProducto.DataSource = objMenu.MostrarMenu("SELECT * FROM Menu WHERE tipo_producto = 'Desayuno'");
                            break;
                        case "Almuerzo":
                            cbProducto.DataSource = objMenu.MostrarMenu("SELECT * FROM Menu WHERE tipo_producto = 'Almuerzo'");
                            break;
                        case "Postre":
                            cbProducto.DataSource = objMenu.MostrarMenu("SELECT * FROM Menu WHERE tipo_producto = 'Postre'");
                            break;
                        case "Bebida":
                            cbProducto.DataSource = objMenu.MostrarMenu("SELECT * FROM Menu WHERE tipo_producto = 'Bebida'");
                            break;
                        default:
                            cbProducto.DataSource = null;
                            break;
                    }

                    cbProducto.DisplayMember = "nombre_producto";
                    cbProducto.ValueMember = "id_menu";
                }
            }
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
        private void GuardarFacturaComoTxt()
        {
            // Asegúrate de que hay filas seleccionadas
            if (dgvOrdenes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona al menos una fila para generar la factura.");
                return;
            }

            // Obtener número de orden y fecha para el nombre del archivo
            string numeroOrden = dgvOrdenes.SelectedRows[0].Cells["NumeroOrden"].Value.ToString();
            string fecha = DateTime.Now.ToString("yyyyMMdd_HHmm"); // Formato único basado en la fecha y hora

            string nombreArchivo = $"Factura_{numeroOrden}_{fecha}.txt";
            string directorio = Path.Combine("C:\\Facturas", nombreArchivo); // Especifica la ruta donde deseas guardar las facturas
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);  // Crea la carpeta si no existe
            }

            // Combina el directorio con el nombre del archivo para obtener la ruta completa
            string rutaArchivo = Path.Combine(directorio, nombreArchivo);

            // Crear el archivo de texto
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                sw.WriteLine($"Factura para la Orden #{numeroOrden}");
                sw.WriteLine($"Fecha: {fecha}");
                sw.WriteLine("----------------------------");

                // Recorrer solo las filas seleccionadas en el DataGridView
                foreach (DataGridViewRow row in dgvOrdenes.SelectedRows)
                {
                    sw.WriteLine($"Cliente: {row.Cells["cliente"].Value}");
                    sw.WriteLine($"Mesa: {row.Cells["mesa"].Value}");
                    sw.WriteLine($"Fecha: {row.Cells["fecha"].Value}");
                    sw.WriteLine($"Hora: {row.Cells["hora"].Value}");
                    sw.WriteLine($"Producto: {row.Cells["producto"].Value}");
                    sw.WriteLine($"Cantidad: {row.Cells["cantidad"].Value}");
                    sw.WriteLine($"Precio Unitario: {row.Cells["preciounitario"].Value}");
                    sw.WriteLine($"Precio Total: {row.Cells["preciototal"].Value}");
                    sw.WriteLine("----------------------------");
                }
            }

            MessageBox.Show("Factura guardada exitosamente.");
            ImprimirFactura(rutaArchivo);
        }
        private void ImprimirFactura(string rutaArchivo)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) => PrintFacturaPage(sender, e, rutaArchivo);

            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private void PrintFacturaPage(object sender, PrintPageEventArgs e, string rutaArchivo)
        {
            // Leer el contenido del archivo de texto
            string facturaContenido = File.ReadAllText(rutaArchivo);
            Font font = new Font("Arial", 12);
            float yPos = e.MarginBounds.Top;
            int lineHeight = (int)font.GetHeight(e.Graphics);

            // Dividir el texto en líneas
            string[] lines = facturaContenido.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                e.Graphics.DrawString(line, font, Brushes.Black, e.MarginBounds.Left, yPos);
                yPos += lineHeight;

                // Verificar si hay espacio para la próxima línea
                if (yPos + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    break;
                }
            }
        }



        private void iconButton2_Click(object sender, EventArgs e)
        {
            objOrdenes.InsertarOrdenes(txtCliente.Text,
            Convert.ToInt32(cbMesa.SelectedValue),null, 
            DateTime.Now.Date, 
            DateTime.Today.Add(DateTime.Now.TimeOfDay), 
            "Pendiente", Convert.ToInt32(cbProducto.SelectedValue), 
            Convert.ToInt32(cbCantidad.Text));
            MessageBox.Show("Producto agregado correctamente");
            MostrarOrdenes();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            GuardarFacturaComoTxt();
        }
    }
}
