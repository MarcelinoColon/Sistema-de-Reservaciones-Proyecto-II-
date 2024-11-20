using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    public class Ordenes
    {
        public int IdMesa { get; set; }
        public string Silla { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Estado { get; set; }
        public int IdMenu { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Producto { get; set; }

        public Ordenes() { }

        public Ordenes(int idMesa, string silla, DateTime fecha, DateTime hora, string estado, int idMenu, int cantidad, decimal precio, string producto)
        {
            IdMesa = idMesa;
            Silla = silla;
            Fecha = fecha;
            Hora = hora;
            Estado = estado;
            IdMenu = idMenu;
            Cantidad = cantidad;
            Precio = precio;
            Producto = producto;
        }
    }

}
