using EventPlanner.Data;
using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EventPlanner.DAO
{
    public class InscripcionDAO
    {
        private Conexion conexion = new Conexion();

        // ==============================
        // CREAR INSCRIPCIÓN
        // ==============================
        public void CrearInscripcion(Inscripcion inscripcion)
        {
            using (SqlConnection conn = conexion.Conectar())
            {
                string query = @"INSERT INTO Inscripcion
                                (tipoInscripcion, modalidad, idAprendiz, idEvento, idGrupo)
                                VALUES
                                (@tipo, @modalidad, @idAprendiz, @idEvento, @idGrupo)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@tipo", inscripcion.tipoInscripcion);
                cmd.Parameters.AddWithValue("@modalidad", inscripcion.modalidad);
                cmd.Parameters.AddWithValue("@idAprendiz", inscripcion.idAprendiz);
                cmd.Parameters.AddWithValue("@idEvento", inscripcion.idEvento);

                if (inscripcion.idGrupo.HasValue)
                    cmd.Parameters.AddWithValue("@idGrupo", inscripcion.idGrupo.Value);
                else
                    cmd.Parameters.AddWithValue("@idGrupo", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ==============================
        // OBTENER TODAS
        // ==============================
        public List<Inscripcion> ObtenerInscripciones()
        {
            List<Inscripcion> lista = new List<Inscripcion>();

            using (SqlConnection conn = conexion.Conectar())
            {
                string query = "SELECT * FROM Inscripcion";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Inscripcion i = new Inscripcion();

                    i.idInscripcion = (int)reader["idInscripcion"];
                    i.fechaInscripcion = (DateTime)reader["fechaInscripcion"];
                    i.tipoInscripcion = reader["tipoInscripcion"].ToString();
                    i.modalidad = reader["modalidad"].ToString();
                    i.estadoInscripcion = reader["estadoInscripcion"].ToString();
                    i.idAprendiz = (int)reader["idAprendiz"];
                    i.idEvento = (int)reader["idEvento"];

                    if (reader["idGrupo"] != DBNull.Value)
                        i.idGrupo = (int)reader["idGrupo"];

                    lista.Add(i);
                }
            }

            return lista;
        }

        // ==============================
        // CANCELAR INSCRIPCIÓN (SOFT DELETE)
        // ==============================
        public void CancelarInscripcion(int idInscripcion)
        {
            using (SqlConnection conn = conexion.Conectar())
            {
                string query = @"UPDATE Inscripcion
                                 SET estadoInscripcion = 'Cancelado'
                                 WHERE idInscripcion = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idInscripcion);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ==============================
        // OBTENER INSCRIPCIONES CON DETALLE
        // ==============================
        public List<InscripcionDetalle> ObtenerInscripcionesConDetalle()
        {
            List<InscripcionDetalle> lista = new List<InscripcionDetalle>();

            using (SqlConnection conn = conexion.Conectar())
            {
                string query = @"SELECT i.idInscripcion, a.nombreAprendiz, e.nombreEvento,
                                        i.modalidad, i.estadoInscripcion
                                 FROM Inscripcion i
                                 INNER JOIN Aprendiz a ON i.idAprendiz = a.idAprendiz
                                 INNER JOIN Evento e ON i.idEvento = e.idEvento";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    InscripcionDetalle detalle = new InscripcionDetalle()
                    {
                        idInscripcion = (int)reader["idInscripcion"],
                        nombreAprendiz = reader["nombreAprendiz"].ToString(),
                        nombreEvento = reader["nombreEvento"].ToString(),
                        modalidad = reader["modalidad"].ToString(),
                        estadoInscripcion = reader["estadoInscripcion"].ToString()
                    };

                    lista.Add(detalle);
                }
            }

            return lista;
        }

        // ==============================
        // ACTUALIZAR ESTADO
        // ==============================
        public void ActualizarEstado(int idInscripcion, string nuevoEstado)
        {
            using (SqlConnection conn = conexion.Conectar())
            {
                string query = @"UPDATE Inscripcion 
                                 SET estadoInscripcion = @estado 
                                 WHERE idInscripcion = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@id", idInscripcion);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}