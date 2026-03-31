// ============================================================
// Archivo: EventoDAO.cs
// Propósito: Maneja todas las operaciones de acceso a datos 
//            relacionadas con los eventos.
// Contiene métodos para crear, listar, actualizar, desactivar 
//            (soft delete) y filtrar eventos por rango de fechas.
// También se encarga de cancelar inscripciones asociadas al 
//            desactivar un evento.
// ============================================================

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
        // Inserta un nuevo evento en la base de datos.
        // Parámetro: evento - Objeto Evento con los datos a registrar.
        // Retorna: true si la inserción fue exitosa, false en caso contrario.
        public bool CrearEvento(Evento evento)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                // Consulta de inserción con todos los campos de la tabla Evento
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
                    // Asignación de parámetros con los valores del objeto evento
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

                    // ExecuteNonQuery retorna el número de filas afectadas.
                    // Si es mayor a 0, la inserción fue exitosa.
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // ======================================
        // LISTAR EVENTOS
        // ======================================
        // Retorna una lista con todos los eventos almacenados.
        public List<Evento> ObtenerEventos()
        {
            List<Evento> lista = new List<Evento>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = "SELECT * FROM Evento"; // Obtiene todas las columnas

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Recorre cada fila y construye un objeto Evento
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
        // ACTUALIZAR EVENTO
        // ======================================
        // Modifica los datos de un evento existente.
        // Parámetro: evento - Objeto Evento con los nuevos valores (debe tener idEvento válido).
        // Retorna: true si la actualización afectó al menos una fila, false si no se encontró el evento.
        public bool ActualizarEvento(Evento evento)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                // Actualiza todos los campos excepto idEvento, fechaFinEvento e idUsuarioCreador (no se incluye fechaFinEvento en la actualización por diseño original)
                string query = @"UPDATE Evento SET
                        nombreEvento = @nombre,
                        tipoEvento = @tipo,
                        lugarEvento = @lugar,
                        descripcionEvento = @descripcion,
                        fechaInicioEvento = @fechaInicio,
                        fechaInicioInscripcion = @inicioInscripcion,
                        fechaFinInscripcion = @finInscripcion,
                        cupoMaximo = @cupo,
                        activo = @activo
                        WHERE idEvento = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", evento.nombreEvento);
                    cmd.Parameters.AddWithValue("@tipo", evento.tipoEvento);
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

        // ======================================
        // DESACTIVAR EVENTO (SOFT DELETE)
        // ======================================
        // No elimina el evento físicamente, solo lo marca como inactivo (activo = 0).
        // Además, cancela todas las inscripciones asociadas a ese evento.
        // Parámetro: idEvento - Identificador del evento a desactivar.
        // Retorna: true si se desactivó correctamente, false si no se encontró el evento.
        public bool DesactivarEvento(int idEvento)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                // Paso 1: Cancelar todas las inscripciones cuyo idEvento coincida
                string queryCancelar = @"UPDATE Inscripcion
                                 SET estadoInscripcion = 'Cancelado'
                                 WHERE idEvento = @id";

                using (SqlCommand cmdCancelar = new SqlCommand(queryCancelar, conexion))
                {
                    cmdCancelar.Parameters.AddWithValue("@id", idEvento);
                    cmdCancelar.ExecuteNonQuery(); // No importa si afecta 0 filas
                }

                // Paso 2: Desactivar el evento (activo = 0)
                string queryDesactivar = @"UPDATE Evento
                                   SET activo = 0
                                   WHERE idEvento = @id";

                using (SqlCommand cmdDesactivar = new SqlCommand(queryDesactivar, conexion))
                {
                    cmdDesactivar.Parameters.AddWithValue("@id", idEvento);
                    return cmdDesactivar.ExecuteNonQuery() > 0;
                }
            }
        }

        // ======================================
        // OBTENER EVENTOS POR RANGO DE FECHAS
        // ======================================
        // Filtra eventos cuya fecha de inicio esté dentro del rango [desde, hasta].
        // Parámetros:
        //   desde - Fecha límite inferior (incluida)
        //   hasta - Fecha límite superior (incluida)
        // Retorna: Lista de eventos que cumplen la condición (solo algunos campos básicos).
        public List<Evento> ObtenerEventosPorFecha(DateTime desde, DateTime hasta)
        {
            List<Evento> lista = new List<Evento>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT * FROM Evento
                         WHERE fechaInicioEvento >= @desde
                         AND fechaInicioEvento <= @hasta";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@desde", desde);
                    cmd.Parameters.AddWithValue("@hasta", hasta);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Solo se asignan algunos campos relevantes para el listado rápido
                            lista.Add(new Evento()
                            {
                                idEvento = Convert.ToInt32(reader["idEvento"]),
                                nombreEvento = reader["nombreEvento"].ToString(),
                                tipoEvento = reader["tipoEvento"].ToString(),
                                lugarEvento = reader["lugarEvento"].ToString(),
                                fechaInicioEvento = Convert.ToDateTime(reader["fechaInicioEvento"]),
                                cupoMaximo = Convert.ToInt32(reader["cupoMaximo"]),
                                activo = Convert.ToBoolean(reader["activo"])
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}