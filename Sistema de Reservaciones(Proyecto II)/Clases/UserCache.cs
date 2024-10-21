using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{
    public class UserCache
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string TipoUsuario { get; set; }

        public static UserCache Current { get; private set; }

        public UserCache(string usuario, string contrasena, string tipoUsuario)
        {
            this.Usuario = usuario;
            this.Contrasena = contrasena;
            this.TipoUsuario = tipoUsuario;
        }

        public static void Initialize(string usuario, string contrasena, string tipoUsuario)
        {
            Current = new UserCache(usuario, contrasena, tipoUsuario);
        }
    }
}
