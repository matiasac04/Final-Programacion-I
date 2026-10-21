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
    public partial class frmProductos : Form
    {
        DataTable productosTable;
        public bool ModoSeleccion { get; set; } = false;
        public int StockSeleccionado { get; private set; }
        public int CodigoSeleccionado { get; private set; }
        public string ProductoSeleccionado { get; private set; }
        public decimal PrecioSeleccionado { get; private set; }

        int idCodigoActual;


        string flag = "No seleccionado";
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            // 🔹 Cargar datos desde el procedimiento almacenado
            var da = new SqlDataAdapter("spu_mostrar_productos", new Conexion().Connect());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            var dt = new DataTable();
            da.Fill(dt);
            dgvProductos.DataSource = dt;

            // 🔹 Configurar encabezados de columnas
            dgvProductos.Columns[0].HeaderText = "Código";
            dgvProductos.Columns[1].HeaderText = "Descripción";
            dgvProductos.Columns[2].HeaderText = "Stock";
            dgvProductos.Columns[3].HeaderText = "Precio";

            // 🔹 Estilo moderno azul oscuro
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProductos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProductos.RowHeadersVisible = false;

            // 🎨 Colores base del formulario
            dgvProductos.BackgroundColor = Color.FromArgb(26, 32, 40); // Fondo general oscuro
            dgvProductos.DefaultCellStyle.BackColor = Color.FromArgb(37, 45, 55); // Fondo de las filas
            dgvProductos.DefaultCellStyle.ForeColor = Color.WhiteSmoke; // Texto principal
            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 38, 48); // Filas alternadas
            dgvProductos.GridColor = Color.FromArgb(49, 66, 82); // Líneas suaves

            // 🔹 Encabezados
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(49, 66, 82); // Azul grisáceo
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProductos.ColumnHeadersHeight = 35;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // 🔹 Filas
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 90, 110); // Azul más claro al seleccionar
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvProductos.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvProductos.RowTemplate.Height = 30;

            // 🔹 Ajustes visuales extra
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
        }

        private void dgvProductos_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                txtCodigo.Text = fila.Cells["codigo"].Value.ToString();
                txtDescripcion.Text = fila.Cells["descripcion"].Value.ToString();
                txtStock.Text = fila.Cells["stock"].Value.ToString();
                txtPrecio.Text = fila.Cells["precio"].Value.ToString();
            }
        }

        private void picAgregar_Click(object sender, EventArgs e)
        {
            flag = "Agregar";
            btnDatos.Text = "Agregar";
            txtCodigo.Text = idCodigoActual.ToString();
            using (SqlConnection conn = new Conexion().Connect())
            {
                SqlCommand cmd = new SqlCommand("spu_id_nueva_producto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                object result = cmd.ExecuteScalar();
                txtCodigo.Text = result.ToString();
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
                if (txtCodigo.Text.Trim() == "" || txtDescripcion.Text.Trim() == "" || txtStock.Text.Trim() == "" || txtPrecio.Text.Trim() == "")
                {
                    MessageBox.Show("Debe completar todos los campos para agragar un producto.");
                    return;
                }
                try
                {
                    decimal precio;

                    if (!decimal.TryParse(txtPrecio.Text, out precio))
                    {
                        MessageBox.Show("Precio inválido.");
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand("spu_alta_productos", new Conexion().Connect()))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text.Trim());
                        cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                        cmd.Parameters.AddWithValue("@stock", txtStock.Text.Trim());
                        cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = precio;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Producto agregado exitosamente.");
                        var da = new SqlDataAdapter("spu_mostrar_productos", new Conexion().Connect());
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvProductos.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el producto: " + ex.Message);
                }
            }
            else if (flag == "Eliminar")
            {
                if (txtCodigo.Text.Trim() == "")
                {
                    MessageBox.Show("Debe ingresar el ID del producto a eliminar.");
                    return;
                }
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spu_baja_productos", new Conexion().Connect()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codigo", Convert.ToInt32(txtCodigo.Text.Trim()));
                        int filas = cmd.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            MessageBox.Show("Producto eliminado exitosamente.");
                            var da = new SqlDataAdapter("spu_mostrar_productos", new Conexion().Connect());
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            var dt = new DataTable();
                            da.Fill(dt);
                            dgvProductos.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el producto con el código especificado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se puede eliminar el producto porque tiene ventas asociadas.",
                    "Operación no permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
);
                }
            }
            else
            if (flag == "Modificar")
            {
                if (txtCodigo.Text.Trim() == "" || txtDescripcion.Text.Trim() == "" || txtStock.Text.Trim() == "" || txtPrecio.Text.Trim() == "")
                {
                    MessageBox.Show("Debe completar todos los campos para modificar un producto.");
                    return;
                }

                try
                {
                    decimal precio;

                    if (!decimal.TryParse(txtPrecio.Text, out precio))
                    {
                        MessageBox.Show("Precio inválido.");
                        return;
                    }
                    using (SqlCommand cmd = new SqlCommand("spu_modificar_productos", new Conexion().Connect()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codigo", Convert.ToInt32(txtCodigo.Text.Trim()));
                        cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text.Trim());
                        cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text.Trim()));
                        cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = precio;


                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                            MessageBox.Show("Producto modificado exitosamente.");
                        else
                            MessageBox.Show("No se encontró el producto a modificar.");
                    }

                    SqlDataAdapter da = new SqlDataAdapter("spu_mostrar_productos", new Conexion().Connect());
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvProductos.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el producto: " + ex.Message);
                }
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!ModoSeleccion)
                return;

            if (dgvProductos.SelectedRows.Count > 0)
            {
                decimal total = 0;

                foreach (DataGridViewRow fila in dgvProductos.SelectedRows)
                {
                    if (fila.Cells["precio"].Value != DBNull.Value)
                        total += Convert.ToDecimal(fila.Cells["precio"].Value);
                }

                //TotalSeleccionado = total;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (dgvProductos.DataSource != null)
            {
                string filtro = txtBuscar.Text.Trim().Replace("'", "''");

                DataTable dt = dgvProductos.DataSource as DataTable;
                if (dt != null)
                {
                    dt.DefaultView.RowFilter =
                        $"Convert([descripcion], 'System.String') LIKE '%{filtro}%'";
                }
            }
        }

        private void dgvProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvProductos.CurrentRow == null) return;

                CodigoSeleccionado = Convert.ToInt32(dgvProductos.CurrentRow.Cells["Codigo"].Value);

                ProductoSeleccionado = dgvProductos.CurrentRow.Cells["Descripcion"].Value.ToString();

                PrecioSeleccionado = Convert.ToDecimal(dgvProductos.CurrentRow.Cells["Precio"].Value);

                StockSeleccionado = Convert.ToInt32(dgvProductos.CurrentRow.Cells["Stock"].Value);

                this.DialogResult = DialogResult.OK;
                this.Close();

                e.Handled = true;
            }
        }

        private void dgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvProductos.Columns[e.ColumnIndex].Name == "stock")
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int stock))
                {
                    if (stock == 0)
                    {
                        dgvProductos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkRed;
                        dgvProductos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        // vuelve al color normal
                        dgvProductos.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                            Color.FromArgb(37, 45, 55);
                        dgvProductos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.WhiteSmoke;
                    }
                }
            }
        }
    }
}
