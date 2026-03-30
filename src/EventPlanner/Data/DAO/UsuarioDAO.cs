using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.Utils;
using System;
using System.Data.SqlClient;

namespace EventPlanner.DAO

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

        public int CrearUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"INSERT INTO Usuario (nombreusuario, passwordusuario, rolusuario)
                         OUTPUT INSERTED.idUsuario
                         VALUES (@usuario, @password, @rol)";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario.nombreUsuario);
                    cmd.Parameters.AddWithValue("@password", usuario.passwordUsuario);
                    cmd.Parameters.AddWithValue("@rol", usuario.rolUsuario);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

    }
}