using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{
    public class Producto
    {
        public int Id { get; set; } // ID generado automáticamente por la base de datos
        public string NombreProducto { get; set; } // Nombre del producto (texto del botón)
        public decimal Precio { get; set; } // Precio del producto
        public string TipoProducto { get; set; } // Tipo del producto (Desayuno, Almuerzo, etc.)
    }
}
