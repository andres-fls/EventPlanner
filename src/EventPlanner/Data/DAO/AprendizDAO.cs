// ============================================================
// Archivo: AprendizDAO.cs
// Propósito: Maneja las operaciones de acceso a datos para 
//            la entidad Aprendiz.
// Contiene métodos para crear aprendices, listarlos, buscar
//            por cédula o por ID de usuario, y obtener aprendices
//            que se inscribieron en un rango de fechas.
// ============================================================

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class AprendizDAO
    {
        // =============================
        // INSERTAR APRENDIZ
        // =============================
        // Inserta un nuevo aprendiz en la base de datos.
        // Parámetro: aprendiz - Objeto Aprendiz con los datos a registrar.
        // Retorna: true si la inserción fue exitosa, false en caso contrario.
        public bool CrearAprendiz(Aprendiz aprendiz)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                // Consulta de inserción con todos los campos de la tabla Aprendiz
                string query = @"INSERT INTO Aprendiz
                                (cedulaAprendiz, nombreAprendiz, edadAprendiz,
                                 generoAprendiz, correoAprendiz, telefonoAprendiz,
                                 codigoFicha, idUsuario)
                                VALUES
                                (@cedula, @nombre, @edad,
                                 @genero, @correo, @telefono,
                                 @ficha, @usuario)";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    // Asignación de parámetros con los valores del objeto aprendiz
                    cmd.Parameters.AddWithValue("@cedula", aprendiz.cedulaAprendiz);
                    cmd.Parameters.AddWithValue("@nombre", aprendiz.nombreAprendiz);
                    cmd.Parameters.AddWithValue("@edad", aprendiz.edadAprendiz);
                    cmd.Parameters.AddWithValue("@genero", aprendiz.generoAprendiz);
                    cmd.Parameters.AddWithValue("@correo", aprendiz.correoAprendiz);
                    cmd.Parameters.AddWithValue("@telefono", aprendiz.telefonoAprendiz);
                    cmd.Parameters.AddWithValue("@ficha", aprendiz.codigoFicha);
                    cmd.Parameters.AddWithValue("@usuario", aprendiz.idUsuario);

                    // ExecuteNonQuery retorna filas afectadas; si es > 0, se insertó correctamente
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // =============================
        // LISTAR APRENDICES
        // =============================
        // Retorna una lista con todos los aprendices registrados.
        public List<Aprendiz> ObtenerAprendices()
        {
            List<Aprendiz> lista = new List<Aprendiz>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = "SELECT * FROM Aprendiz"; // Obtiene todas las columnas

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Recorre cada fila y construye un objeto Aprendiz
                    while (reader.Read())
                    {
                        Aprendiz a = new Aprendiz()
                        {
                            idAprendiz = Convert.ToInt32(reader["idAprendiz"]),
                            cedulaAprendiz = reader["cedulaAprendiz"].ToString(),
                            nombreAprendiz = reader["nombreAprendiz"].ToString(),
                            edadAprendiz = Convert.ToInt32(reader["edadAprendiz"]),
                            generoAprendiz = reader["generoAprendiz"].ToString(),
                            correoAprendiz = reader["correoAprendiz"].ToString(),
                            telefonoAprendiz = reader["telefonoAprendiz"].ToString(),
                            codigoFicha = Convert.ToInt32(reader["codigoFicha"]),
                            idUsuario = Convert.ToInt32(reader["idUsuario"])
                        };

                        lista.Add(a);
                    }
                }
            }

            return lista;
        }

        // =============================
        // BUSCAR POR CEDULA
        // =============================
        // Busca un aprendiz específico por su número de cédula.
        // Parámetro: cedula - Número de cédula a buscar.
        // Retorna: Objeto Aprendiz si se encuentra, o null si no existe.
        public Aprendiz BuscarPorCedula(string cedula)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT * FROM Aprendiz
                                 WHERE cedulaAprendiz = @cedula";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@cedula", cedula);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Aprendiz()
                            {
                                idAprendiz = Convert.ToInt32(reader["idAprendiz"]),
                                cedulaAprendiz = reader["cedulaAprendiz"].ToString(),
                                nombreAprendiz = reader["nombreAprendiz"].ToString(),
                                edadAprendiz = Convert.ToInt32(reader["edadAprendiz"]),
                                generoAprendiz = reader["generoAprendiz"].ToString(),
                                correoAprendiz = reader["correoAprendiz"].ToString(),
                                telefonoAprendiz = reader["telefonoAprendiz"].ToString(),
                                codigoFicha = Convert.ToInt32(reader["codigoFicha"]),
                                idUsuario = Convert.ToInt32(reader["idUsuario"])
                            };
                        }
                    }
                }
            }

            return null; // No se encontró ningún aprendiz con esa cédula
        }

        // =============================
        // BUSCAR POR ID DE USUARIO
        // =============================
        // Busca un aprendiz asociado a un ID de usuario específico.
        // Útil para vincular la cuenta de usuario con los datos del aprendiz.
        // Parámetro: idUsuario - Identificador del usuario en la tabla Usuario.
        // Retorna: Objeto Aprendiz si se encuentra, o null si no existe.
        public Aprendiz BuscarPorIdUsuario(int idUsuario)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = "SELECT * FROM Aprendiz WHERE idUsuario = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Aprendiz()
                            {
                                idAprendiz = Convert.ToInt32(reader["idAprendiz"]),
                                cedulaAprendiz = reader["cedulaAprendiz"].ToString(),
                                nombreAprendiz = reader["nombreAprendiz"].ToString(),
                                edadAprendiz = Convert.ToInt32(reader["edadAprendiz"]),
                                generoAprendiz = reader["generoAprendiz"].ToString(),
                                correoAprendiz = reader["correoAprendiz"].ToString(),
                                telefonoAprendiz = reader["telefonoAprendiz"].ToString(),
                                codigoFicha = Convert.ToInt32(reader["codigoFicha"]),
                                idUsuario = Convert.ToInt32(reader["idUsuario"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        // ======================================
        // OBTENER APRENDICES POR RANGO DE FECHAS
        // ======================================
        // Retorna aprendices que hayan realizado inscripciones dentro del rango de fechas.
        // Se usa DISTINCT para evitar duplicados (un aprendiz puede tener varias inscripciones).
        // Parámetros:
        //   desde - Fecha inicial del rango
        //   hasta - Fecha final del rango
        // Retorna: Lista de aprendices (solo información básica: id, cédula, nombre, correo, teléfono).
        public List<Aprendiz> ObtenerAprendicesPorFecha(DateTime desde, DateTime hasta)
        {
            List<Aprendiz> lista = new List<Aprendiz>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                // JOIN con Inscripcion para filtrar por fecha de inscripción
                string query = @"SELECT DISTINCT a.* FROM Aprendiz a
                         INNER JOIN Inscripcion i ON a.idAprendiz = i.idAprendiz
                         WHERE i.fechaInscripcion >= @desde
                         AND i.fechaInscripcion <= @hasta";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", hasta);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Solo se asignan campos básicos para el listado
                            lista.Add(new Aprendiz()
                            {
                                idAprendiz = Convert.ToInt32(reader["idAprendiz"]),
                                cedulaAprendiz = reader["cedulaAprendiz"].ToString(),
                                nombreAprendiz = reader["nombreAprendiz"].ToString(),
                                correoAprendiz = reader["correoAprendiz"].ToString(),
                                telefonoAprendiz = reader["telefonoAprendiz"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}