namespace Sistema_de_Reservaciones_Proyecto_II_
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.PanelLogin = new System.Windows.Forms.Panel();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelContraseña = new System.Windows.Forms.Label();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.tbUsuario = new System.Windows.Forms.TextBox();
            this.tbContraseña = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PanelLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 35);
            this.panel1.TabIndex = 0;
            // 
            // PanelLogin
            // 
            this.PanelLogin.BackColor = System.Drawing.Color.White;
            this.PanelLogin.Controls.Add(this.button1);
            this.PanelLogin.Controls.Add(this.tbContraseña);
            this.PanelLogin.Controls.Add(this.tbUsuario);
            this.PanelLogin.Controls.Add(this.labelUsuario);
            this.PanelLogin.Controls.Add(this.labelContraseña);
            this.PanelLogin.Controls.Add(this.labelLogin);
            this.PanelLogin.Location = new System.Drawing.Point(375, 150);
            this.PanelLogin.Name = "PanelLogin";
            this.PanelLogin.Size = new System.Drawing.Size(300, 350);
            this.PanelLogin.TabIndex = 1;
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.BackColor = System.Drawing.Color.Transparent;
            this.labelLogin.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.Location = new System.Drawing.Point(70, 30);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(171, 28);
            this.labelLogin.TabIndex = 0;
            this.labelLogin.Text = "Iniciar Sesion";
            // 
            // labelContraseña
            // 
            this.labelContraseña.AutoSize = true;
            this.labelContraseña.BackColor = System.Drawing.Color.Transparent;
            this.labelContraseña.Font = new System.Drawing.Font("Dosis", 9.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContraseña.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelContraseña.Location = new System.Drawing.Point(47, 164);
            this.labelContraseña.Name = "labelContraseña";
            this.labelContraseña.Size = new System.Drawing.Size(100, 27);
            this.labelContraseña.TabIndex = 1;
            this.labelContraseña.Text = "Contraseña";
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.BackColor = System.Drawing.Color.Transparent;
            this.labelUsuario.Font = new System.Drawing.Font("Dosis", 9.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsuario.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelUsuario.Location = new System.Drawing.Point(47, 87);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(70, 27);
            this.labelUsuario.TabIndex = 2;
            this.labelUsuario.Text = "Usuario";
            // 
            // tbUsuario
            // 
            this.tbUsuario.Location = new System.Drawing.Point(51, 113);
            this.tbUsuario.Name = "tbUsuario";
            this.tbUsuario.Size = new System.Drawing.Size(200, 28);
            this.tbUsuario.TabIndex = 3;
            // 
            // tbContraseña
            // 
            this.tbContraseña.Location = new System.Drawing.Point(51, 190);
            this.tbContraseña.Name = "tbContraseña";
            this.tbContraseña.Size = new System.Drawing.Size(200, 28);
            this.tbContraseña.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(102, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 39);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1028, 594);
            this.Controls.Add(this.PanelLogin);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Dosis", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.PanelLogin.ResumeLayout(false);
            this.PanelLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PanelLogin;
        private System.Windows.Forms.TextBox tbContraseña;
        private System.Windows.Forms.TextBox tbUsuario;
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.Label labelContraseña;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Button button1;
    }
}

