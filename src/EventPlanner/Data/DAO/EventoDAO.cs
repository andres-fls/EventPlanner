using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class EventoDAO
    {
        // ======================================
        // CREAR EVENTO
        // ======================================
        public bool CrearEvento(Evento evento)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"INSERT INTO Evento
                (nombreEvento, tipoEvento, lugarEvento, descripcionEvento,
                 fechaInicioEvento, fechaFinEvento,
                 fechaInicioInscripcion, fechaFinInscripcion,
                 cupoMaximo, activo, idUsuarioCreador)
                VALUES
                (@nombre, @tipo, @lugar, @descripcion,
                 @fechaInicio, @fechaFin,
                 @inicioInscripcion, @finInscripcion,
                 @cupo, @activo, @usuario)";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", evento.nombreEvento);
                    cmd.Parameters.AddWithValue("@tipo", evento.tipoEvento);
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

        // ======================================
        // LISTAR EVENTOS
        // ======================================
        public List<Evento> ObtenerEventos()
        {
            List<Evento> lista = new List<Evento>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = "SELECT * FROM Evento";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Evento e = new Evento()
                        {
                            idEvento = Convert.ToInt32(reader["idEvento"]),
                            nombreEvento = reader["nombreEvento"].ToString(),
                            tipoEvento = reader["tipoEvento"].ToString(),
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

                        lista.Add(e);
                    }
                }
            }

            return lista;
        }

        // ======================================
        // DESACTIVAR EVENTO (SOFT DELETE)
        // ======================================
        public bool DesactivarEvento(int idEvento)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"UPDATE Evento
                                 SET activo = 0
                                 WHERE idEvento = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idEvento);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}