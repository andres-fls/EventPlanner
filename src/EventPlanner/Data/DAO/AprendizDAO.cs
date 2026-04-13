using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class AprendizDAO
    {
        // =====================================================
        // MÉTODO PRIVADO DE MAPEO (EVITA DUPLICACIÓN)
        // =====================================================
        private Aprendiz Mapear(SqlDataReader reader)
        {
            return new Aprendiz
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

        // =============================
        // CREAR APRENDIZ
        // =============================
        public bool CrearAprendiz(Aprendiz aprendiz)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
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
                    cmd.Parameters.Add("@cedula", SqlDbType.VarChar).Value = aprendiz.cedulaAprendiz;
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = aprendiz.nombreAprendiz;
                    cmd.Parameters.Add("@edad", SqlDbType.Int).Value = aprendiz.edadAprendiz;
                    cmd.Parameters.Add("@genero", SqlDbType.VarChar).Value = aprendiz.generoAprendiz;
                    cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = aprendiz.correoAprendiz;
                    cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = aprendiz.telefonoAprendiz;
                    cmd.Parameters.Add("@ficha", SqlDbType.Int).Value = aprendiz.codigoFicha;
                    cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = aprendiz.idUsuario;

                    conexion.Open();
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
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Aprendiz", conexion))
            {
                conexion.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(Mapear(reader));
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
            using (SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Aprendiz WHERE cedulaAprendiz = @cedula", conexion))
            {
                cmd.Parameters.Add("@cedula", SqlDbType.VarChar).Value = cedula;

                conexion.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return Mapear(reader);
                }
            }

            return null;
        }

        // =============================
        // BUSCAR POR ID USUARIO
        // =============================
        public Aprendiz BuscarPorIdUsuario(int idUsuario)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Aprendiz WHERE idUsuario = @id", conexion))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario;

                conexion.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return Mapear(reader);
                }
            }

            return null;
        }

        // =============================
        // APRENDICES POR FECHA
        // =============================
        public List<Aprendiz> ObtenerAprendicesPorFecha(DateTime desde, DateTime hasta)
        {
            List<Aprendiz> lista = new List<Aprendiz>();

            using (SqlConnection conexion = new Conexion().Conectar())
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT DISTINCT a.*
                FROM Aprendiz a
                INNER JOIN Inscripcion i ON a.idAprendiz = i.idAprendiz
                WHERE i.fechaInscripcion BETWEEN @desde AND @hasta", conexion))
            {
                cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = desde;
                cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = hasta;

                conexion.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(Mapear(reader));
                }
            }

            return lista;
        }
    }
}