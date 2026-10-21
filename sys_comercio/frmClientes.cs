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
    public partial class frmClientes : Form
    {
        DataTable clientesTable;
        public int IdClienteSeleccionado { get; set; }
        int idActual;
        string flag = "No seleccionado";
        public frmClientes()
        {
            InitializeComponent();
            var da = new SqlDataAdapter("spu_mostrar_cliente", new Conexion().Connect());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            clientesTable = new DataTable();
            da.Fill(clientesTable);
            dgvClientes.DataSource = clientesTable;
            dgvClientes.Columns[0].HeaderText = "ID";
            dgvClientes.Columns[1].HeaderText = "Razón social";
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            // Estilo azul oscuro moderno para DataGridView
            dgvClientes.EnableHeadersVisualStyles = false;
            dgvClientes.BorderStyle = BorderStyle.None;
            dgvClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvClientes.RowHeadersVisible = false;

            // Colores base del formulario
            dgvClientes.BackgroundColor = Color.FromArgb(26, 32, 40); // Fondo general oscuro
            dgvClientes.DefaultCellStyle.BackColor = Color.FromArgb(37, 45, 55); // Fondo de las filas
            dgvClientes.DefaultCellStyle.ForeColor = Color.WhiteSmoke; // Texto principal
            dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 38, 48); // Filas alternadas
            dgvClientes.GridColor = Color.FromArgb(49, 66, 82); // Líneas suaves

            // Encabezados
            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(49, 66, 82); // Azul grisáceo
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvClientes.ColumnHeadersHeight = 35;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Filas
            dgvClientes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 90, 110); // Azul más claro al seleccionar
            dgvClientes.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvClientes.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvClientes.RowTemplate.Height = 30;

            // Ajustes visuales extra
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AllowUserToResizeRows = false;

        }
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];

                txtId.Text = fila.Cells["id_cliente"].Value.ToString();
                txtRazonSocial.Text = fila.Cells["razon_social"].Value.ToString();

            }
        }
        private void dgvClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvClientes.CurrentRow == null) return;

                IdClienteSeleccionado = Convert.ToInt32(dgvClientes.CurrentRow.Cells["id_cliente"].Value);
                this.DialogResult = DialogResult.OK;
                this.Close();

                e.Handled = true; 
            }
        }


        private void picAgregar_Click(object sender, EventArgs e)
        {
            flag = "Agregar";
            btnDatos.Text = "Agregar";

            txtId.Text = idActual.ToString();
            using (SqlConnection conn = new Conexion().Connect())
            {
                SqlCommand cmd = new SqlCommand("spu_id_nueva_cliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                object result = cmd.ExecuteScalar();
                txtId.Text = result.ToString();
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

        private void btnDatos_Click(object sender, EventArgs e)
        {
            if (flag == "No seleccionado")
            {
                MessageBox.Show("Debe seleccionar una accion (Agregar, Eliminar, Modificar)");
                return;
            }
            else if (flag == "Agregar")
            {
                if (txtRazonSocial.Text.Trim() == "" || txtId.Text.Trim() == "")
                {
                    MessageBox.Show("Debe completar todos los campos para agragar un cliente.");
                    return;
                }
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spu_alta_cliente", new Conexion().Connect()))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_cliente", txtId.Text.Trim());
                        cmd.Parameters.AddWithValue("@razon_social", txtRazonSocial.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cliente agregado exitosamente.");
                        var da = new SqlDataAdapter("spu_mostrar_cliente", new Conexion().Connect());
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvClientes.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el cliente: " + ex.Message);
                }
            }
            else if (flag == "Eliminar")
            {
                if (txtId.Text.Trim() == "")
                {
                    MessageBox.Show("Debe ingresar el ID del cliente a eliminar.");
                    return;
                }
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spu_baja_cliente", new Conexion().Connect()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_Cliente", Convert.ToInt32(txtId.Text.Trim()));
                        int filas = cmd.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            MessageBox.Show("Cliente eliminado exitosamente.");
                            var da = new SqlDataAdapter("spu_mostrar_cliente", new Conexion().Connect());
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            var dt = new DataTable();
                            da.Fill(dt);
                            dgvClientes.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el cliente con el ID especificado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se puede eliminar el cliente porque tiene ventas asociadas.", "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            if (flag == "Modificar")
            {
                if (txtId.Text.Trim() == "" || txtRazonSocial.Text.Trim() == "")
                {
                    MessageBox.Show("Debe completar todos los campos para modificar un cliente.");
                    return;
                }

                try
                {

                    using (SqlCommand cmd = new SqlCommand("spu_modificar_cliente", new Conexion().Connect()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_cliente", Convert.ToInt32(txtId.Text.Trim()));
                        cmd.Parameters.AddWithValue("@razon_social", txtRazonSocial.Text.Trim());


                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                            MessageBox.Show("Cliente modificado exitosamente.");
                        else
                            MessageBox.Show("No se encontró el cliente a modificar.");
                    }

                    SqlDataAdapter da = new SqlDataAdapter("spu_mostrar_cliente", new Conexion().Connect());
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvClientes.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el cliente: " + ex.Message);
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvClientes.DataSource != null)
            {
                string filtro = txtBuscar.Text.Trim().Replace("'", "''");

                DataTable dt = dgvClientes.DataSource as DataTable;
                if (dt != null)
                {
                    dt.DefaultView.RowFilter =
                        $"Convert([razon_social], 'System.String') LIKE '%{filtro}%'" +
                        $"OR Convert([id_cliente], 'System.String') LIKE '%{filtro}%'";
                }
            }
        }


    }
    }



