using Sistema_de_Reservaciones_Proyecto_II_.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    public partial class MenuForm : Form
    {
        private int? tagSeleccionado;  // Usamos nullable int para gestionar la selección de un panel
        private PanelManager panelManager;
        public MenuForm()
        {
            InitializeComponent();
            this.Text = "Menu";
            panelManager = new PanelManager();
            LoadPaneles();
        }
       
        private void LoadPaneles()
        {
            flowLayoutPanel1.Controls.Clear();
            var paneles = panelManager.GetPaneles();

            foreach (var panel in paneles)
            {
                var panelDinamico = new panelProductos(panel.Tag, panel.Descripcion, panel.Precio, panel.ImagenPath);
                panelDinamico.Click += (sender, e) => OnPanelClick(panelDinamico);
                panelDinamico.pictureBox.Click += (sender, e) => panelDinamico.OnPanelClick();
                panelDinamico.labelDescripcion.Click += (sender, e) => panelDinamico.OnPanelClick();

                flowLayoutPanel1.Controls.Add(panelDinamico);
            }
        }
        private void OnPanelClick(panelProductos panel)
        {
            tagSeleccionado = panel.TagValor;
            MesasForm.idmenu = tagSeleccionado.Value;
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    Imagen.Image == null ||
                    cbMenu.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, complete todos los campos y seleccione una imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    MessageBox.Show("El precio ingresado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tipoProducto = cbMenu.SelectedItem.ToString();
                var nuevoPanel = new PanelInfo
                {
                    Tag = new Random().Next(1, 1000),  // Generar Tag único
                    Descripcion = txtDescripcion.Text,
                    Precio = precio,
                    ImagenPath = Imagen.Tag?.ToString(),
                    Menu = tipoProducto
                };

                panelManager.AddPanel(nuevoPanel);
                LoadPaneles();  // Recargar los paneles

                ClearInputs();
                MessageBox.Show("Panel agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el panel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
            txtDescripcion.Clear();
            txtPrecio.Clear();
            cbMenu.Text = "";
            Imagen.Image = null;
            Imagen.Tag = null;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (tagSeleccionado.HasValue)
            {
                panelManager.RemovePanel(tagSeleccionado.Value);
                LoadPaneles(); // Recargar los paneles después de la eliminación
                tagSeleccionado = null; // Reiniciar la selección
                MessageBox.Show("Panel eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay ningún panel seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
