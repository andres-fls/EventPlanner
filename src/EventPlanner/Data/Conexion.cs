using System.Data.SqlClient;

namespace EventPlanner.Data
{
    public class Conexion
    {
        private string cadenaConexion =
            "Server=.;Database=EventPlanner;Trusted_Connection=True;";

        public SqlConnection Conectar()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}