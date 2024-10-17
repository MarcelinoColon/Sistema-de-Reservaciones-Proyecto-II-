using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{
    public class DBGeneral
    {
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection("workstation id=SistemaReservaciones.mssql.somee.com;packet size=4096;user id=MarcelinoColon_SQLLogin_1;pwd=v92aeu6fej;data source=SistemaReservaciones.mssql.somee.com;persist security info=False;initial catalog=SistemaReservaciones;TrustServerCertificate=True");
            conexion.Open();

            return conexion;
        }
    }


}
