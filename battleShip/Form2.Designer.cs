
namespace battleShip
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btnJugar = new System.Windows.Forms.Button();
            this.btnInstrucciones = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnJugar
            // 
            this.btnJugar.BackColor = System.Drawing.Color.Transparent;
            this.btnJugar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnJugar.BackgroundImage")));
            this.btnJugar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJugar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnJugar.ForeColor = System.Drawing.Color.Silver;
            this.btnJugar.Location = new System.Drawing.Point(23, 39);
            this.btnJugar.Name = "btnJugar";
            this.btnJugar.Size = new System.Drawing.Size(209, 63);
            this.btnJugar.TabIndex = 1;
            this.btnJugar.Text = "Jugar";
            this.btnJugar.UseVisualStyleBackColor = false;
            this.btnJugar.Click += new System.EventHandler(this.btnJugar_Click);
            this.btnJugar.MouseEnter += new System.EventHandler(this.btnMouseEnter);
            this.btnJugar.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            // 
            // btnInstrucciones
            // 
            this.btnInstrucciones.BackColor = System.Drawing.Color.Transparent;
            this.btnInstrucciones.BackgroundImage = global::battleShip.Properties.Resources.boton;
            this.btnInstrucciones.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInstrucciones.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInstrucciones.ForeColor = System.Drawing.Color.Silver;
            this.btnInstrucciones.Location = new System.Drawing.Point(23, 108);
            this.btnInstrucciones.Name = "btnInstrucciones";
            this.btnInstrucciones.Size = new System.Drawing.Size(209, 63);
            this.btnInstrucciones.TabIndex = 2;
            this.btnInstrucciones.Text = "Instrucciones";
            this.btnInstrucciones.UseVisualStyleBackColor = false;
            this.btnInstrucciones.Click += new System.EventHandler(this.btnInstrucciones_Click);
            this.btnInstrucciones.MouseEnter += new System.EventHandler(this.btnMouseEnter);
            this.btnInstrucciones.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Transparent;
            this.btnSalir.BackgroundImage = global::battleShip.Properties.Resources.boton;
            this.btnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSalir.ForeColor = System.Drawing.Color.Silver;
            this.btnSalir.Location = new System.Drawing.Point(23, 177);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(209, 63);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseEnter += new System.EventHandler(this.btnMouseEnter);
            this.btnSalir.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::battleShip.Properties.Resources.fondobarcos;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(799, 450);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::battleShip.Properties.Resources.menu;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnJugar);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Controls.Add(this.btnInstrucciones);
            this.panel1.Font = new System.Drawing.Font("Bernard MT Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(160)))), ((int)(((byte)(30)))));
            this.panel1.Location = new System.Drawing.Point(531, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 279);
            this.panel1.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartBattleShip";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnJugar;
        private System.Windows.Forms.Button btnInstrucciones;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panel1;
    }
}