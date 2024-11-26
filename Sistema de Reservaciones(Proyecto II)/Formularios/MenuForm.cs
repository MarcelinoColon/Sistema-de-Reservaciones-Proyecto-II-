using Newtonsoft.Json;
using Sistema_de_Reservaciones_Proyecto_II_.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    public partial class MenuForm : Form
    {
        public string idmenu;
        ButtonManager buttonManager = new ButtonManager();

        public MenuForm()
        {
            InitializeComponent();
            this.Text = "Menu";
            CargarBotonesDesdeJson();
        }
        private void CargarBotonesDesdeJson()
        {
            string archivoJson = "ruta_a_tu_archivo_json.json";
            if (File.Exists(archivoJson))
            {
                string json = File.ReadAllText(archivoJson);
                List<CustomButton> botones = JsonConvert.DeserializeObject<List<CustomButton>>(json);

                foreach (var boton in botones)
                {
                    // Cargar los botones en el FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(boton);
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos están completos
            if (string.IsNullOrWhiteSpace(txtProducto.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text) || cbMenu.SelectedItem == null || Imagen.Image == null)
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            // Crear el nuevo producto y el botón
            Producto producto = new Producto
            {
                NombreProducto = txtProducto.Text,
                Precio = decimal.Parse(txtPrecio.Text),
                TipoProducto = cbMenu.SelectedItem.ToString()
            };

            // Guardar el producto en la base de datos
            buttonManager.GuardarProducto(producto);

            // Crear el botón personalizado
            CustomButton boton = new CustomButton(producto.Id.ToString(), producto.NombreProducto, Imagen.ImageLocation);

            // Agregar el botón al FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(boton);

            // Guardar el botón en el archivo JSON
            buttonManager.GuardarBotonEnJson(boton);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de Imagen (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Seleccionar una imagen"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string imagePath = openFileDialog.FileName;
                    Imagen.Image = Image.FromFile(imagePath);
                    Imagen.Tag = imagePath;
                    Imagen.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ClearInputs()
        {
            txtProducto.Clear();
            txtPrecio.Clear();
            cbMenu.Text = "";
            Imagen.Image = null;
            Imagen.Tag = null;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0 || flowLayoutPanel1.Controls.OfType<CustomButton>().All(b => b != SelectedButton))
            {
                MessageBox.Show("Seleccione un botón para eliminar.");
                return;
            }

            // Obtener el ID del botón seleccionado
            string idMenu = SelectedButton.Id;

            // Eliminar el botón del FlowLayoutPanel
            flowLayoutPanel1.Controls.Remove(SelectedButton);

            // Eliminar el botón del archivo JSON
            buttonManager.EliminarBotonEnJson(int.Parse(idMenu));

            // Eliminar el producto de la base de datos
            buttonManager.EliminarProducto(int.Parse(idMenu));
        }
        private CustomButton SelectedButton;
    }
}
