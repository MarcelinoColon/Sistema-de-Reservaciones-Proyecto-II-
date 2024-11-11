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

    }
}
