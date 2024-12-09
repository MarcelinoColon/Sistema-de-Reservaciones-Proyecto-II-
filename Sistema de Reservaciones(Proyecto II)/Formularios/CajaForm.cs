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
