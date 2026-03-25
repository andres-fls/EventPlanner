using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace EventPlanner
{
    class Conexion
    {
        SqlConnection con;

        public SqlConnection Conectar()
        {
            try
            {
                string cadena =
                    ConfigurationManager
                    .ConnectionStrings["EventPlannerDB"]
                    .ConnectionString;

                con = new SqlConnection(cadena);
                con.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error de conexión: " + e.Message);
            }

            return con;
        }

        public void Cerrar()
        {
            if (con != null &&
                con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}