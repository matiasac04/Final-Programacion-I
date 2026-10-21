using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sys_comercio
{
    public partial class frmMain : Form
    {
        int idUsuarioActual;
        private string usuarioActual;

        public frmMain(string usuario, int idUsuario)
        {
            InitializeComponent();
            idUsuarioActual = idUsuario;
            usuarioActual = usuario;
            this.IsMdiContainer = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmFondo frmFondo = new frmFondo();
            frmFondo.MdiParent = this;
            frmFondo.TopLevel = false;
            frmFondo.FormBorderStyle = FormBorderStyle.None;
            frmFondo.Dock = DockStyle.Fill;
            frmFondo.WindowState = FormWindowState.Normal;
            frmFondo.Show();
            lblEstado.Text = "Conectado: " + usuarioActual;
        }



        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes frmClientes = new frmClientes();
            frmClientes.MdiParent = this;
            frmClientes.TopLevel = false;
            frmClientes.FormBorderStyle = FormBorderStyle.None;
            frmClientes.Dock = DockStyle.Fill;
            frmClientes.WindowState = FormWindowState.Normal;
            frmClientes.Show();
            btnClientes.BackColor = Color.FromArgb(0, 80, 200);
            btnProductos.BackColor = Color.FromArgb(26, 32, 40);
            btnVentas.BackColor = Color.FromArgb(26, 32, 40);
            btnBackup.BackColor = Color.FromArgb(26, 32, 40);
            btnCerrar.BackColor = Color.FromArgb(26, 32, 40);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmClientes frmClientes = new frmClientes();
            frmClientes.MdiParent = this;
            frmClientes.TopLevel = false;
            frmClientes.FormBorderStyle = FormBorderStyle.None;
            frmClientes.Dock = DockStyle.Fill;
            frmClientes.WindowState = FormWindowState.Normal;
            frmClientes.Show();
            btnClientes.BackColor = Color.FromArgb(0, 80, 200);
            btnProductos.BackColor = Color.FromArgb(26, 32, 40);
            btnVentas.BackColor = Color.FromArgb(26, 32, 40);
            btnBackup.BackColor = Color.FromArgb(26, 32, 40);
            btnCerrar.BackColor = Color.FromArgb(26, 32, 40);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmProductos frmProductos = new frmProductos();
            frmProductos.MdiParent = this;
            frmProductos.TopLevel = false;
            frmProductos.FormBorderStyle = FormBorderStyle.None;
            frmProductos.Dock = DockStyle.Fill;
            frmProductos.WindowState = FormWindowState.Normal;
            frmProductos.Show();
            btnProductos.BackColor = Color.FromArgb(0, 80, 200);
            btnVentas.BackColor = Color.FromArgb(26, 32, 40);
            btnClientes.BackColor = Color.FromArgb(26, 32, 40);
            btnBackup.BackColor = Color.FromArgb(26, 32, 40);
            btnCerrar.BackColor = Color.FromArgb(26, 32, 40);
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            frmProductos frmProductos = new frmProductos();
            frmProductos.MdiParent = this;
            frmProductos.TopLevel = false;
            frmProductos.FormBorderStyle = FormBorderStyle.None;
            frmProductos.Dock = DockStyle.Fill;
            frmProductos.WindowState = FormWindowState.Normal;
            frmProductos.Show();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            frmVentas frmVentas = new frmVentas(idUsuarioActual);
            frmVentas.MdiParent = this;
            frmVentas.TopLevel = false;
            frmVentas.FormBorderStyle = FormBorderStyle.None;
            frmVentas.Dock = DockStyle.Fill;
            frmVentas.WindowState = FormWindowState.Normal;
            frmVentas.Show();
            btnVentas.BackColor = Color.FromArgb(0, 80, 200);
            btnProductos.BackColor = Color.FromArgb(26, 32, 40);
            btnClientes.BackColor = Color.FromArgb(26, 32, 40);
            btnBackup.BackColor = Color.FromArgb(26, 32, 40);
            btnCerrar.BackColor = Color.FromArgb(26, 32, 40);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmVentas frmVentas = new frmVentas(idUsuarioActual);
            frmVentas.MdiParent = this;
            frmVentas.TopLevel = false;
            frmVentas.FormBorderStyle = FormBorderStyle.None;
            frmVentas.Dock = DockStyle.Fill;
            frmVentas.WindowState = FormWindowState.Normal;
            frmVentas.Show();
            btnVentas.BackColor = Color.FromArgb(0, 80, 200);
            btnProductos.BackColor = Color.FromArgb(26, 32, 40);
            btnClientes.BackColor = Color.FromArgb(26, 32, 40);
            btnBackup.BackColor = Color.FromArgb(26, 32, 40);
            btnCerrar.BackColor = Color.FromArgb(26, 32, 40);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            RealizarBackup();
            btnBackup.BackColor = Color.FromArgb(0, 80, 200);
            btnProductos.BackColor = Color.FromArgb(26, 32, 40);
            btnVentas.BackColor = Color.FromArgb(26, 32, 40);
            btnClientes.BackColor = Color.FromArgb(26, 32, 40);
            btnCerrar.BackColor = Color.FromArgb(26, 32, 40);
        }

        private void picBackUp_Click(object sender, EventArgs e)
        {
            RealizarBackup();
            btnBackup.BackColor = Color.FromArgb(0, 80, 200);
            btnProductos.BackColor = Color.FromArgb(26, 32, 40);
            btnVentas.BackColor = Color.FromArgb(26, 32, 40);
            btnClientes.BackColor = Color.FromArgb(26, 32, 40);
            btnCerrar.BackColor = Color.FromArgb(26, 32, 40);
        }

        // Pide confirmación y hace el respaldo de la base (spu_backup).
        // La carpeta de destino sale de la configuración (clave "CarpetaBackup");
        // si está vacía se usa la carpeta de backup por defecto de la instancia.
        // Al terminar muestra dónde quedó el archivo .bak.
        private void RealizarBackup()
        {
            DialogResult result = MessageBox.Show(
                "¿Está seguro de que desea realizar un backup?",
                "Confirmar Backup",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            try
            {
                string archivo;

                using (SqlConnection conn = new Conexion().Connect())
                {
                    if (conn == null)
                        return;

                    using (SqlCommand cmd = new SqlCommand("spu_backup", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        string carpeta = Conexion.ObtenerCarpetaBackup();
                        if (!string.IsNullOrWhiteSpace(carpeta))
                        {
                            cmd.Parameters.AddWithValue("@ruta_destino", carpeta);
                        }

                        archivo = cmd.ExecuteScalar() as string;
                    }
                }

                MessageBox.Show(
                    string.IsNullOrWhiteSpace(archivo)
                        ? "Backup realizado correctamente."
                        : "Backup realizado correctamente.\n\nArchivo: " + archivo,
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al realizar el backup: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
            btnCerrar.BackColor = Color.FromArgb(0, 80, 200);
            btnProductos.BackColor = Color.FromArgb(26, 32, 40);
            btnVentas.BackColor = Color.FromArgb(26, 32, 40);
            btnClientes.BackColor = Color.FromArgb(26, 32, 40);
            btnBackup.BackColor = Color.FromArgb(26, 32, 40);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            btnCerrar.BackColor = Color.FromArgb(0, 80, 200);
            btnProductos.BackColor = Color.FromArgb(26, 32, 40);
            btnVentas.BackColor = Color.FromArgb(26, 32, 40);
            btnClientes.BackColor = Color.FromArgb(26, 32, 40);
            btnBackup.BackColor = Color.FromArgb(26, 32, 40);
        }

    }
}
