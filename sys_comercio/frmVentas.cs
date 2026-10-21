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
    public partial class frmVentas : Form
    {
        int idUsuarioActual;
        string flag = "No seleccionado";
        public frmVentas(int idUsuario)
        {
            InitializeComponent();
            idUsuarioActual = idUsuario;

            var da = new SqlDataAdapter("spu_mostrar_venta", new Conexion().Connect());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            var dt = new DataTable();
            da.Fill(dt);
            dgvVentas.DataSource = dt;
            dgvVentas.Columns[0].HeaderText = "ID ventas";
            dgvVentas.Columns[1].HeaderText = "Fecha";
            dgvVentas.Columns[2].HeaderText = "ID clientes";
            dgvVentas.Columns[3].HeaderText = "Importe";
            dgvVentas.Columns[4].HeaderText = "ID usuarios";
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // 🔹 Estilo moderno azul oscuro (consistente con los otros formularios)
            dgvVentas.EnableHeadersVisualStyles = false;
            dgvVentas.BorderStyle = BorderStyle.None;
            dgvVentas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVentas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvVentas.RowHeadersVisible = false;

            // 🎨 Colores base del formulario
            dgvVentas.BackgroundColor = Color.FromArgb(26, 32, 40); // Fondo general oscuro
            dgvVentas.DefaultCellStyle.BackColor = Color.FromArgb(37, 45, 55); // Fondo de las filas
            dgvVentas.DefaultCellStyle.ForeColor = Color.WhiteSmoke; // Texto principal
            dgvVentas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 38, 48); // Filas alternadas
            dgvVentas.GridColor = Color.FromArgb(49, 66, 82); // Líneas suaves

            // 🔹 Encabezados
            dgvVentas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(49, 66, 82); // Azul grisáceo
            dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvVentas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvVentas.ColumnHeadersHeight = 35;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // 🔹 Filas
            dgvVentas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 90, 110); // Azul más claro al seleccionar
            dgvVentas.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvVentas.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvVentas.RowTemplate.Height = 30;

            // 🔹 Ajustes visuales extra
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.MultiSelect = false;
            dgvVentas.ReadOnly = true;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.AllowUserToResizeRows = false;
        }
        private void dgvVentas_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvVentas.Rows[e.RowIndex];

                txtIdVenta.Text = fila.Cells["id_venta"].Value.ToString();
                txtIdCliente.Text = fila.Cells["id_cliente"].Value.ToString();
                txtImporte.Text = fila.Cells["importe"].Value.ToString();
                txtIdUsuario.Text = fila.Cells["id_usuario"].Value.ToString();
            }
        }

        private void picAgregar_Click(object sender, EventArgs e)
        {   
            flag = "Agregar";
            btnDatos.Text = "Agregar";
            txtIdUsuario.Text = idUsuarioActual.ToString();
            using (SqlConnection conn = new Conexion().Connect())
            {
                SqlCommand cmd = new SqlCommand("spu_id_nueva_venta", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                object result = cmd.ExecuteScalar();
                txtIdVenta.Text = result.ToString();
            }
        }

        private void picActualizar_Click(object sender, EventArgs e)
        {
            flag = "Modificar";
            btnDatos.Text = "Modificar";
        }

        private void picEliminar_Click(object sender, EventArgs e)
        {
            flag = "Eliminar";
            btnDatos.Text = "Eliminar";
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (frmClientes frm = new frmClientes())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtIdCliente.Text = frm.IdClienteSeleccionado.ToString();
                }
            }
        }

        private void picLupaImporte_Click(object sender, EventArgs e)
        {
            using (frmProductos frm = new frmProductos())
            {
                frm.ModoSeleccion = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    decimal importeActual = 0;
                    decimal.TryParse(txtImporte.Text, out importeActual);

                    decimal nuevoTotal = importeActual + frm.TotalSeleccionado;
                    txtImporte.Text = nuevoTotal.ToString("0.00");
                }
            }
        }

        private void btnDatos_Click_1(object sender, EventArgs e)
        {
            if (flag == "No seleccionado")
            {
                MessageBox.Show("Debe seleccionar una accion (Agregar, Eliminar, Modificar)");
                return;
            }
            else if (flag == "Agregar")
            {
                if (txtIdVenta.Text.Trim() == "" || txtIdCliente.Text.Trim() == "" || txtImporte.Text.Trim() == "" || txtIdUsuario.Text.Trim() == "")
                {
                    MessageBox.Show("Debe completar todos los campos para agragar una venta.");
                    return;
                }
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spu_alta_venta", new Conexion().Connect()))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_venta", int.Parse(txtIdVenta.Text.Trim()));
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(txtFecha.Text.Trim()));
                        cmd.Parameters.AddWithValue("@id_cliente", int.Parse(txtIdCliente.Text.Trim()));
                        cmd.Parameters.AddWithValue("@importe", decimal.Parse(txtImporte.Text.Trim()));
                        cmd.Parameters.AddWithValue("@id_usuario", int.Parse(txtIdUsuario.Text.Trim()));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Venta agregada exitosamente.");
                        var da = new SqlDataAdapter("spu_mostrar_venta", new Conexion().Connect());
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvVentas.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar la venta: " + ex.Message);
                }
            }
            else if (flag == "Eliminar")
            {
                if (txtIdVenta.Text.Trim() == "")
                {
                    MessageBox.Show("Debe ingresar el ID de la venta a eliminar.");
                    return;
                }
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spu_eliminar_venta", new Conexion().Connect()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_venta", int.Parse(txtIdVenta.Text.Trim()));

                        int filas = cmd.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            MessageBox.Show("Venta eliminada exitosamente.");
                            var da = new SqlDataAdapter("spu_mostrar_venta", new Conexion().Connect());
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            var dt = new DataTable();
                            da.Fill(dt);
                            dgvVentas.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No se encontró la venta con el ID especificado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la venta: " + ex.Message);
                }
            }
            else
            if (flag == "Modificar")
            {
                if (txtIdVenta.Text.Trim() == "" || txtFecha.Text.Trim() == "" || txtIdCliente.Text.Trim() == "" || txtImporte.Text.Trim() == "" || txtIdUsuario.Text.Trim() == "")
                {
                    MessageBox.Show("Debe completar todos los campos para modificar una venta.");
                    return;
                }

                try
                {

                    using (SqlCommand cmd = new SqlCommand("spu_modificar_venta", new Conexion().Connect()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_venta", int.Parse(txtIdVenta.Text.Trim()));
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Parse(txtFecha.Text.Trim()));
                        cmd.Parameters.AddWithValue("@id_cliente", int.Parse(txtIdCliente.Text.Trim()));
                        cmd.Parameters.AddWithValue("@importe", decimal.Parse(txtImporte.Text.Trim()));
                        cmd.Parameters.AddWithValue("@id_usuario", int.Parse(txtIdUsuario.Text.Trim()));



                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                            MessageBox.Show("Venta modificada exitosamente.");
                        else
                            MessageBox.Show("No se encontró la venta a modificar.");
                    }

                    SqlDataAdapter da = new SqlDataAdapter("spu_mostrar_venta", new Conexion().Connect());
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvVentas.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar la venta: " + ex.Message);
                }
            }
        }
    }
}
