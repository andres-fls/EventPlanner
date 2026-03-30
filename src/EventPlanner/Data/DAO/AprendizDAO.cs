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
        public bool CrearAprendiz(Aprendiz aprendiz)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

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
                    cmd.Parameters.AddWithValue("@cedula", aprendiz.cedulaAprendiz);
                    cmd.Parameters.AddWithValue("@nombre", aprendiz.nombreAprendiz);
                    cmd.Parameters.AddWithValue("@edad", aprendiz.edadAprendiz);
                    cmd.Parameters.AddWithValue("@genero", aprendiz.generoAprendiz);
                    cmd.Parameters.AddWithValue("@correo", aprendiz.correoAprendiz);
                    cmd.Parameters.AddWithValue("@telefono", aprendiz.telefonoAprendiz);
                    cmd.Parameters.AddWithValue("@ficha", aprendiz.codigoFicha);
                    cmd.Parameters.AddWithValue("@usuario", aprendiz.idUsuario);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // =============================
        // LISTAR APRENDICES
        // =============================
        public List<Aprendiz> ObtenerAprendices()
        {
            List<Aprendiz> lista = new List<Aprendiz>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = "SELECT * FROM Aprendiz";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
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

            return null;
        }

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
        public List<Aprendiz> ObtenerAprendicesPorFecha(DateTime desde, DateTime hasta)
        {
            List<Aprendiz> lista = new List<Aprendiz>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

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