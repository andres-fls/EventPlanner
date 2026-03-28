using System.Data.SqlClient;

namespace EventPlanner.Data
{
    class UsuarioDAO
    {
        Conexion cn = new Conexion();

        public string ValidarLogin(string usuario, string password)
        {
            using (SqlConnection conexion = cn.Conectar())
            {
                string query = @"SELECT rolusuario 
                                 FROM usuario
                                 WHERE nombreusuario = @usuario
                                 AND passwordusuario = @password";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@password", password);

                    var resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                        return resultado.ToString();

                    return null;
                }
            }
        }
    }
}