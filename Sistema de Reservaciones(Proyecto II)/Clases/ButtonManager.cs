using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{

    public class ButtonManager
    {
        DBGeneral conexion = new DBGeneral();
        public void GuardarProducto(Producto producto)
        {
            using (SqlConnection con = DBGeneral.ObtenerConexion())
            {
                var comando = new SqlCommand("INSERT INTO Menu (nombre_producto, precio, tipo_producto) VALUES (@NombreProducto, @Precio, @TipoProducto); Select SCOPE_IDENTITY();", conexion.AbrirConexion());
                comando.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.Parameters.AddWithValue("@TipoProducto", producto.TipoProducto);
                producto.Id = Convert.ToInt32(comando.ExecuteScalar());
            }
        }
        public void GuardarBotonEnJson(CustomButton boton)
        {
            string archivoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "archivo.json");

            List<ButtonData> botones = new List<ButtonData>();

            if (File.Exists(archivoJson))
            {
                string jsonExistente = File.ReadAllText(archivoJson);
                botones = JsonConvert.DeserializeObject<List<ButtonData>>(jsonExistente);
            }

            // Convertir CustomButton a ButtonData
            ButtonData botonData = new ButtonData
            {
                Id = boton.Id,
                Text = boton.Text,
                Menu = boton.Menu,
                ImagePath = boton.ImagePath // Asegúrate de usar ImagePath correctamente
            };

            botones.Add(botonData);

            string json = JsonConvert.SerializeObject(botones, Formatting.Indented);
            File.WriteAllText(archivoJson, json);
        }
        public void EliminarProducto(int idMenu)
        {
            using (SqlConnection con = DBGeneral.ObtenerConexion())
            {
                var comando = new SqlCommand("DELETE FROM Menu WHERE id_menu = @id_menu", conexion.AbrirConexion());
                comando.Parameters.AddWithValue("@id_menu", idMenu); // Asegúrate que el parámetro coincida con el nombre en la consulta SQL.

                comando.ExecuteNonQuery(); // Ejecuta la eliminación
            }
        }
        public void EliminarBotonEnJson(int idMenu)
        {
            string archivoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "archivo.json");

            if (File.Exists(archivoJson))
            {
                // Leer el contenido del archivo JSON
                string jsonExistente = File.ReadAllText(archivoJson);

                // Deserializar el JSON en una lista de botones
                List<ButtonData> botones = JsonConvert.DeserializeObject<List<ButtonData>>(jsonExistente);

                // Buscar y eliminar el botón con el Id correspondiente
                botones.RemoveAll(b => b.Id == idMenu);  // Compara el idMenu con el Id del botón, asegurándose de que ambos sean cadenas

                // Serializar la lista modificada de botones y guardar de nuevo el archivo JSON
                string json = JsonConvert.SerializeObject(botones, Formatting.Indented);
                File.WriteAllText(archivoJson, json);
            }
        }
        public void CargarBotonesDesdeJson(FlowLayoutPanel flowLayoutPanel, List<string> descripciones)
        {
            string archivoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "archivo.json");

            if (File.Exists(archivoJson))
            {
                string json = File.ReadAllText(archivoJson);
                List<ButtonData> botones = JsonConvert.DeserializeObject<List<ButtonData>>(json);

                // Filtrar los botones que coincidan con alguna descripción en la lista
                var botonesFiltrados = botones.Where(b => descripciones.Contains(b.Menu, StringComparer.OrdinalIgnoreCase));

                foreach (var botonData in botonesFiltrados)
                {
                    // Crear un nuevo CustomButton
                    CustomButton boton = new CustomButton(botonData.Id, botonData.Text, botonData.Menu, botonData.ImagePath);

                    // Agregarlo al FlowLayoutPanel
                    flowLayoutPanel.Controls.Add(boton);
                }
            }
        }
    }
}
