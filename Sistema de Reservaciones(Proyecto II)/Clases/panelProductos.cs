using Sistema_de_Reservaciones_Proyecto_II_.Formularios;
using System;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{
    public class panelProductos : Panel
    {
        // Cambiar la visibilidad de estos controles a public
        public Label labelDescripcion;
        public Label labelPrecio;
        public PictureBox pictureBox;

        public int TagValor { get; set; }

        public panelProductos(int tagValor, string descripcion, decimal precio, string imagenPath)
        {
            this.Size = new System.Drawing.Size(208, 122);

            pictureBox = new PictureBox
            {
                Size = new System.Drawing.Size(111, 122),
                Dock = DockStyle.Left,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            try
            {
                if (!string.IsNullOrEmpty(imagenPath))
                    pictureBox.ImageLocation = imagenPath;
            }
            catch
            {
                MessageBox.Show($"Error al cargar la imagen: {imagenPath}");
            }

            labelDescripcion = new Label
            {
                Size = new System.Drawing.Size(82, 82),
                MaximumSize = new System.Drawing.Size(82, 82),
                Location = new System.Drawing.Point(120, 0),
                Text = descripcion,
                Font = new System.Drawing.Font("Arial Rounded MT Bold", 8f)
            };

            labelPrecio = new Label
            {
                Size = new System.Drawing.Size(43, 18),
                MaximumSize = new System.Drawing.Size(43, 18),
                Location = new System.Drawing.Point(120, 82),
                Text = $"{precio:0.00}",
                Font = new System.Drawing.Font("Arial Rounded MT Bold", 8f)
            };

            this.Tag = tagValor;

            this.Controls.Add(pictureBox);
            this.Controls.Add(labelDescripcion);
            this.Controls.Add(labelPrecio);

            this.Click += (sender, e) => OnPanelClick();
        }

        public void OnPanelClick()
        {
            // Cuando se hace clic en el panel, se guarda el Tag en la variable global
            MesasForm.idmenu = (int)this.Tag;  // Actualiza el id del menú
        }
    }
}
