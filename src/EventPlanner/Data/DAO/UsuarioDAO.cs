// ============================================================
// Archivo: UsuarioDAO.cs
// Propósito: Maneja las operaciones de acceso a datos relacionadas 
//            con los usuarios de la aplicación.
// Contiene métodos para validar credenciales de inicio de sesión 
//            y para crear nuevos usuarios en la base de datos.
// ============================================================

using EventPlanner.Data;
using EventPlanner.Models;
using EventPlanner.Utils;
using System;
using System.Data.SqlClient;

namespace EventPlanner.DAO
{
    public class UsuarioDAO
    {
        /// <summary>
        /// Valida las credenciales de un usuario contra la base de datos.
        /// Si son correctas, almacena el Id del usuario en la sesión activa.
        /// </summary>
        /// <param name="usuario">Nombre de usuario ingresado</param>
        /// <param name="password">Contraseña ingresada (sin encriptar aún)</param>
        /// <returns>El rol del usuario si es exitoso, null si falla la autenticación</returns>
        public string ValidarLogin(string usuario, string password)
        {
            // Usa 'using' para asegurar que la conexión se cierre automáticamente al salir del bloque
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();  // Abre la conexión a la base de datos

                // Consulta SQL: obtiene el ID y el rol del usuario que coincida con usuario y contraseña
                string query = @"SELECT idUsuario, rolusuario 
                         FROM usuario
                         WHERE nombreusuario = @usuario
                         AND passwordusuario = @password";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    // Parámetros para evitar inyección SQL
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@password", password);

                    // Ejecuta la consulta y obtiene el resultado
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Si se encontró al menos un registro
                        if (reader.Read())
                        {
                            // Guarda el Id del usuario en la sesión global (para usarlo en toda la app)
                            Session.IdUsuario = Convert.ToInt32(reader["idUsuario"]);
                            // Retorna el rol del usuario (ej: "admin", "instructor", "aprendiz")
                            return reader["rolusuario"].ToString();
                        }
                    }
                }
            }

            // Si no se encontró ningún usuario con esas credenciales, retorna null
            return null;
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto Usuario con los datos a registrar</param>
        /// <returns>El Id autogenerado del nuevo usuario</returns>
        public int CrearUsuario(Usuario usuario)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                // Consulta de inserción con OUTPUT INSERTED.idUsuario para obtener el ID generado
                string query = @"INSERT INTO Usuario (nombreusuario, passwordusuario, rolusuario)
                         OUTPUT INSERTED.idUsuario
                         VALUES (@usuario, @password, @rol)";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    // Asigna los valores del objeto Usuario a los parámetros
                    cmd.Parameters.AddWithValue("@usuario", usuario.nombreUsuario);
                    cmd.Parameters.AddWithValue("@password", usuario.passwordUsuario);
                    cmd.Parameters.AddWithValue("@rol", usuario.rolUsuario);

                    // ExecuteScalar retorna el valor de la primera columna de la primera fila (el id insertado)
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}