// ============================================================
// Archivo: InscripcionDAO.cs
// Acceso a datos para Inscripciones
// ============================================================

using EventPlanner.Data;
using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EventPlanner.DAO
{
    public class InscripcionDAO
    {
        // =====================================================
        // CREAR INSCRIPCION
        // =====================================================
        public void CrearInscripcion(Inscripcion inscripcion)
        {
            using (SqlConnection conn = new Conexion().Conectar())
            {
                conn.Open();

                string query = @"
                INSERT INTO Inscripcion
                (tipoInscripcion, modalidad, idAprendiz, idEvento, idGrupo)
                VALUES
                (@tipo,@modalidad,@aprendiz,@evento,@grupo)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = inscripcion.tipoInscripcion;
                    cmd.Parameters.Add("@modalidad", SqlDbType.VarChar).Value = inscripcion.modalidad;
                    cmd.Parameters.Add("@aprendiz", SqlDbType.Int).Value = inscripcion.idAprendiz;
                    cmd.Parameters.Add("@evento", SqlDbType.Int).Value = inscripcion.idEvento;

                    cmd.Parameters.Add("@grupo", SqlDbType.Int);
                    if (inscripcion.idGrupo.HasValue)
                        cmd.Parameters["@grupo"].Value = inscripcion.idGrupo.Value;
                    else
                        cmd.Parameters["@grupo"].Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =====================================================
        // OBTENER INSCRIPCIONES
        // =====================================================
        public List<Inscripcion> ObtenerInscripciones()
        {
            List<Inscripcion> lista = new List<Inscripcion>();

            using (SqlConnection conn = new Conexion().Conectar())
            {
                conn.Open();

                string query = @"
                SELECT idInscripcion,fechaInscripcion,
                       tipoInscripcion,modalidad,estadoInscripcion,
                       idAprendiz,idEvento,idGrupo
                FROM Inscripcion";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(MapearInscripcion(reader));
                }
            }

            return lista;
        }

        // =====================================================
        // CANCELAR (SOFT DELETE CORRECTO)
        // =====================================================
        public void CancelarInscripcion(int idInscripcion)
        {
            using (SqlConnection conn = new Conexion().Conectar())
            {
                conn.Open();

                string query = @"
                UPDATE Inscripcion 
                SET estadoInscripcion = 'Cancelado' 
                WHERE idInscripcion = @id
                AND estadoInscripcion = 'Activo'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = idInscripcion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =====================================================
        // DETALLE CON JOIN
        // =====================================================
        public List<InscripcionDetalle> ObtenerInscripcionesConDetalle()
        {
            List<InscripcionDetalle> lista = new List<InscripcionDetalle>();

            using (SqlConnection conn = new Conexion().Conectar())
            {
                conn.Open();

                string query = @"
                SELECT i.idInscripcion,
                       i.idAprendiz,
                       i.idEvento,
                       a.nombreAprendiz,
                       e.nombreEvento,
                       i.modalidad,
                       i.estadoInscripcion
                FROM Inscripcion i
                INNER JOIN Aprendiz a ON a.idAprendiz = i.idAprendiz
                INNER JOIN Evento e ON e.idEvento = i.idEvento";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new InscripcionDetalle
                        {
                            idInscripcion = reader.GetInt32(0),
                            idAprendiz = reader.GetInt32(1),
                            idEvento = reader.GetInt32(2),
                            nombreAprendiz = reader.GetString(3),
                            nombreEvento = reader.GetString(4),
                            modalidad = reader.GetString(5),
                            estadoInscripcion = reader.GetString(6)
                        });
                    }
                }
            }

            return lista;
        }

        // =====================================================
        // ACTUALIZAR ESTADO
        // =====================================================
        public void ActualizarEstado(int idInscripcion, string estado)
        {
            using (SqlConnection conn = new Conexion().Conectar())
            {
                conn.Open();

                string query = @"
                UPDATE Inscripcion 
                SET estadoInscripcion = @estado 
                WHERE idInscripcion = @id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@estado", SqlDbType.VarChar).Value = estado;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = idInscripcion;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =====================================================
        // CRUCE DE HORARIOS
        // =====================================================
        public bool TieneCruceHorario(int idAprendiz, DateTime fechaEvento)
        {
            using (SqlConnection conn = new Conexion().Conectar())
            {
                conn.Open();

                string query = @"
                SELECT COUNT(*)
                FROM Inscripcion i
                INNER JOIN Evento e ON e.idEvento = i.idEvento
                WHERE i.idAprendiz = @aprendiz
                AND i.estadoInscripcion = 'Activo'
                AND ABS(DATEDIFF(MINUTE, e.fechaInicioEvento, @fecha)) < 60";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@aprendiz", SqlDbType.Int).Value = idAprendiz;
                    cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fechaEvento;

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        // =====================================================
        // VALIDAR SI YA EXISTE INSCRIPCIÓN
        // =====================================================
        public bool ExisteInscripcion(int idAprendiz, int idEvento)
        {
            using (SqlConnection conn = new Conexion().Conectar())
            {
                conn.Open();

                string query = @"
                SELECT COUNT(*)
                FROM Inscripcion
                WHERE idAprendiz = @aprendiz
                AND idEvento = @evento
                AND estadoInscripcion = 'Activo'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@aprendiz", SqlDbType.Int).Value = idAprendiz;
                    cmd.Parameters.Add("@evento", SqlDbType.Int).Value = idEvento;

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        // =====================================================
        // MAPPER PRIVADO
        // =====================================================
        private Inscripcion MapearInscripcion(SqlDataReader reader)
        {
            Inscripcion inscripcion = new Inscripcion();

            inscripcion.idInscripcion = reader.GetInt32(0);
            inscripcion.fechaInscripcion = reader.GetDateTime(1);
            inscripcion.tipoInscripcion = reader.GetString(2);
            inscripcion.modalidad = reader.GetString(3);
            inscripcion.estadoInscripcion = reader.GetString(4);
            inscripcion.idAprendiz = reader.GetInt32(5);
            inscripcion.idEvento = reader.GetInt32(6);

            if (!reader.IsDBNull(7))
                inscripcion.idGrupo = reader.GetInt32(7);
            else
                inscripcion.idGrupo = null;

            return inscripcion;
        }
    }
}