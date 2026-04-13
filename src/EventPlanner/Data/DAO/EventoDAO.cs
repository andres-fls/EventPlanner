// ============================================================
// Archivo: EventoDAO.cs
// Propósito: Acceso a datos para eventos.
// Contiene operaciones CRUD + soft delete con integridad.
// ============================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class EventoDAO
    {
        // =====================================================
        // CREAR EVENTO
        // =====================================================
        public bool CrearEvento(Evento evento)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"
                INSERT INTO Evento
                (nombreEvento, categoriaEvento, lugarEvento, descripcionEvento,
                 fechaInicioEvento, fechaFinEvento,
                 fechaInicioInscripcion, fechaFinInscripcion,
                 cupoMaximo, activo, idUsuarioCreador)
                VALUES
                (@nombre,@categoria,@lugar,@descripcion,
                 @fechaInicio,@fechaFin,
                 @inicioInscripcion,@finInscripcion,
                 @cupo,@activo,@usuario)";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", evento.nombreEvento);
                    cmd.Parameters.AddWithValue("@categoria", evento.categoriaEvento);
                    cmd.Parameters.AddWithValue("@lugar", evento.lugarEvento);
                    cmd.Parameters.AddWithValue("@descripcion", evento.descripcionEvento);

                    cmd.Parameters.AddWithValue("@fechaInicio", evento.fechaInicioEvento);
                    cmd.Parameters.AddWithValue("@fechaFin", evento.fechaFinEvento);

                    cmd.Parameters.AddWithValue("@inicioInscripcion", evento.fechaInicioInscripcion);
                    cmd.Parameters.AddWithValue("@finInscripcion", evento.fechaFinInscripcion);

                    cmd.Parameters.AddWithValue("@cupo", evento.cupoMaximo);
                    cmd.Parameters.AddWithValue("@activo", evento.activo);
                    cmd.Parameters.AddWithValue("@usuario", evento.idUsuarioCreador);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // =====================================================
        // OBTENER TODOS LOS EVENTOS
        // =====================================================
        public List<Evento> ObtenerEventos()
        {
            List<Evento> lista = new List<Evento>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"
                SELECT
                    idEvento,
                    nombreEvento,
                    categoriaEvento,
                    lugarEvento,
                    descripcionEvento,
                    fechaInicioEvento,
                    fechaFinEvento,
                    fechaInicioInscripcion,
                    fechaFinInscripcion,
                    cupoMaximo,
                    activo,
                    idUsuarioCreador
                FROM Evento";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        lista.Add(MapearEvento(reader));
                }
            }

            return lista;
        }

        // =====================================================
        // OBTENER EVENTO POR ID ⭐
        // =====================================================
        public Evento ObtenerEventoPorId(int idEvento)
        {
            Evento evento = null;

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = "SELECT * FROM Evento WHERE idEvento=@id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idEvento);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            evento = MapearEvento(reader);
                    }
                }
            }

            return evento;
        }

        // =====================================================
        // ACTUALIZAR EVENTO
        // =====================================================
        public bool ActualizarEvento(Evento evento)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"
                UPDATE Evento SET
                    nombreEvento=@nombre,
                    categoriaEvento=@categoria,
                    lugarEvento=@lugar,
                    descripcionEvento=@descripcion,
                    fechaInicioEvento=@fechaInicio,
                    fechaFinEvento=@fechaFin,
                    fechaInicioInscripcion=@inicioInscripcion,
                    fechaFinInscripcion=@finInscripcion,
                    cupoMaximo=@cupo,
                    activo=@activo
                WHERE idEvento=@id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", evento.nombreEvento);
                    cmd.Parameters.AddWithValue("@categoria", evento.categoriaEvento);
                    cmd.Parameters.AddWithValue("@lugar", evento.lugarEvento);
                    cmd.Parameters.AddWithValue("@descripcion", evento.descripcionEvento);
                    cmd.Parameters.AddWithValue("@fechaInicio", evento.fechaInicioEvento);
                    cmd.Parameters.AddWithValue("@inicioInscripcion", evento.fechaInicioInscripcion);
                    cmd.Parameters.AddWithValue("@finInscripcion", evento.fechaFinInscripcion);
                    cmd.Parameters.AddWithValue("@cupo", evento.cupoMaximo);
                    cmd.Parameters.AddWithValue("@activo", evento.activo);
                    cmd.Parameters.AddWithValue("@id", evento.idEvento);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // =====================================================
        // DESACTIVAR EVENTO (SOFT DELETE CON TRANSACTION) ⭐
        // =====================================================
        public bool DesactivarEvento(int idEvento)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                using (SqlTransaction tx = conexion.BeginTransaction())
                {
                    try
                    {
                        string cancelar = @"
                        UPDATE Inscripcion
                        SET estadoInscripcion='Cancelado'
                        WHERE idEvento=@id";

                        using (SqlCommand cmd = new SqlCommand(cancelar, conexion, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idEvento);
                            cmd.ExecuteNonQuery();
                        }

                        string desactivar = @"
                        UPDATE Evento
                        SET activo=0
                        WHERE idEvento=@id";

                        using (SqlCommand cmd = new SqlCommand(desactivar, conexion, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", idEvento);

                            bool ok = cmd.ExecuteNonQuery() > 0;

                            tx.Commit();
                            return ok;
                        }
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // =====================================================
        // EVENTOS POR RANGO DE FECHA
        // =====================================================
        public List<Evento> ObtenerEventosPorFecha(DateTime desde, DateTime hasta)
        {
            List<Evento> lista = new List<Evento>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"
                SELECT *
                FROM Evento
                WHERE fechaInicioEvento BETWEEN @desde AND @hasta";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", hasta);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            lista.Add(MapearEvento(reader));
                    }
                }
            }

            return lista;
        }

        // =====================================================
        // MAPPER PRIVADO ⭐ (EVITA DUPLICACIÓN)
        // =====================================================
        private Evento MapearEvento(SqlDataReader reader)
        {
            return new Evento()
            {
                idEvento = Convert.ToInt32(reader["idEvento"]),
                nombreEvento = reader["nombreEvento"].ToString(),
                categoriaEvento = reader["categoriaEvento"].ToString(),
                lugarEvento = reader["lugarEvento"].ToString(),
                descripcionEvento = reader["descripcionEvento"].ToString(),

                fechaInicioEvento = Convert.ToDateTime(reader["fechaInicioEvento"]),
                fechaFinEvento = Convert.ToDateTime(reader["fechaFinEvento"]),

                fechaInicioInscripcion = Convert.ToDateTime(reader["fechaInicioInscripcion"]),
                fechaFinInscripcion = Convert.ToDateTime(reader["fechaFinInscripcion"]),

                cupoMaximo = Convert.ToInt32(reader["cupoMaximo"]),
                activo = Convert.ToBoolean(reader["activo"]),
                idUsuarioCreador = Convert.ToInt32(reader["idUsuarioCreador"])
            };
        }
    }
}