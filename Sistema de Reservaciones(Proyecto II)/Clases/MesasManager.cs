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
    public class MesasManager
    {
        DBGeneral conexion = new DBGeneral();
        public void GuardarMesa(Mesa mesa)
        {
            using (SqlConnection con = DBGeneral.ObtenerConexion())
            {
                var comando = new SqlCommand("INSERT INTO Mesa (numero_mesa, capacidad) VALUES (@NumeroMesa, @Capacidad); Select SCOPE_IDENTITY();", conexion.AbrirConexion());
                SqlParameter sqlParameter = 
                comando.Parameters.AddWithValue("@NumeroMesa", mesa.NumeroMesa);
                comando.Parameters.AddWithValue("@Capacidad", mesa.Capacidad);
                mesa.Id = Convert.ToInt32(comando.ExecuteScalar());
            }
        }
        public void GuardarMesaEnJson(CustomMesa mesa)
        {
            string archivoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mesas.json");

            List<MesaData> mesas = new List<MesaData>();

            if (File.Exists(archivoJson))
            {
                string jsonExistente = File.ReadAllText(archivoJson);
                mesas = JsonConvert.DeserializeObject<List<MesaData>>(jsonExistente);
            }

            // Convertir CustomButton a ButtonData
            MesaData mesaData = new MesaData
            {
                Id = mesa.Id,
                NumeroMesa = mesa.Text,
                Estado = mesa.Estado,
                ImagePath = mesa.ImagePath // Asegúrate de usar ImagePath correctamente
            };

            mesas.Add(mesaData);

            string json = JsonConvert.SerializeObject(mesas, Formatting.Indented);
            File.WriteAllText(archivoJson, json);
        }
        public void EliminarMesa(int idMesa)
        {
            using (SqlConnection con = DBGeneral.ObtenerConexion())
            {
                var comando = new SqlCommand("DELETE FROM Mesa WHERE id_mesa = @id_mesa", conexion.AbrirConexion());
                comando.Parameters.AddWithValue("@id_mesa", idMesa); // Asegúrate que el parámetro coincida con el nombre en la consulta SQL.

                comando.ExecuteNonQuery(); // Ejecuta la eliminación
            }
        }
        public void EliminarMesaEnJson(int idMesa)
        {
            string archivoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mesas.json");

            if (File.Exists(archivoJson))
            {
                // Leer el contenido del archivo JSON
                string jsonExistente = File.ReadAllText(archivoJson);

                // Deserializar el JSON en una lista de botones
                List<MesaData> botonesMesa = JsonConvert.DeserializeObject<List<MesaData>>(jsonExistente);

                // Buscar y eliminar el botón con el Id correspondiente
                botonesMesa.RemoveAll(b => b.Id == idMesa);  // Compara el idMenu con el Id del botón, asegurándose de que ambos sean cadenas

                // Serializar la lista modificada de botones y guardar de nuevo el archivo JSON
                string json = JsonConvert.SerializeObject(botonesMesa, Formatting.Indented);
                File.WriteAllText(archivoJson, json);
            }
        }

        public void CargarMesasDesdeJson(FlowLayoutPanel flowLayoutPanel, List<string> descripciones)
        {
            string archivoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mesas.json");

            if (File.Exists(archivoJson))
            {
                string json = File.ReadAllText(archivoJson);
                List<MesaData> mesas = JsonConvert.DeserializeObject<List<MesaData>>(json);

                // Obtener los estados actualizados de la base de datos
                Dictionary<int, string> estadosActualizados = ObtenerEstadosActualizados();

                // Actualizar el estado de las mesas cargadas desde el JSON
                foreach (var mesaData in mesas)
                {
                    if (estadosActualizados.TryGetValue(mesaData.Id, out string estadoActualizado))
                    {
                        mesaData.Estado = estadoActualizado; // Actualiza el estado de la mesa
                    }
                }

                // Filtrar los botones que coincidan con alguna descripción en la lista
                var mesasFiltradas = mesas.Where(b => descripciones.Contains(b.Estado, StringComparer.OrdinalIgnoreCase));

                foreach (var mesaData in mesasFiltradas)
                {
                    // Crear un nuevo CustomButton
                    CustomMesa mesa = new CustomMesa(mesaData.Id, mesaData.NumeroMesa, mesaData.Estado, mesaData.ImagePath);

                    // Agregarlo al FlowLayoutPanel
                    flowLayoutPanel.Controls.Add(mesa);
                }
            }
        }

        private Dictionary<int, string> ObtenerEstadosActualizados()
        {
            var estados = new Dictionary<int, string>();

            using (SqlConnection con = DBGeneral.ObtenerConexion())
            {
                string query = "SELECT id_mesa, estado FROM Mesa";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string estado = reader.GetString(1);
                            estados[id] = estado;
                        }
                    }
                }
            }

            return estados;
        }

    }
}
