using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Clases
{
    public class CustomButton : Button
    {
        // Propiedades del botón
        public string Id { get; set; } // Identificador único
        public string ImagenPath { get; set; } // Ruta de la imagen a cargar

        // Constructor
        public CustomButton(string id, string text, string imagePath)
        {
            this.Id = id;
            this.Text = text; // Usa la propiedad heredada Text de Button
            this.ImagenPath = imagePath;
            this.Size = new Size(137, 155); // Tamaño fijo
            this.FlatStyle = FlatStyle.Flat; // Estilo plano
            this.FlatAppearance.BorderSize = 0; // Sin borde
            this.BackColor = Color.White; // Fondo blanco

            // Asignar el evento Paint para personalizar el dibujo
            this.Paint += CustomButton_Paint;
        }

        // Evento Paint para dibujar el contenido del botón
        private void CustomButton_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(this.BackColor); // Limpiar el fondo

            // Dibujar la imagen (125x125 centrada)
            if (!string.IsNullOrEmpty(ImagenPath) && File.Exists(ImagenPath))
            {
                try
                {
                    Image img = Image.FromFile(ImagenPath);
                    Rectangle imageRect = new Rectangle(
                        (this.Width - 125) / 2, // Centrar horizontalmente
                        5, // Margen superior
                        125,
                        125
                    );
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

            // Dibujar el texto en la parte inferior
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near
            };
            Rectangle textRect = new Rectangle(0, 135, this.Width, this.Height - 135);
            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRect, sf);
        }
    }
}
