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
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "¿Está seguro de que desea realizar un backup?",
        "Confirmar Backup",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (result == DialogResult.No)
                return; 

            try
            {
                using (SqlConnection conn = new Conexion().Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("spu_backup", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Backup realizado correctamente.",
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

        private void picBackUp_Click(object sender, EventArgs e)
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
                using (SqlConnection conn = new Conexion().Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("spu_backup", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Backup realizado correctamente.",
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
    }
}
