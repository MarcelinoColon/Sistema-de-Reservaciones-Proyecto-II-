using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

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

    }
}
