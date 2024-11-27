using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
    
{
    public static class PData
    {
        // Propiedad estática para almacenar el valor de idmenu
        public static int _idmenu { get; set; }

        // Propiedad privada para controlar el cambio del valor de idmenu
        public static int idmenu
        {
            get { return _idmenu; }
            set
            {
                if (_idmenu != value)  // Solo disparar el evento si el valor cambia
                {
                    _idmenu = value;
                    OnIdMenuChanged(); // Llamar al método que dispara el evento
                }
            }
        }

        // Evento que se dispara cuando idmenu cambia
        public static event Action IdMenuChanged;

        // Método que dispara el evento
        private static void OnIdMenuChanged()
        {
            IdMenuChanged?.Invoke();  // Invocar el evento, actualiza el Label automáticamente
        }

        // Método que actualiza el Label con el nuevo precio
        public static void ActualizarLabelPrecio(Label lbPrecio)
        {
            decimal precio = ObtenerPrecioProducto(_idmenu);
            lbPrecio.Text = precio.ToString("C2"); // Actualizar el texto del Label con el precio
        }

        // Método para obtener el precio desde la base de datos (igual que antes)
        private static decimal ObtenerPrecioProducto(int idMenu)
        {
            decimal precio = 0;

            using (SqlConnection conexion = DBGeneral.ObtenerConexion())
            {
                string consulta = "SELECT precio FROM Menu WHERE id_menu = @id_menu";
                using (var comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id_menu", idMenu);
                    var result = comando.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        precio = Convert.ToDecimal(result);
                    }
                }
            }

            return precio;
        }
    }
}
