namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbMenu = new System.Windows.Forms.ComboBox();
            this.rutaImagen = new FontAwesome.Sharp.IconButton();
            this.btnEliminar = new FontAwesome.Sharp.IconButton();
            this.Imagen = new System.Windows.Forms.PictureBox();
            this.btnAgregar = new FontAwesome.Sharp.IconButton();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.txtProducto = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnImagenMesa = new FontAwesome.Sharp.IconButton();
            this.pbMesa = new System.Windows.Forms.PictureBox();
            this.btnEliminarMesa = new FontAwesome.Sharp.IconButton();
            this.btnGuardarMesa = new FontAwesome.Sharp.IconButton();
            this.tbNumeroMesa = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagen)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMesa)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(980, 680);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbMenu);
            this.tabPage1.Controls.Add(this.rutaImagen);
            this.tabPage1.Controls.Add(this.btnEliminar);
            this.tabPage1.Controls.Add(this.Imagen);
            this.tabPage1.Controls.Add(this.btnAgregar);
            this.tabPage1.Controls.Add(this.txtPrecio);
            this.tabPage1.Controls.Add(this.txtProducto);
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(972, 647);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbMenu
            // 
            this.cbMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMenu.FormattingEnabled = true;
            this.cbMenu.Items.AddRange(new object[] {
            "Desayuno",
            "Almuerzo",
            "Postre",
            "Bebida"});
            this.cbMenu.Location = new System.Drawing.Point(707, 296);
            this.cbMenu.Name = "cbMenu";
            this.cbMenu.Size = new System.Drawing.Size(157, 40);
            this.cbMenu.TabIndex = 9;
            // 
            // rutaImagen
            // 
            this.rutaImagen.IconChar = FontAwesome.Sharp.IconChar.None;
            this.rutaImagen.IconColor = System.Drawing.Color.Black;
            this.rutaImagen.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.rutaImagen.Location = new System.Drawing.Point(707, 146);
            this.rutaImagen.Name = "rutaImagen";
            this.rutaImagen.Size = new System.Drawing.Size(157, 31);
            this.rutaImagen.TabIndex = 8;
            this.rutaImagen.Text = "Seleccionar imagen";
            this.rutaImagen.UseVisualStyleBackColor = true;
            this.rutaImagen.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnEliminar.IconColor = System.Drawing.Color.Black;
            this.btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEliminar.Location = new System.Drawing.Point(804, 538);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(140, 31);
            this.btnEliminar.TabIndex = 6;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // Imagen
            // 
            this.Imagen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Imagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Imagen.Location = new System.Drawing.Point(707, 47);
            this.Imagen.Name = "Imagen";
            this.Imagen.Size = new System.Drawing.Size(157, 93);
            this.Imagen.TabIndex = 4;
            this.Imagen.TabStop = false;
            // 
            // btnAgregar
            // 
            this.btnAgregar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnAgregar.IconColor = System.Drawing.Color.Black;
            this.btnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAgregar.Location = new System.Drawing.Point(619, 538);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(140, 31);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Guardar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // txtPrecio
            // 
            this.txtPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecio.Location = new System.Drawing.Point(707, 365);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(157, 39);
            this.txtPrecio.TabIndex = 2;
            // 
            // txtProducto
            // 
            this.txtProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProducto.Location = new System.Drawing.Point(707, 221);
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.Size = new System.Drawing.Size(157, 39);
            this.txtProducto.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(600, 635);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbNumeroMesa);
            this.tabPage2.Controls.Add(this.btnEliminarMesa);
            this.tabPage2.Controls.Add(this.btnGuardarMesa);
            this.tabPage2.Controls.Add(this.btnImagenMesa);
            this.tabPage2.Controls.Add(this.pbMesa);
            this.tabPage2.Controls.Add(this.flowLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(972, 647);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 6);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(600, 635);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // btnImagenMesa
            // 
            this.btnImagenMesa.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnImagenMesa.IconColor = System.Drawing.Color.Black;
            this.btnImagenMesa.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnImagenMesa.Location = new System.Drawing.Point(709, 274);
            this.btnImagenMesa.Name = "btnImagenMesa";
            this.btnImagenMesa.Size = new System.Drawing.Size(157, 31);
            this.btnImagenMesa.TabIndex = 10;
            this.btnImagenMesa.Text = "Seleccionar imagen";
            this.btnImagenMesa.UseVisualStyleBackColor = true;
            this.btnImagenMesa.Click += new System.EventHandler(this.btnImagenMesa_Click);
            // 
            // pbMesa
            // 
            this.pbMesa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbMesa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbMesa.Location = new System.Drawing.Point(709, 160);
            this.pbMesa.Name = "pbMesa";
            this.pbMesa.Size = new System.Drawing.Size(157, 93);
            this.pbMesa.TabIndex = 9;
            this.pbMesa.TabStop = false;
            // 
            // btnEliminarMesa
            // 
            this.btnEliminarMesa.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnEliminarMesa.IconColor = System.Drawing.Color.Black;
            this.btnEliminarMesa.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEliminarMesa.Location = new System.Drawing.Point(809, 564);
            this.btnEliminarMesa.Name = "btnEliminarMesa";
            this.btnEliminarMesa.Size = new System.Drawing.Size(140, 31);
            this.btnEliminarMesa.TabIndex = 12;
            this.btnEliminarMesa.Text = "Eliminar";
            this.btnEliminarMesa.UseVisualStyleBackColor = true;
            this.btnEliminarMesa.Click += new System.EventHandler(this.btnEliminarMesa_Click);
            // 
            // btnGuardarMesa
            // 
            this.btnGuardarMesa.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnGuardarMesa.IconColor = System.Drawing.Color.Black;
            this.btnGuardarMesa.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardarMesa.Location = new System.Drawing.Point(624, 564);
            this.btnGuardarMesa.Name = "btnGuardarMesa";
            this.btnGuardarMesa.Size = new System.Drawing.Size(140, 31);
            this.btnGuardarMesa.TabIndex = 11;
            this.btnGuardarMesa.Text = "Guardar";
            this.btnGuardarMesa.UseVisualStyleBackColor = true;
            this.btnGuardarMesa.Click += new System.EventHandler(this.btnGuardarMesa_Click);
            // 
            // tbNumeroMesa
            // 
            this.tbNumeroMesa.Location = new System.Drawing.Point(732, 348);
            this.tbNumeroMesa.Name = "tbNumeroMesa";
            this.tbNumeroMesa.Size = new System.Drawing.Size(100, 26);
            this.tbNumeroMesa.TabIndex = 13;
            // 
            // MenuForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(980, 680);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Imagen)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMesa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage2;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private System.Windows.Forms.PictureBox Imagen;
        private FontAwesome.Sharp.IconButton btnAgregar;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtProducto;
        private FontAwesome.Sharp.IconButton rutaImagen;
        private System.Windows.Forms.ComboBox cbMenu;
        private FontAwesome.Sharp.IconButton btnEliminarMesa;
        private FontAwesome.Sharp.IconButton btnGuardarMesa;
        private FontAwesome.Sharp.IconButton btnImagenMesa;
        private System.Windows.Forms.PictureBox pbMesa;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TextBox tbNumeroMesa;
    }
}