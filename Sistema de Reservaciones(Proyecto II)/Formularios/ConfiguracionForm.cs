﻿using Newtonsoft.Json;
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
    public partial class ConfiguracionForm : Form
    {
        ButtonManager buttonManager = new ButtonManager();
        MesasManager mesasManager = new MesasManager();
        string nombreProducto;
        decimal precio;
        string tipoProducto;

        public ConfiguracionForm()
        {
            InitializeComponent();
            this.Text = "Menu";
            buttonManager.CargarBotonesDesdeJson(flowLayoutPanel1, new List<string> { "Desayuno", "Almuerzo", "Postre", "Bebida" });
            mesasManager.CargarMesasDesdeJson(flowLayoutPanel2, new List<string> { "Libre", "Reservada", "Ocupada" });
            AsignarEventoClickAMesas();
        }
        private void AsignarEventoClickAMesas()
        {
            foreach (Control control in flowLayoutPanel2.Controls)
            {
                if (control is CustomMesa mesa)
                {
                    // Asignar el evento Click al control CustomMesa
                    mesa.Click += Boton_Click;
                }
            }
        }

        // Evento Click asociado a los botones CustomMesa
        private void Boton_Click(object sender, EventArgs e)
        {
            if (sender is CustomMesa mesa)
            {
                PMesa.Id = mesa.Id;
            }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto();
            producto.NombreProducto = txtProducto.Text;
            producto.TipoProducto = cbMenu.SelectedItem.ToString();
            producto.Precio = Convert.ToDecimal(txtPrecio.Text);
            // Validar que todos los campos están completos
            if (string.IsNullOrWhiteSpace(txtProducto.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text) || cbMenu.SelectedItem == null || Imagen.Image == null)
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            // Guardar el producto en la base de datos
            buttonManager.GuardarProducto(producto);

            // Crear el botón personalizado
            CustomButton boton = new CustomButton(producto.Id, producto.NombreProducto, producto.TipoProducto, Imagen.Tag.ToString());

            // Agregar el botón al FlowLayoutPanel
            flowLayoutPanel1.Controls.Add(boton);

            // Guardar el botón en el archivo JSON
            buttonManager.GuardarBotonEnJson(boton);
            MessageBox.Show("Producto guardado exitosamente");
            ClearInputs();
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
            // Verificar si el FlowLayoutPanel tiene botones
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("No hay botones para eliminar.");
                return;
            }

            // Buscar el botón con el Id que coincida con el valor actual de PData.idmenu
            foreach (CustomButton boton in flowLayoutPanel1.Controls.OfType<CustomButton>())
            {
                if (boton.Id == PData.idmenu) // Verificar si el Id del botón coincide con el valor de idmenu
                {
                    // Eliminar el botón del FlowLayoutPanel
                    flowLayoutPanel1.Controls.Remove(boton);

                    // Eliminar el botón del archivo JSON
                    buttonManager.EliminarBotonEnJson(PData.idmenu);

                    // Eliminar el producto de la base de datos
                    buttonManager.EliminarProducto(PData.idmenu);

                    MessageBox.Show("Botón eliminado exitosamente.");
                    return;
                }
            }

            // Si no se encuentra el botón con el Id correspondiente
            MessageBox.Show("No se encontró un botón con el id especificado.");
        }
        private CustomButton SelectedButton;

        private void btnGuardarMesa_Click(object sender, EventArgs e)
        {
            Mesa mesa = new Mesa();
            mesa.NumeroMesa = tbNumeroMesa.Text;
            mesa.Estado = "Libre";
            mesa.Capacidad = 4;
            // Validar que todos los campos están completos

            if (string.IsNullOrWhiteSpace(tbNumeroMesa.Text) || pbMesa.Image == null)
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            // Guardar el producto en la base de datos
            mesasManager.GuardarMesa(mesa);

            // Crear el botón personalizado
            CustomMesa botonmesa = new CustomMesa(mesa.Id,"Mesa "+ mesa.NumeroMesa, mesa.Estado, pbMesa.Tag.ToString());

            // Agregar el botón al FlowLayoutPanel
            flowLayoutPanel2.Controls.Add(botonmesa);

            // Guardar el botón en el archivo JSON
            mesasManager.GuardarMesaEnJson(botonmesa);
            MessageBox.Show("Mesa guardada exitosamente");
        }

        private void btnImagenMesa_Click(object sender, EventArgs e)
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
                    pbMesa.Image = Image.FromFile(imagePath);
                    pbMesa.Tag = imagePath;
                    pbMesa.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminarMesa_Click(object sender, EventArgs e)
        {
            // Verificar si el FlowLayoutPanel tiene botones
            if (flowLayoutPanel2.Controls.Count == 0)
            {
                MessageBox.Show("No hay botones para eliminar.");
                return;
            }

            // Buscar el botón con el Id que coincida con el valor actual de PData.idmenu
            foreach (CustomMesa mesa in flowLayoutPanel2.Controls.OfType<CustomMesa>())
            {
                if (mesa.Id == PMesa.Id) // Verificar si el Id del botón coincide con el valor de idmenu
                {
                    // Eliminar el botón del FlowLayoutPanel
                    flowLayoutPanel2.Controls.Remove(mesa);

                    // Eliminar el botón del archivo JSON
                    mesasManager.EliminarMesa(PMesa.Id);

                    // Eliminar el producto de la base de datos
                    mesasManager.EliminarMesaEnJson(PMesa.Id);

                    MessageBox.Show("Mesa eliminada exitosamente.");
                    return;
                }
            }
        }
    }
}
