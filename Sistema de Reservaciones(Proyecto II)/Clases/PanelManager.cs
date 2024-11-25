using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{
    public class PanelManager
    {
        private List<PanelInfo> paneles;        // Lista de paneles en memoria
        private readonly string filePath;       // Ruta del archivo JSON

        // Constructor
        public PanelManager()
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            filePath = Path.Combine(projectDirectory, "paneles.json"); // Ruta del archivo JSON
            paneles = LoadPaneles(); // Cargar los paneles al iniciar
        }

        // Método para agregar un nuevo panel
        public void AddPanel(PanelInfo panel)
        {
            paneles.Add(panel);      // Agregar panel a la lista
            SavePaneles();           // Guardar la lista en el archivo JSON
        }

        // Método para eliminar un panel por su Tag
        public void RemovePanel(int tag)
        {
            var panelToRemove = paneles.FirstOrDefault(p => p.Tag == tag);
            if (panelToRemove != null)
            {
                paneles.Remove(panelToRemove); // Eliminar panel de la lista
                SavePaneles();                // Guardar la lista actualizada en el archivo JSON
            }
        }

        // Obtener todos los paneles desde la lista en memoria
        public List<PanelInfo> GetPaneles()
        {
            return paneles;
        }

        // Cargar los paneles desde el archivo JSON
        private List<PanelInfo> LoadPaneles()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var panelesDesdeJson = JsonConvert.DeserializeObject<List<PanelInfo>>(json);
                    return panelesDesdeJson ?? new List<PanelInfo>(); // Si es null, devolver una lista vacía
                }
                else
                {
                    return new List<PanelInfo>(); // Si el archivo no existe, devolver una lista vacía
                }
            }
            catch (Exception)
            {
                return new List<PanelInfo>();
            }
        }

        // Guardar los paneles en el archivo JSON
        private void SavePaneles()
        {
            try
            {
                string json = JsonConvert.SerializeObject(paneles, Formatting.Indented);
                File.WriteAllText(filePath, json); // Sobrescribir el archivo JSON con la lista actual
            }
            catch (Exception ex)
            {
                // Manejo de errores
            }
        }
    }
}
