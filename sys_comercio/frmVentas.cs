using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sys_comercio
{
    public partial class frmVentas : Form
    {
        int stockActual = -1;
        int idVentaActual;
        int idUsuarioActual;
        string flag = "No seleccionado";

        public frmVentas(int idUsuario)
        {
            InitializeComponent();
            idUsuarioActual = idUsuario;

            SqlDataAdapter da = new SqlDataAdapter("spu_mostrar_venta", new Conexion().Connect());

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvVentas.DataSource = dt;

            dgvVentas.Columns[0].HeaderText = "ID venta";
            dgvVentas.Columns[1].HeaderText = "Fecha";
            dgvVentas.Columns[2].HeaderText = "ID cliente";
            dgvVentas.Columns[3].HeaderText = "Importe";
            dgvVentas.Columns[4].HeaderText = "ID usuario";
        }

        private void CargarDetalle()
        {
            SqlDataAdapter da = new SqlDataAdapter("spu_mostrar_detalle_venta", new Conexion().Connect());

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@id_venta", int.Parse(txtIdVenta.Text));

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvDetalle.DataSource = dt;
        }

        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow fila in dgvDetalle.Rows)
            {
                if (fila.Cells["precio_r"].Value != null)
                {
                    total += Convert.ToDecimal(fila.Cells["precio_r"].Value);
                }
            }

            txtImporteTotal.Text = total.ToString("0.00");
        }

        private void LimpiarDetalle()
        {
            txtCodigo.Clear();
            txtProducto.Clear();
            txtPrecio.Clear();
            txtCant.Clear();
            txtPrecio_r.Clear();
            txtCodigo.Focus();
        }



        private void frmVentas_Load(object sender, EventArgs e)
        {
            timerFecha.Interval = 1000;
            timerFecha.Start();
            //txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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

            // 🔹 Estilo moderno azul oscuro – Detalle Venta
            dgvDetalle.EnableHeadersVisualStyles = false;
            dgvDetalle.BorderStyle = BorderStyle.None;
            dgvDetalle.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDetalle.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDetalle.RowHeadersVisible = false;

            // 🎨 Colores base
            dgvDetalle.BackgroundColor = Color.FromArgb(26, 32, 40);
            dgvDetalle.DefaultCellStyle.BackColor = Color.FromArgb(37, 45, 55);
            dgvDetalle.DefaultCellStyle.ForeColor = Color.WhiteSmoke;
            dgvDetalle.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(30, 38, 48);
            dgvDetalle.GridColor = Color.FromArgb(49, 66, 82);

            // 🔹 Encabezados
            dgvDetalle.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(49, 66, 82);
            dgvDetalle.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetalle.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDetalle.ColumnHeadersHeight = 35;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // 🔹 Filas
            dgvDetalle.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 90, 110);
            dgvDetalle.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvDetalle.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvDetalle.RowTemplate.Height = 30;

            // 🔹 Ajustes extra
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.MultiSelect = false;
            dgvDetalle.ReadOnly = true;
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.AllowUserToResizeRows = false;

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


        private void btnDatos_Click_1(object sender, EventArgs e)
        {
            if (flag == "No seleccionado")
            {
                MessageBox.Show("Debe seleccionar una accion (Agregar, Eliminar, Modificar)");
                return;
            }
            else if (flag == "Agregar")
            {
                if (txtIdVenta.Text.Trim() == "" || txtIdCliente.Text.Trim() == "" || txtIdUsuario.Text.Trim() == "")
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
                        cmd.Parameters.AddWithValue("@importe", 0);
                        cmd.Parameters.AddWithValue("@id_usuario", int.Parse(txtIdUsuario.Text.Trim()));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Venta agregada exitosamente.");                        

                        var da = new SqlDataAdapter("spu_mostrar_venta", new Conexion().Connect());
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvVentas.DataSource = dt;
                        
                        SqlDataAdapter daDetalle = new SqlDataAdapter("spu_mostrar_detalle_venta", new Conexion().Connect());

                        daDetalle.SelectCommand.CommandType = CommandType.StoredProcedure;
                        daDetalle.SelectCommand.Parameters.AddWithValue("@id_venta", idVentaActual);

                        DataTable dtDetalle = new DataTable();
                        daDetalle.Fill(dtDetalle);

                        dgvDetalle.DataSource = dtDetalle;

                        dgvDetalle.Columns[0].HeaderText = "Descripcion";
                        dgvDetalle.Columns[1].HeaderText = "Precio";
                        dgvDetalle.Columns[2].HeaderText = "Cantidad";
                        dgvDetalle.Columns[3].HeaderText = "Precio R";

                        txtIdVenta.Enabled = false;
                        txtIdCliente.Enabled = false;
                        txtIdUsuario.Enabled = false;
                        btnDatos.Enabled = false;
                        picLupaIdCliente.Enabled = false;

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
                    using (SqlCommand cmdDetalle = new SqlCommand("spu_eliminar_detalles_por_venta", new Conexion().Connect()))
                    {
                        cmdDetalle.CommandType = CommandType.StoredProcedure;
                        cmdDetalle.Parameters.AddWithValue("@id_venta", int.Parse(txtIdVenta.Text));
                        cmdDetalle.ExecuteNonQuery();
                    }
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

        private void picLupaCodigo_Click(object sender, EventArgs e)
        {
            stockActual = -1;
            btnIngresar.Enabled = false;

            using (frmProductos frm = new frmProductos())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtCodigo.Text = frm.CodigoSeleccionado.ToString();
                    txtProducto.Text = frm.ProductoSeleccionado;
                    txtPrecio.Text = frm.PrecioSeleccionado.ToString("0.00");

                    stockActual = frm.StockSeleccionado;

                    if (stockActual == 0)
                    {
                        btnIngresar.Enabled = false;
                        MessageBox.Show("No hay stock disponible para este producto.");
                    }
                    else
                    {
                        btnIngresar.Enabled = true;
                    }

                    txtCant.Text = "";
                    txtPrecio_r.Text = "";
                    txtCant.Focus();
                }
            }
        }

        private void txtCant_TextChanged(object sender, EventArgs e)
        {
            if (txtCant.Text == "") return;

            if (!int.TryParse(txtCant.Text, out int cantidad)) return;

            decimal precio = decimal.Parse(txtPrecio.Text);

            txtPrecio_r.Text = (precio * cantidad).ToString("0.00");
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (stockActual <= 0)
            {
                MessageBox.Show("No se puede agregar el producto porque no hay stock.");
                return;
            }

            int cantidad = Convert.ToInt32(txtCant.Text);

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.");
                return;
            }

            if (cantidad > stockActual)
            {
                MessageBox.Show("La cantidad ingresada supera el stock disponible.");
                return;
            }

            if (
                 txtCodigo.Text == "" || txtCant.Text == "" || txtPrecio.Text == "" || txtPrecio_r.Text == "")
            {
                MessageBox.Show("Complete todos los datos del detalle.");
                return;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand("spu_cargar_detalleventa", new Conexion().Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_venta", int.Parse(txtIdVenta.Text));
                    cmd.Parameters.AddWithValue("@codigo", int.Parse(txtCodigo.Text));
                    cmd.Parameters.AddWithValue("@precio", decimal.Parse(txtPrecio.Text));
                    cmd.Parameters.AddWithValue("@cant", int.Parse(txtCant.Text));
                    cmd.Parameters.AddWithValue("@precio_r", decimal.Parse(txtPrecio_r.Text));

                    cmd.ExecuteNonQuery();
                }

                CargarDetalle();
                CalcularTotal();
                LimpiarDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar detalle: " + ex.Message);
            }
        }

        private void btnFinalizarDetealle_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spu_actualizar_venta", new Conexion().Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_venta", int.Parse(txtIdVenta.Text));
                    cmd.Parameters.AddWithValue("@id_cliente", int.Parse(txtIdCliente.Text));
                    cmd.Parameters.AddWithValue("@importe", decimal.Parse(txtImporteTotal.Text));

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Venta actualizada correctamente.");

                var da = new SqlDataAdapter("spu_mostrar_venta", new Conexion().Connect());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                var dt = new DataTable();
                da.Fill(dt);
                dgvVentas.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la venta: " + ex.Message);
            }
            picLupaIdCliente.Enabled = true;
            txtIdVenta.Enabled = true;
            txtIdCliente.Enabled = true;
            txtIdUsuario.Enabled = true;
            btnDatos.Enabled = true;
        }

        private void btnEliminarDetalle_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un detalle para eliminar.");
                return;
            }

            int codigo = Convert.ToInt32(dgvDetalle.CurrentRow.Cells["codigo"].Value);
            int idVenta = int.Parse(txtIdVenta.Text);

            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "spu_eliminar_detalleventa",
                    new Conexion().Connect()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_venta", idVenta);
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    cmd.ExecuteNonQuery();
                }
                CargarDetalle();
                CalcularTotal();

                MessageBox.Show("Detalle eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar detalle: " + ex.Message);
            }
        }


        private void timerFecha_Tick(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
