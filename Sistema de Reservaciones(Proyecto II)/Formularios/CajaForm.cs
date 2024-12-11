using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
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
        int Mesa;
        MostrarDatos objOrdenes = new MostrarDatos();
        List<CheckBox> checkBoxes = new List<CheckBox> { };

        public CajaForm()
        {
            InitializeComponent();
            this.Text = "Caja";
            checkSilla1.CheckedChanged += checkSilla_CheckedChanged;
            checkSilla2.CheckedChanged += checkSilla_CheckedChanged;
            checkSilla3.CheckedChanged += checkSilla_CheckedChanged;
            checkSilla4.CheckedChanged += checkSilla_CheckedChanged;
            checkBoxes.Add(checkSilla1);
            checkBoxes.Add(checkSilla2);
            checkBoxes.Add(checkSilla3);
            checkBoxes.Add(checkSilla4);
            checkSilla1.Visible=false;
            checkSilla2.Visible = false;
            checkSilla3.Visible = false;
            checkSilla4.Visible = false;
        }


        private void CajaForm_Load(object sender, EventArgs e)
        {
            MostrarMesas();
        }
        private void checkSilla_CheckedChanged(object sender, EventArgs e)
        {
            // Crear una lista de enteros para las sillas seleccionadas
            List<int> sillasSeleccionadas = new List<int>();

            // Verificar si los CheckBox están marcados y agregar las sillas correspondientes a la lista
            if (checkSilla1.Checked) sillasSeleccionadas.Add(1);
            if (checkSilla2.Checked) sillasSeleccionadas.Add(2);
            if (checkSilla3.Checked) sillasSeleccionadas.Add(3);
            if (checkSilla4.Checked) sillasSeleccionadas.Add(4);

            // Convertir la lista de sillas seleccionadas a una cadena separada por comas
            string sillasString = string.Join(",", sillasSeleccionadas);

            // Asegurarse de que se ha seleccionado al menos una silla
            if (sillasSeleccionadas.Count > 0)
            {
                // Obtener el valor de la mesa seleccionada
                int mesaSeleccionada = Convert.ToInt32(cbMesa.SelectedValue);

                // Llamar al método para mostrar las órdenes, pasando la mesa y las sillas seleccionadas
                MostrarOrdenes(mesaSeleccionada, sillasString);
            }
            else
            {
                // Si no se ha seleccionado ninguna silla, vaciar las órdenes
                dgvOrdenes.DataSource = null;
            }
            CalcularTotalConImpuesto();
        }
        private void MostrarOrdenes(int mesa, string silla)
        {
            MostrarDatos objOrdenes = new MostrarDatos();
            ColumnaData();
            dgvOrdenes.DataSource = objOrdenes.MostrarOrdenes(mesa, silla);
        }
        private void MostrarMesas()
        {
            MostrarDatos objMesa = new MostrarDatos();
            cbMesa.DataSource = objMesa.MostrarMesas();
            cbMesa.DisplayMember = "numero_mesa";
            cbMesa.ValueMember = "id_mesa";
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
            string numeroOrden = dgvOrdenes.SelectedRows[0].Cells["Orden"].Value.ToString();
            string fecha = DateTime.Now.ToString("yyyyMMdd_HHmm");

            string directorio = @"C:\Facturas";
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            string nombreArchivo = $"Factura_{numeroOrden}_{fecha}.txt";
            string rutaArchivo = Path.Combine(directorio, nombreArchivo);

            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                // Encabezado
                sw.WriteLine("********************************");
                sw.WriteLine("  Sistema de Reservaciones ");
                sw.WriteLine(" Calle Falsa 123, Ciudad Fict. ");
                sw.WriteLine("  Tel: (809) 555-1234          ");
                sw.WriteLine("********************************");
                sw.WriteLine($"Factura N.º: {numeroOrden}");
                sw.WriteLine($"Fecha: {DateTime.Now.ToShortDateString()}");
                sw.WriteLine($"Hora: {DateTime.Now.ToShortTimeString()}");
                sw.WriteLine("--------------------------------");

                // Encabezados de columnas
                sw.WriteLine("Producto          Qty   Precio   Total");
                sw.WriteLine("--------------------------------");

                // Variables para totales
                decimal subtotal = 0;

                // Detalles de productos
                foreach (DataGridViewRow row in dgvOrdenes.SelectedRows)
                {
                    string producto = row.Cells["producto"].Value.ToString();
                    int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);
                    decimal precioUnitario = Convert.ToDecimal(row.Cells["Precio"].Value);
                    decimal precioTotal = Convert.ToDecimal(row.Cells["Total"].Value);

                    // Dividir el nombre del producto si es muy largo
                    int maxLength = 16;  // Longitud máxima por línea
                    while (producto.Length > maxLength)
                    {
                        // Imprimir la primera parte del nombre
                        sw.WriteLine($"{producto.Substring(0, maxLength),-16} {cantidad,3}  {precioUnitario,7:C}  {precioTotal,7:C}");

                        // Eliminar la parte ya impresa y continuar con la siguiente
                        producto = producto.Substring(maxLength);
                    }
                    if (producto.Length > 0)
                    {
                        // Imprimir el resto del nombre
                        sw.WriteLine($"{producto,-16} {cantidad,3}  {precioUnitario,7:C}  {precioTotal,7:C}");
                    }

                    subtotal += precioTotal;
                }

                // Calcular impuestos y totales
                decimal impuesto = subtotal * 0.18m; // 18% ITBIS
                decimal propina = subtotal * 0.10m; // 10% Propina
                decimal total = subtotal + impuesto + propina;

                sw.WriteLine("--------------------------------");
                sw.WriteLine($"Subtotal:                  {subtotal,7:C}");
                sw.WriteLine($"ITBIS (18%):               {impuesto,7:C}");
                sw.WriteLine($"Propina (10%):             {propina,7:C}");
                sw.WriteLine($"Total:                     {total,7:C}");
                sw.WriteLine("********************************");
                sw.WriteLine("Gracias por su visita. ¡Vuelva pronto!");
            }

            MessageBox.Show($"Factura guardada exitosamente en: {rutaArchivo}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            GuardarFacturaComoTxt();
        }

        private void cbMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMesa.SelectedValue != null && int.TryParse(cbMesa.SelectedValue.ToString(), out int idMesa))
            {
                Mesa = idMesa;
                checkSilla1.Checked = false;
                checkSilla2.Checked = false;
                checkSilla3.Checked = false;
                checkSilla4.Checked = false;
                MostrarDatos objDetallesOrdenes = new MostrarDatos();
                objDetallesOrdenes.VerificarSillasPendientes(idMesa, checkBoxes);
                dgvOrdenes.DataSource = null;
            }
        }
        private void ColumnaData()
        {
            // Desactiva la generación automática de columnas
            dgvOrdenes.AutoGenerateColumns = false;

            // Limpia cualquier columna previa
            dgvOrdenes.Columns.Clear();

            // Configura columnas manualmente
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Orden", DataPropertyName = "Orden", Width = 82 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Silla", DataPropertyName = "Silla", Width = 82 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Mesa", DataPropertyName = "Mesa", Width = 82 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", DataPropertyName = "Fecha", Width = 82 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Hora", DataPropertyName = "Hora", Width = 82 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", DataPropertyName = "Producto", Width = 250 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", DataPropertyName = "Cantidad", Width = 100 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Precio", DataPropertyName = "Precio", Width = 100 });
            dgvOrdenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", DataPropertyName = "Total", Width = 100 });

        }
        private void CalcularTotalConImpuesto()
        {
            decimal sumaTotal = 0;

            // Itera sobre las filas del DataGridView
            foreach (DataGridViewRow fila in dgvOrdenes.Rows)
            {
                // Verifica que la fila no sea nueva y que la columna no esté vacía
                if (fila.Cells["Total"].Value != null && decimal.TryParse(fila.Cells["Total"].Value.ToString(), out decimal valor))
                {
                    sumaTotal += valor;
                }
            }

            // Aplica el impuesto del 18% y muestra el resultado en el TextBox
            tbImpuestos.Text = (sumaTotal * 0.28m).ToString("0.00");
            tbSubTotal.Text = sumaTotal.ToString("0.00");
            tbTotal.Text = (sumaTotal * 1.28m).ToString("0.00");
        }

    }
}
