using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{

    public  class ButtonManager
    {
        DBGeneral conexion = new DBGeneral();
        public void GuardarProducto(Producto producto)
        {
            using (conexion.AbrirConexion())
            {
                var comando = new SqlCommand("INSERT INTO Menu (NombreProducto, Precio, TipoProducto) VALUES (@NombreProducto, @Precio, @TipoProducto)", conexion.AbrirConexion());
                comando.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.Parameters.AddWithValue("@TipoProducto", producto.TipoProducto);

                comando.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
        }
        public void GuardarBotonEnJson(CustomButton boton)
        {
            string archivoJson = "ruta_a_tu_archivo_json.json";

            List<CustomButton> botones = new List<CustomButton>();
            if (File.Exists(archivoJson))
            {
                string jsonExistente = File.ReadAllText(archivoJson);
                botones = JsonConvert.DeserializeObject<List<CustomButton>>(jsonExistente);
            }

            botones.Add(boton);

            string json = JsonConvert.SerializeObject(botones, Formatting.Indented);
            File.WriteAllText(archivoJson, json);
        }
        public void EliminarProducto(int idMenu)
        {
            using (conexion.AbrirConexion())
            {
                var comando = new SqlCommand("DELETE FROM Menu WHERE IdMenu = @IdMenu", conexion.AbrirConexion());
                comando.Parameters.AddWithValue("@IdMenu", idMenu);

                comando.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
        }
        public void EliminarBotonEnJson(int idMenu)
        {
            string archivoJson = "ruta_a_tu_archivo_json.json";

            if (File.Exists(archivoJson))
            {
                string jsonExistente = File.ReadAllText(archivoJson);
                List<CustomButton> botones = JsonConvert.DeserializeObject<List<CustomButton>>(jsonExistente);

                // Buscar y eliminar el botón con el Id correspondiente (antes se usaba IdMenu)
                botones.RemoveAll(b => b.Id == idMenu.ToString());  // Asegúrate de que idMenu sea un string

                // Guardar nuevamente el archivo JSON
                string json = JsonConvert.SerializeObject(botones, Formatting.Indented);
                File.WriteAllText(archivoJson, json);
            }
        }
    }
}
