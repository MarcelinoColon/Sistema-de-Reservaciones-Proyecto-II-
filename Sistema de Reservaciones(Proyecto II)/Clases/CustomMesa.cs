using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{
    public class CustomMesa : Button
    {
        // Propiedades del botón
        public int Id { get; set; } // Identificador único
        public string Estado { get; set; }
        public string ImagePath { get; set; } // Ruta de la imagen a cargar

        // Constructor
        public CustomMesa(int id, string numeroMesa  , string estado, string imagePath)
        {
            this.Id = id;
            this.Text = numeroMesa; // Usa la propiedad heredada Text de Button
            this.Estado = estado;
            this.ImagePath = imagePath;
            this.Size = new Size(137, 155); // Tamaño fijo
            this.FlatStyle = FlatStyle.Flat; // Estilo plano
            this.FlatAppearance.BorderSize = 0; // Sin borde
            this.BackColor = Color.White; // Fondo transparente


            // Asignar el evento Paint para personalizar el dibujo
            this.Paint += CustomButton_Paint;
            ImagePath = imagePath;
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
