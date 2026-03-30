using System;
using System.Data.SqlClient;
using EventPlanner.Utils;

namespace EventPlanner.Data
{
    public class UsuarioDAO
    {
        public string ValidarLogin(string usuario, string password)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();
                string query = @"SELECT idUsuario, rolusuario 
                         FROM usuario
                         WHERE nombreusuario = @usuario
                         AND passwordusuario = @password";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Session.IdUsuario = Convert.ToInt32(reader["idUsuario"]);
                            return reader["rolusuario"].ToString();
                        }
                    }
                }
            }

            return null;
        }
    }
}