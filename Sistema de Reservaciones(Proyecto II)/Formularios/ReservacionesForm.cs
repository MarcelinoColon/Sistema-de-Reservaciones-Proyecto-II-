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
        public ReservacionesForm()
        {
            InitializeComponent();
            this.Text = "Reservaciones";
        }

        private void ReservacionesForm_Load(object sender, EventArgs e)
        {
            MostrarClientes();
            MostrarReservaciones();
        }

        private void MostrarClientes()
        {
            MostrarDatos objClientes = new MostrarDatos();
            cbCliente.DataSource = objClientes.MostrarClientes();
            cbCliente.DisplayMember = "nombreCompleto";
            cbCliente.ValueMember = "id_cliente";
        }

        private void MostrarReservaciones()
        {
            MostrarDatos objReservaciones = new MostrarDatos();
            dgvReservaciones.DataSource = objReservaciones.MostrarReservaciones();
        }
    }
}
