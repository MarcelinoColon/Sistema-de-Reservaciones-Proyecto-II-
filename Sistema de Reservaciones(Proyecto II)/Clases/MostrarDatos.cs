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
        public DataTable MostrarSolicitudes()
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select * from Solicitudes";
            LeerComando = comando.ExecuteReader();
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
        public DataTable MostrarMesasDisponibles(string fecha)
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_ObtenerMesasDisponibles";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@fecha_reserva", fecha);
            LeerComando = comando.ExecuteReader();
            Tabla.Load(LeerComando);
            LeerComando.Close();
            conexion.CerrarConexion();
            return Tabla;
        }
        public DataTable MostrarOrdenes(int idMesa, string sillasString)
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_ObtenerOrdenes";
            comando.CommandType = CommandType.StoredProcedure;

            // Parámetros del procedimiento almacenado
            comando.Parameters.AddWithValue("@sillas", sillasString);  // Parámetro con las sillas seleccionadas
            comando.Parameters.AddWithValue("@mesa", idMesa);  // El parámetro de la mesa, por ejemplo

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
        public int ContarMesas()
        {
            int totalMesas = 0;

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "SELECT COUNT(*) FROM Mesa";
                comando.CommandType = CommandType.Text;

                totalMesas = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al contar las mesas: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return totalMesas;
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
        public bool ExisteOrdenPendiente(int idMesa, int silla)
        {
            bool existePendiente = false;

            // Usamos la conexión obtenida del método DBGeneral.ObtenerConexion()
            using (SqlConnection conexion = DBGeneral.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM Orden WHERE id_mesa = @idMesa AND silla = @silla AND estado = 'Pendiente'";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idMesa", idMesa);
                cmd.Parameters.AddWithValue("@silla", silla);

                // Ejecutamos la consulta
                int count = (int)cmd.ExecuteScalar();
                existePendiente = count > 0;
            }

            return existePendiente;
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

        //-----------------------MESAS----------------------
        public void OcuparMesa(int idMesa)
        {
            SqlConnection con = conexion.AbrirConexion();
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE Mesa Set estado = 'Ocupada' WHERE id_mesa = @id_mesa", con);
                cmd.Parameters.AddWithValue("@id_mesa", idMesa);
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
        public void ReservarMesa(int idMesa)
        {
            SqlConnection con = conexion.AbrirConexion();
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE Mesa Set estado = 'Reservada' WHERE id_mesa = @id_mesa", con);
                cmd.Parameters.AddWithValue("@id_mesa", idMesa);
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
        public void LiberarMesa(int idMesa)
        {
            SqlConnection con = conexion.AbrirConexion();
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE Mesa Set estado = 'Libre' WHERE id_mesa = @id_mesa", con);
                cmd.Parameters.AddWithValue("@id_mesa", idMesa);
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
        public void VerificarYActualizarOrden(int idMesa)
        {
            using (SqlConnection conexion = DBGeneral.ObtenerConexion())
            {
                try
                {
                    // Consulta para buscar órdenes abiertas en la mesa
                    string querySelect = "SELECT * FROM Orden WHERE estado = 'Abierta' AND id_mesa = @mesa";

                    using (SqlCommand comandoSelect = new SqlCommand(querySelect, conexion))
                    {
                        comandoSelect.Parameters.AddWithValue("@mesa", idMesa);

                        using (SqlDataReader reader = comandoSelect.ExecuteReader())
                        {
                            if (reader.HasRows) // Si hay registros
                            {
                                reader.Close(); // Cerramos el lector para ejecutar otro comando

                                // Actualizar el estado a 'Pendiente'
                                string queryUpdate = "UPDATE Orden SET estado = 'Pendiente' WHERE estado = 'Abierta' AND id_mesa = @mesa";

                                using (SqlCommand comandoUpdate = new SqlCommand(queryUpdate, conexion))
                                {
                                    comandoUpdate.Parameters.AddWithValue("@mesa", idMesa);
                                    int filasAfectadas = comandoUpdate.ExecuteNonQuery();

                                    MessageBox.Show("Ordenes cerradas exitosamente.", "Ordenes cerradas");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No hay órdenes abiertas en esta mesa.", "Sin Órdenes");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error");
                }
            }
        }
        public void VerificarSillasPendientes(int idMesa, List<CheckBox> checkBoxes)
        {
            using (SqlConnection conexion = DBGeneral.ObtenerConexion())
            {
                try
                {
                    // Consulta para buscar órdenes pendientes en la mesa
                    string query = "SELECT silla FROM Orden WHERE estado = 'Pendiente' AND id_mesa = @mesa";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@mesa", idMesa);

                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            // Reiniciar la visibilidad de los CheckBox
                            foreach (var checkBox in checkBoxes)
                            {
                                checkBox.Visible = false; // Inicialmente, todos no visibles
                            }

                            while (reader.Read())
                            {
                                // Obtener el valor de silla
                                int silla = reader.GetInt32(0); // Suponiendo que 'silla' es un INT en la base de datos

                                // Asegurarse de que el índice de la silla corresponde a un CheckBox
                                if (silla >= 1 && silla <= checkBoxes.Count)
                                {
                                    checkBoxes[silla - 1].Visible = true; // Poner visible el CheckBox correspondiente
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error");
                }
            }
        }

    }
}
