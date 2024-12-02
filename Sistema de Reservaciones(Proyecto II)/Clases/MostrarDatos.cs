using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{
    class MostrarDatos
    {
        private DBGeneral conexion = new DBGeneral();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader LeerComando;

        //Mostrar ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        public DataTable MostrarReservaciones()
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_ObtenerReservas";
            comando.CommandType = CommandType.StoredProcedure;
            LeerComando= comando.ExecuteReader();
            Tabla.Load(LeerComando);
            LeerComando.Close();
            conexion.CerrarConexion();
            return Tabla;
        }

        public DataTable MostrarClientes()
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT id_cliente, telefono, email, CONCAT(nombre, ' ', apellido) AS nombreCompleto FROM Cliente";
            LeerComando = comando.ExecuteReader();
            Tabla.Load(LeerComando);
            LeerComando.Close();
            conexion.CerrarConexion();
            return Tabla;
        }
        public DataTable MostrarDetallesOrdenes(int mesa, int silla)
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_ObtenerDetalleOrden";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@mesa", mesa);
            comando.Parameters.AddWithValue("@silla", silla);
            LeerComando = comando.ExecuteReader();
            Tabla.Load(LeerComando);
            LeerComando.Close();
            conexion.CerrarConexion();
            return Tabla;
        }
        public DataTable MostrarOrdenes()
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_ObtenerOrdenes";
            comando.CommandType = CommandType.StoredProcedure;
            LeerComando = comando.ExecuteReader();
            Tabla.Load(LeerComando);
            LeerComando.Close();
            conexion.CerrarConexion();
            return Tabla;
        }
        public DataTable MostrarMesas()
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select * from Mesa";
            LeerComando = comando.ExecuteReader();
            Tabla.Load(LeerComando);
            LeerComando.Close();
            conexion.CerrarConexion();
            return Tabla;
        }
        public DataTable MostrarMenu(string query)
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = query;
            LeerComando = comando.ExecuteReader();
            Tabla.Load(LeerComando);
            LeerComando.Close();
            conexion.CerrarConexion();
            return Tabla;
        }
        public DataTable DataMenu(int id)
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "Select * from Menu where id_menu = " +id;
            LeerComando = comando.ExecuteReader();
            Tabla.Load(LeerComando);
            LeerComando.Close();
            conexion.CerrarConexion();
            return Tabla;
        }

        //Insertar ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        //  public void InsertarOrdenes(string silla, int idMesa, int? idEmpleado, DateTime fechaOrden, DateTime horaOrden,string estado, int idMenu, int cantidad)
        //  {
        //      comando.Connection = conexion.AbrirConexion();
        //      comando.CommandText = "sp_InsertarOrden";
        //      comando.CommandType = CommandType.StoredProcedure;
        //    comando.Parameters.AddWithValue("@silla", silla);
        //  comando.Parameters.AddWithValue("@id_mesa ", idMesa);
        //  comando.Parameters.AddWithValue("@id_empleado  ", idEmpleado);
        //  comando.Parameters.AddWithValue("@fecha_orden", fechaOrden);
        //  comando.Parameters.AddWithValue("@hora_orden ", horaOrden);
        //  comando.Parameters.AddWithValue("@estado  ", estado);
        //  comando.Parameters.AddWithValue("@id_menu ", idMenu);
        //  comando.Parameters.AddWithValue("@cantidad  ", cantidad);
        //  comando.ExecuteNonQuery();
        //  comando.Parameters.Clear();
        // }


        //ORDENES
        public int ObtenerOrdenActiva(int idMesa, int silla)
        {
            int idOrden = 0;
            SqlConnection con = conexion.AbrirConexion();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT id_orden FROM Orden WHERE id_mesa = @id_mesa AND silla = @silla AND estado = 'Abierta'", con);
                cmd.Parameters.AddWithValue("@id_mesa", idMesa);
                cmd.Parameters.AddWithValue("@silla", silla);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    idOrden = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al obtener orden activa: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return idOrden;
        }
        public int CrearNuevaOrden(int idMesa, int silla)
        {
            int idOrden = 0;
            SqlConnection con = conexion.AbrirConexion();
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Orden (id_mesa, silla, fecha_orden, hora_orden, estado) OUTPUT INSERTED.id_orden VALUES (@id_mesa, @silla, GETDATE(), GETDATE(), 'Abierta')", con);
                cmd.Parameters.AddWithValue("@id_mesa", idMesa);
                cmd.Parameters.AddWithValue("@silla", silla);

                var result = cmd.ExecuteScalar();  // Ejecutar la consulta y obtener el valor

                // Verificar si el valor no es null o DBNull
                if (result != null && result != DBNull.Value)
                {
                    idOrden = Convert.ToInt32(result);  // Convertir el resultado a int
                }
                else
                {
                    // Manejo del caso cuando ExecuteScalar() devuelve null
                    MessageBox.Show("No se pudo obtener el ID de la orden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al crear nueva orden: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return idOrden;
        }
        public void AgregarDetalleOrden(int idOrden, int idMenu, int cantidad, decimal precioUnitario)
        {
            SqlConnection con = conexion.AbrirConexion();
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO DetalleOrden (id_orden, id_menu, cantidad, precio_unitario) VALUES (@id_orden, @id_menu, @cantidad, @precio_unitario)", con);
                cmd.Parameters.AddWithValue("@id_orden", idOrden);
                cmd.Parameters.AddWithValue("@id_menu", idMenu);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@precio_unitario", precioUnitario);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al agregar detalle a la orden: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
        public void ActualizarOrden(int idOrden)
        {
            SqlConnection con = conexion.AbrirConexion();
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE Orden Set estado = 'Pendiente' WHERE id_orden = @id_orden", con);
                cmd.Parameters.AddWithValue("@id_orden", idOrden);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al agregar detalle a la orden: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
