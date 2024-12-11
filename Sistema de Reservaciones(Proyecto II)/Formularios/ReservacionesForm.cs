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
    public partial class ReservacionesForm : Form
    {
        string Fecha;
        public ReservacionesForm()
        {
            InitializeComponent();
            this.Text = "Reservaciones";
            tabControl1.TabPages.Remove(Cliente);
            tabControl1.TabPages.Remove(Solicitudes);
        }

        private void ReservacionesForm_Load(object sender, EventArgs e)
        {
            MostrarClientes();
            MostrarSolicitudes();
        }
        private void MostrarSolicitudes()
        {
            MostrarDatos objSolicitudes = new MostrarDatos();
            dgvSolicitudes.DataSource = objSolicitudes.MostrarSolicitudes();
        }

        private void MostrarClientes()
        {
            MostrarDatos objClientes = new MostrarDatos();
            ColumnasClientes();
            dgvClientes.DataSource = objClientes.MostrarClientes();
        }
        private void MostrarMesasDisponibles(string fecha)
        {
            MostrarDatos objClientes = new MostrarDatos();
            cbMesas.DataSource = objClientes.MostrarMesasDisponibles(fecha);
            cbMesas.DisplayMember = "numero_mesa";
            cbMesas.ValueMember = "id_mesa";
        }

        private void MostrarReservaciones()
        {
            MostrarDatos objReservaciones = new MostrarDatos();
            dgvReservaciones.DataSource = objReservaciones.MostrarReservaciones();
        }
        private void ColumnasClientes()
        {
            // Desactiva la generación automática de columnas
            dgvClientes.AutoGenerateColumns = false;

            // Limpia cualquier columna previa
            dgvClientes.Columns.Clear();

            // Configura columnas manualmente
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "ID", DataPropertyName = "id_cliente", Width = 50, Visible = false }); // Oculta esta columna
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "telefono", DataPropertyName = "telefono", Width = 200, Visible = false });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "email", DataPropertyName = "email", Width = 100, Visible = false });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cliente", DataPropertyName = "nombreCompleto", Width = 208 });
        }

        private void tbFiltroCliente_TextChanged(object sender, EventArgs e)
        {
            if (dgvClientes.DataSource is DataTable dataTable)
            {
                string filtro = tbFiltroCliente.Text.Trim();

                if (string.IsNullOrEmpty(filtro))
                {
                    // Muestra todos los datos si el filtro está vacío
                    dataTable.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    // Filtra los datos por el campo "nombreCompleto"
                    dataTable.DefaultView.RowFilter = $"nombreCompleto LIKE '%{filtro}%'";
                }
            }
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(Cliente);
            tabControl1.TabPages.Remove(Reservaciones);
            dgvClientes.ClearSelection();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(Reservaciones);
            tabControl1.TabPages.Remove(Cliente);
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica que no sea un encabezado o área inválida
            {
                // Obtén la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvClientes.Rows[e.RowIndex];

                // Asigna los valores a los TextBox
                txtNombre.Text = filaSeleccionada.Cells["Cliente"].Value?.ToString().Split(' ')[0]; // Toma solo el nombre
                txtApellido.Text = filaSeleccionada.Cells["Cliente"].Value?.ToString().Split(' ')[1]; // Toma el apellido
                txtTelefono.Text = filaSeleccionada.Cells["telefono"].Value?.ToString();
                txtEmail.Text = filaSeleccionada.Cells["email"].Value?.ToString();
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = dateTimePicker3.Value;
            Fecha = fechaSeleccionada.ToString("yyyy-MM-dd");
            MostrarMesasDisponibles(Fecha);
            cbMesas.Text = "";
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {

        }

        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(Solicitudes);
            tabControl1.TabPages.Remove(Reservaciones);
        }

        private void btnSatras_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(Reservaciones);
            tabControl1.TabPages.Remove(Solicitudes);
        }
    }
}
