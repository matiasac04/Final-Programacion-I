namespace sys_comercio
{
    partial class frmVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVentas));
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.lblImporte = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.picActualizar = new System.Windows.Forms.PictureBox();
            this.picEliminar = new System.Windows.Forms.PictureBox();
            this.picAgregar = new System.Windows.Forms.PictureBox();
            this.btnDatos = new System.Windows.Forms.Button();
            this.lblIdVenta = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.txtIdVenta = new System.Windows.Forms.TextBox();
            this.lblIdUsuario = new System.Windows.Forms.Label();
            this.txtIdUsuario = new System.Windows.Forms.TextBox();
            this.picLupaIdCliente = new System.Windows.Forms.PictureBox();
            this.picLupaImporte = new System.Windows.Forms.PictureBox();
            this.dgvVentas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.picActualizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEliminar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAgregar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLupaIdCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLupaImporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.AutoSize = true;
            this.lblIdCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdCliente.Location = new System.Drawing.Point(388, 53);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(97, 24);
            this.lblIdCliente.TabIndex = 41;
            this.lblIdCliente.Text = "ID cliente";
            // 
            // lblImporte
            // 
            this.lblImporte.AutoSize = true;
            this.lblImporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporte.Location = new System.Drawing.Point(598, 52);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(80, 24);
            this.lblImporte.TabIndex = 40;
            this.lblImporte.Text = "Importe";
            // 
            // txtImporte
            // 
            this.txtImporte.BackColor = System.Drawing.SystemColors.Info;
            this.txtImporte.Location = new System.Drawing.Point(576, 79);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(126, 20);
            this.txtImporte.TabIndex = 39;
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.BackColor = System.Drawing.SystemColors.Info;
            this.txtIdCliente.Location = new System.Drawing.Point(371, 80);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(130, 20);
            this.txtIdCliente.TabIndex = 38;
            // 
            // picActualizar
            // 
            this.picActualizar.Image = ((System.Drawing.Image)(resources.GetObject("picActualizar.Image")));
            this.picActualizar.Location = new System.Drawing.Point(59, 12);
            this.picActualizar.Name = "picActualizar";
            this.picActualizar.Size = new System.Drawing.Size(33, 28);
            this.picActualizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picActualizar.TabIndex = 37;
            this.picActualizar.TabStop = false;
            this.picActualizar.Click += new System.EventHandler(this.picActualizar_Click);
            // 
            // picEliminar
            // 
            this.picEliminar.Image = ((System.Drawing.Image)(resources.GetObject("picEliminar.Image")));
            this.picEliminar.Location = new System.Drawing.Point(108, 12);
            this.picEliminar.Name = "picEliminar";
            this.picEliminar.Size = new System.Drawing.Size(33, 28);
            this.picEliminar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEliminar.TabIndex = 36;
            this.picEliminar.TabStop = false;
            this.picEliminar.Click += new System.EventHandler(this.picEliminar_Click);
            // 
            // picAgregar
            // 
            this.picAgregar.Image = ((System.Drawing.Image)(resources.GetObject("picAgregar.Image")));
            this.picAgregar.Location = new System.Drawing.Point(12, 12);
            this.picAgregar.Name = "picAgregar";
            this.picAgregar.Size = new System.Drawing.Size(33, 28);
            this.picAgregar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAgregar.TabIndex = 35;
            this.picAgregar.TabStop = false;
            this.picAgregar.Click += new System.EventHandler(this.picAgregar_Click);
            // 
            // btnDatos
            // 
            this.btnDatos.BackColor = System.Drawing.SystemColors.Info;
            this.btnDatos.Location = new System.Drawing.Point(977, 79);
            this.btnDatos.Name = "btnDatos";
            this.btnDatos.Size = new System.Drawing.Size(94, 21);
            this.btnDatos.TabIndex = 34;
            this.btnDatos.Text = "...";
            this.btnDatos.UseVisualStyleBackColor = false;
            this.btnDatos.Click += new System.EventHandler(this.btnDatos_Click_1);
            // 
            // lblIdVenta
            // 
            this.lblIdVenta.AutoSize = true;
            this.lblIdVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdVenta.Location = new System.Drawing.Point(44, 53);
            this.lblIdVenta.Name = "lblIdVenta";
            this.lblIdVenta.Size = new System.Drawing.Size(85, 24);
            this.lblIdVenta.TabIndex = 33;
            this.lblIdVenta.Text = "ID venta";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(207, 53);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(69, 24);
            this.lblFecha.TabIndex = 32;
            this.lblFecha.Text = "Fecha";
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.SystemColors.Info;
            this.txtFecha.Location = new System.Drawing.Point(165, 80);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(157, 20);
            this.txtFecha.TabIndex = 31;
            // 
            // txtIdVenta
            // 
            this.txtIdVenta.BackColor = System.Drawing.SystemColors.Info;
            this.txtIdVenta.Location = new System.Drawing.Point(59, 80);
            this.txtIdVenta.Name = "txtIdVenta";
            this.txtIdVenta.Size = new System.Drawing.Size(52, 20);
            this.txtIdVenta.TabIndex = 30;
            // 
            // lblIdUsuario
            // 
            this.lblIdUsuario.AutoSize = true;
            this.lblIdUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdUsuario.Location = new System.Drawing.Point(804, 53);
            this.lblIdUsuario.Name = "lblIdUsuario";
            this.lblIdUsuario.Size = new System.Drawing.Size(104, 24);
            this.lblIdUsuario.TabIndex = 43;
            this.lblIdUsuario.Text = "ID usuario";
            // 
            // txtIdUsuario
            // 
            this.txtIdUsuario.BackColor = System.Drawing.SystemColors.Info;
            this.txtIdUsuario.Location = new System.Drawing.Point(793, 79);
            this.txtIdUsuario.Name = "txtIdUsuario";
            this.txtIdUsuario.Size = new System.Drawing.Size(124, 20);
            this.txtIdUsuario.TabIndex = 42;
            // 
            // picLupaIdCliente
            // 
            this.picLupaIdCliente.Image = ((System.Drawing.Image)(resources.GetObject("picLupaIdCliente.Image")));
            this.picLupaIdCliente.Location = new System.Drawing.Point(507, 79);
            this.picLupaIdCliente.Name = "picLupaIdCliente";
            this.picLupaIdCliente.Size = new System.Drawing.Size(22, 19);
            this.picLupaIdCliente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLupaIdCliente.TabIndex = 46;
            this.picLupaIdCliente.TabStop = false;
            this.picLupaIdCliente.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // picLupaImporte
            // 
            this.picLupaImporte.Image = ((System.Drawing.Image)(resources.GetObject("picLupaImporte.Image")));
            this.picLupaImporte.Location = new System.Drawing.Point(708, 79);
            this.picLupaImporte.Name = "picLupaImporte";
            this.picLupaImporte.Size = new System.Drawing.Size(22, 19);
            this.picLupaImporte.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLupaImporte.TabIndex = 47;
            this.picLupaImporte.TabStop = false;
            this.picLupaImporte.Click += new System.EventHandler(this.picLupaImporte_Click);
            // 
            // dgvVentas
            // 
            this.dgvVentas.AllowUserToAddRows = false;
            this.dgvVentas.AllowUserToDeleteRows = false;
            this.dgvVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVentas.Location = new System.Drawing.Point(0, 184);
            this.dgvVentas.Name = "dgvVentas";
            this.dgvVentas.ReadOnly = true;
            this.dgvVentas.Size = new System.Drawing.Size(1158, 219);
            this.dgvVentas.TabIndex = 2;
            this.dgvVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVentas_Click);
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(1200, 654);
            this.Controls.Add(this.dgvVentas);
            this.Controls.Add(this.picLupaImporte);
            this.Controls.Add(this.picLupaIdCliente);
            this.Controls.Add(this.lblIdUsuario);
            this.Controls.Add(this.txtIdUsuario);
            this.Controls.Add(this.lblIdCliente);
            this.Controls.Add(this.lblImporte);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.txtIdVenta);
            this.Controls.Add(this.txtIdCliente);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.picActualizar);
            this.Controls.Add(this.lblIdVenta);
            this.Controls.Add(this.picEliminar);
            this.Controls.Add(this.btnDatos);
            this.Controls.Add(this.picAgregar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVentas";
            this.Text = "Ventas";
            this.Load += new System.EventHandler(this.frmVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picActualizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEliminar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAgregar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLupaIdCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLupaImporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.Label lblImporte;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.PictureBox picActualizar;
        private System.Windows.Forms.PictureBox picEliminar;
        private System.Windows.Forms.PictureBox picAgregar;
        private System.Windows.Forms.Button btnDatos;
        private System.Windows.Forms.Label lblIdVenta;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.TextBox txtIdVenta;
        private System.Windows.Forms.Label lblIdUsuario;
        private System.Windows.Forms.TextBox txtIdUsuario;
        private System.Windows.Forms.PictureBox picLupaIdCliente;
        private System.Windows.Forms.PictureBox picLupaImporte;
        private System.Windows.Forms.DataGridView dgvVentas;
    }
}