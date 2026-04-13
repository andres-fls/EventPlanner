using EventPlanner.Data;
using EventPlanner.Models;
using System.Data;
using System.Data.SqlClient;

namespace EventPlanner.DAO
{
    public class UsuarioDAO
    {
        // ==========================
        // LOGIN
        // ==========================
        public Usuario ObtenerUsuarioLogin(string usuario, string password)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                string query = @"SELECT idUsuario, nombreusuario, rolusuario
                                 FROM Usuario
                                 WHERE nombreusuario = @usuario
                                 AND passwordusuario = @password";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                idUsuario = (int)reader["idUsuario"],
                                nombreUsuario = reader["nombreusuario"].ToString(),
                                rolUsuario = reader["rolusuario"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        // ==========================
        // CREAR USUARIO
        // ==========================
        public int CrearUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                string query = @"INSERT INTO Usuario
                                (nombreusuario, passwordusuario, rolusuario)
                                OUTPUT INSERTED.idUsuario
                                VALUES (@usuario, @password, @rol)";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario.nombreUsuario;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = usuario.passwordUsuario;
                    cmd.Parameters.Add("@rol", SqlDbType.VarChar).Value = usuario.rolUsuario;

                    conexion.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        // ==========================
        // OBTENER POR ID
        // ==========================
        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                string query = @"SELECT idUsuario, nombreusuario, passwordusuario, rolusuario
                                 FROM Usuario
                                 WHERE idUsuario = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario;

                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                idUsuario = (int)reader["idUsuario"],
                                nombreUsuario = reader["nombreusuario"].ToString(),
                                passwordUsuario = reader["passwordusuario"].ToString(),
                                rolUsuario = reader["rolusuario"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        // ==========================
        // OBTENER POR NOMBRE (CORREGIDO)
        // ==========================
        public Usuario ObtenerPorNombre(string nombreUsuario)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                string query = @"SELECT idUsuario, nombreusuario, passwordusuario, rolusuario
                                 FROM Usuario
                                 WHERE nombreusuario = @usuario";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = nombreUsuario;

                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                idUsuario = (int)reader["idUsuario"],
                                nombreUsuario = reader["nombreusuario"].ToString(),
                                passwordUsuario = reader["passwordusuario"].ToString(),
                                rolUsuario = reader["rolusuario"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}