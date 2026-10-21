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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void picCerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {


            Conexion c = new Conexion();
            SqlConnection conn = c.Connect();

            string usuario = txtUsuario.Text.Trim();
            string clave = txtPsw.Text.Trim();

            if (usuario == "" || clave == "")
            {
                MessageBox.Show("Debe ingresar usuario y clave.");
                return;
            }


            if (conn == null) return;

            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_login", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@psw", clave);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int idUsuario = Convert.ToInt32(dr["id_usuario"]);
                            string nombreUsuario = dr["usuario"].ToString();

                            MessageBox.Show("Bienvenido " + nombreUsuario);

                            this.DialogResult = DialogResult.OK;
                            frmMain frm = new frmMain(nombreUsuario, idUsuario); 
                            frm.ShowDialog();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar usuario: " + ex.Message);
            }

        }

    }
}
