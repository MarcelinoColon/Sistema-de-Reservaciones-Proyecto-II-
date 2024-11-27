using Sistema_de_Reservaciones_Proyecto_II_.Formularios;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{
    public class CustomButton : Button
    {
        // Propiedades del botón
        public int Id { get; set; } // Identificador único
        public string ImagePath { get; set; } // Ruta de la imagen a cargar
        public string Menu {  get; set; }

        // Constructor
        public CustomButton(int id, string text, string menu, string imagePath)
        {
            this.Id = id;
            this.Text = text; // Usa la propiedad heredada Text de Button
            this.Menu = menu;
            this.ImagePath = imagePath;
            this.Size = new Size(137, 155); // Tamaño fijo
            this.FlatStyle = FlatStyle.Flat; // Estilo plano
            this.FlatAppearance.BorderSize = 0; // Sin borde
            this.BackColor = Color.White; // Fondo blanco

            this.Click += CustomButton_Click;

            // Asignar el evento Paint para personalizar el dibujo
            this.Paint += CustomButton_Paint;
        }
        private void CustomButton_Click(object sender, EventArgs e)
        {
            // Actualizar idmenu en la clase global PData
            PData.idmenu = this.Id;
        }

        // Evento Paint para dibujar el contenido del botón
        private void CustomButton_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor); // Limpiar el fondo

            // Verificar si la ruta de la imagen es válida
            if (!string.IsNullOrEmpty(ImagePath) && File.Exists(ImagePath))
            {
                try
                {
                    // Cargar la imagen
                    Image img = Image.FromFile(ImagePath);

                    // Mantener la relación de aspecto de la imagen
                    float aspectRatio = (float)img.Width / img.Height;

                    // Definir el área donde queremos aplicar el zoom (125x125)
                    int newWidth = 125;
                    int newHeight = (int)(newWidth / aspectRatio); // Calcular la altura manteniendo la proporción

                    // Si la altura resultante es mayor que 125, ajustamos la altura y recalculamos el ancho
                    if (newHeight > 125)
                    {
                        newHeight = 125;
                        newWidth = (int)(newHeight * aspectRatio);
                    }

                    // Ajuste del rectángulo de la imagen, centrado horizontalmente y margen superior
                    Rectangle imageRect = new Rectangle(
                        (this.Width - newWidth) / 2, // Centrar horizontalmente
                        5, // Margen superior
                        newWidth, // Nuevo tamaño ajustado
                        newHeight // Nuevo tamaño ajustado
                    );

                    // Dibujar la imagen escalada (efecto zoom)
                    g.DrawImage(img, imageRect);
                }
                catch (Exception ex)
                {
                    // Manejo de errores en caso de que la imagen no se cargue
                    g.DrawString("Imagen no disponible", this.Font, Brushes.Red, new PointF(10, 10));
                    // Log para depuración (si es necesario)
                    Console.WriteLine($"Error al cargar imagen: {ex.Message}");
                }
            }
            else
            {
                // Si la imagen no se carga, mostrar mensaje de error
                g.DrawString("Imagen no encontrada", this.Font, Brushes.Red, new PointF(10, 10));
            }

            // Dibujar el texto en la parte inferior
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near
            };
            Rectangle textRect = new Rectangle(0, 135, this.Width, this.Height - 135);
            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRect, sf);
        }

        // Forzar el redibujo del botón cuando cambie la imagen
        public void SetImage(string imagePath)
        {
            ImagePath = imagePath;
            this.Invalidate(); // Redibuja el botón
        }
    }
}
