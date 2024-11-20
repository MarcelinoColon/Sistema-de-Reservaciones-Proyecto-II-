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
        private int idmesa;
        private string silla;
        private DateTime fecha = DateTime.Now.Date;
        private DateTime hora = DateTime.Today.Add(DateTime.Now.TimeOfDay);
        private string estado = "Pendiente";
        private int idmenu;
        private int cantidad;
        private decimal precio;
        MostrarDatos mostrarDatos = new MostrarDatos();
        private List<Ordenes> lista = new List<Ordenes>();
        private string producto;
        public MesasForm()
        {
            InitializeComponent();
            ocultarTab();
            this.Text = "Mesas";
            pnDesayuno.Visible = false;
            pnAlmuerzo.Visible = false;
            pnPostres.Visible = false;
            pnBebidas.Visible = false;
            pnDescripcion.Visible = false;
            lbCantidad.Text = "1"; 
        }
        private void MostrarEnDataGridView()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Producto");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Precio");
            dt.Columns.Add("Total"); 

            foreach (var orden in lista)
            {
                decimal total = orden.Cantidad * orden.Precio;
                dt.Rows.Add(orden.Producto, orden.Cantidad, orden.Precio, total);
            }

            dgvOrdenes.DataSource = dt;
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

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            siguienteTab();
        }
        private void btnAtrasSillas_Click(object sender, EventArgs e)
        {
            siguienteTab();
        }

        private void btnDesayunos_Click(object sender, EventArgs e)
        {
            if (pnDesayuno.Visible == false) { pnDesayuno.Visible = true; }
            pnAlmuerzo.Visible = false;
            pnPostres.Visible = false;
            pnBebidas.Visible = false;
        }

        private void btnAlmuerzos_Click_1(object sender, EventArgs e)
        {
            if (pnAlmuerzo.Visible == false) { pnAlmuerzo.Visible = true; }
            pnDesayuno.Visible = false;
            pnPostres.Visible = false;
            pnBebidas.Visible = false;
        }

        private void btnPostres_Click_1(object sender, EventArgs e)
        {
            if (pnPostres.Visible == false) { pnPostres.Visible = true; }
            pnDesayuno.Visible = false;
            pnAlmuerzo.Visible = false;
            pnBebidas.Visible = false;
        }

        private void btnBebidas_Click_1(object sender, EventArgs e)
        {
            if (pnBebidas.Visible == false) { pnBebidas.Visible = true; }
            pnDesayuno.Visible = false;
            pnAlmuerzo.Visible = false;
            pnPostres.Visible = false;
        }

        private void iconButton4_Click(object sender, EventArgs e)
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

        private void iconButton5_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lbCantidad .Text, out int currentValue))
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

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            siguienteTab();
            idmesa = 1;
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            siguienteTab();
            silla = "1";
            List<Ordenes> lista = new List<Ordenes>();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            pnDescripcion.Visible = true;
            idmenu = 1;
            DataTable menuData = mostrarDatos.DataMenu(idmenu);
            if (menuData.Rows.Count > 0) 
            {
                DataRow row = menuData.Rows[0];
                precio = Convert.ToDecimal(row["precio"]);
                producto = row["nombre_producto"].ToString();

                lbPrecio.Text = precio.ToString("F2") + "$";
            }
            else
            {
                MessageBox.Show("No se encontraron datos para el ID del menú proporcionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            int cantidad = int.Parse(lbCantidad.Text);
            Ordenes orden = new Ordenes(idmesa, silla, fecha, hora, estado, idmenu, cantidad, precio, producto);
            lista.Add(orden);
            MostrarEnDataGridView();
        }

        private void btnTomarOrden_Click(object sender, EventArgs e)
        {
            foreach (Ordenes orden in lista) 
            {
                mostrarDatos.InsertarOrdenes(orden.Silla, orden.IdMesa,null, orden.Fecha, orden.Hora, orden.Estado, orden.IdMenu, orden.Cantidad);
                MessageBox.Show("Orden tomada correctamente");
                lista.Clear();
            }
        }
    }
}
