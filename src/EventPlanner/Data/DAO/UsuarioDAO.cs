using System.Data.SqlClient;

namespace EventPlanner.Data
{
    public class UsuarioDAO
    {
        public string ValidarLogin(string usuario, string password)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();
                string query = @"SELECT rolusuario 
                                 FROM usuario
                                 WHERE nombreusuario = @usuario
                                 AND passwordusuario = @password";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@password", password);

                    object resultado = cmd.ExecuteScalar();

                    return resultado?.ToString();
                }
            }
        }
    }
}