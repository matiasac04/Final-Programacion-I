using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace sys_comercio
{
    internal class Conexion
    {
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader dr;

        // Cadena de conexión: se busca en este orden.
        //   1) App.config.local → archivo local NO versionado (la clave real de cada máquina).
        //   2) App.config       → placeholder versionado en el repo (sin clave).
        private static string ObtenerCadenaConexion()
        {
            string cadenaLocal = LeerCadenaDeArchivoLocal();
            if (!string.IsNullOrWhiteSpace(cadenaLocal))
            {
                return cadenaLocal;
            }

            return ConfigurationManager.AppSettings["CadenaConexion"];
        }

        // Lee App.config.local (mismo formato XML que App.config) desde la carpeta
        // donde corre el .exe. Devuelve null si el archivo no existe, no se puede
        // leer o no tiene la clave "CadenaConexion".
        private static string LeerCadenaDeArchivoLocal()
        {
            try
            {
                string ruta = Path.Combine(Application.StartupPath, "App.config.local");
                if (!File.Exists(ruta))
                {
                    return null;
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(ruta);

                XmlNode nodo = doc.SelectSingleNode("/configuration/appSettings/add[@key='CadenaConexion']");
                if (nodo == null || nodo.Attributes == null || nodo.Attributes["value"] == null)
                {
                    return null;
                }

                return nodo.Attributes["value"].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Carpeta donde se guardan los respaldos (clave "CarpetaBackup").
        // Vacío o ausente = la carpeta de backup por defecto de la instancia de SQL Server.
        public static string ObtenerCarpetaBackup()
        {
            string carpeta = ConfigurationManager.AppSettings["CarpetaBackup"];
            return string.IsNullOrWhiteSpace(carpeta) ? null : carpeta.Trim();
        }

        public SqlConnection Connect()
        {
            string cadena = ObtenerCadenaConexion();
            if (string.IsNullOrWhiteSpace(cadena))
            {
                MessageBox.Show(
                    "No hay cadena de conexión configurada.\n\n" +
                    "Copiá 'App.config.local.example' a 'App.config.local' (en la carpeta del proyecto) " +
                    "y poné tu cadena de conexión:\n\n" +
                    "Data Source=TU_SERVIDOR;Initial Catalog=comercio;User ID=sa;Password=TU_CLAVE",
                    "Falta la configuración de la base de datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return null;
            }

            connection = new SqlConnection(cadena);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
