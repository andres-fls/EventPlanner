// ============================================================
// Archivo: InscripcionDAO.cs
// Propósito: Maneja todas las operaciones de acceso a datos 
//            relacionadas con las inscripciones de aprendices a eventos.
// Incluye métodos para crear inscripciones, listarlas, cancelarlas 
//            (soft delete), obtener detalles con nombres y actualizar estados.
// ============================================================

using EventPlanner.Data;
using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EventPlanner.DAO
{
    public class InscripcionDAO
    {
        // Objeto de conexión reutilizable (se instancia una vez)
        private Conexion conexion = new Conexion();

        // ==============================
        // CREAR INSCRIPCIÓN
        // ==============================
        // Inserta una nueva inscripción en la base de datos.
        // Parámetro: inscripcion - Objeto con los datos de la inscripción.
        // Nota: idGrupo puede ser null (inscripción individual), por eso se maneja DBNull.
        public void CrearInscripcion(Inscripcion inscripcion)
        {
            using (SqlConnection conn = conexion.Conectar())
            {
                // Consulta de inserción con todos los campos requeridos
                string query = @"INSERT INTO Inscripcion
                                (tipoInscripcion, modalidad, idAprendiz, idEvento, idGrupo)
                                VALUES
                                (@tipo, @modalidad, @idAprendiz, @idEvento, @idGrupo)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Asignación de parámetros con los valores del objeto
                cmd.Parameters.AddWithValue("@tipo", inscripcion.tipoInscripcion);
                cmd.Parameters.AddWithValue("@modalidad", inscripcion.modalidad);
                cmd.Parameters.AddWithValue("@idAprendiz", inscripcion.idAprendiz);
                cmd.Parameters.AddWithValue("@idEvento", inscripcion.idEvento);

                // Si idGrupo tiene valor (no es null), se usa ese valor; de lo contrario se envía DBNull (NULL en BD)
                if (inscripcion.idGrupo.HasValue)
                    cmd.Parameters.AddWithValue("@idGrupo", inscripcion.idGrupo.Value);
                else
                    cmd.Parameters.AddWithValue("@idGrupo", DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery(); // Ejecuta la inserción (no retorna filas)
            }
        }

        // ==============================
        // OBTENER TODAS LAS INSCRIPCIONES
        // ==============================
        // Retorna una lista con todas las inscripciones (sin detalles de nombres, solo IDs).
        public List<Inscripcion> ObtenerInscripciones()
        {
            List<Inscripcion> lista = new List<Inscripcion>();

            using (SqlConnection conn = conexion.Conectar())
            {
                string query = "SELECT * FROM Inscripcion";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                // Recorre cada fila y construye el objeto Inscripcion
                while (reader.Read())
                {
                    Inscripcion i = new Inscripcion();

                    // Asigna valores básicos
                    i.idInscripcion = (int)reader["idInscripcion"];
                    i.fechaInscripcion = (DateTime)reader["fechaInscripcion"];
                    i.tipoInscripcion = reader["tipoInscripcion"].ToString();
                    i.modalidad = reader["modalidad"].ToString();
                    i.estadoInscripcion = reader["estadoInscripcion"].ToString();
                    i.idAprendiz = (int)reader["idAprendiz"];
                    i.idEvento = (int)reader["idEvento"];

                    // idGrupo puede ser NULL en BD, por eso se verifica DBNull
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
        // No elimina físicamente el registro, solo cambia su estado a 'Cancelado'.
        // Parámetro: idInscripcion - Identificador de la inscripción a cancelar.
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
                cmd.ExecuteNonQuery(); // Ejecuta la actualización
            }
        }

        // ==============================
        // OBTENER INSCRIPCIONES CON DETALLE
        // ==============================
        // Retorna una lista con información combinada de inscripciones,
        // incluyendo el nombre del aprendiz y el nombre del evento (JOINs).
        // Útil para mostrar en reportes o listados con nombres legibles.
        public List<InscripcionDetalle> ObtenerInscripcionesConDetalle()
        {
            List<InscripcionDetalle> lista = new List<InscripcionDetalle>();

            using (SqlConnection conn = conexion.Conectar())
            {
                // Consulta con INNER JOIN para traer nombres desde las tablas relacionadas
                string query = @"SELECT i.idInscripcion, i.idAprendiz, i.idEvento,
                                        a.nombreAprendiz, e.nombreEvento,
                                        i.modalidad, i.estadoInscripcion
                                 FROM Inscripcion i
                                 INNER JOIN Aprendiz a ON i.idAprendiz = a.idAprendiz
                                 INNER JOIN Evento e ON i.idEvento = e.idEvento";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                // Construye objetos InscripcionDetalle con los datos combinados
                while (reader.Read())
                {
                    InscripcionDetalle detalle = new InscripcionDetalle()
                    {
                        idInscripcion = (int)reader["idInscripcion"],
                        idAprendiz = (int)reader["idAprendiz"],
                        idEvento = (int)reader["idEvento"],
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
        // ACTUALIZAR ESTADO DE INSCRIPCIÓN
        // ==============================
        // Cambia el estado de una inscripción (ej: 'Confirmado', 'Cancelado', 'Pendiente').
        // Parámetros:
        //   idInscripcion - ID de la inscripción a modificar.
        //   nuevoEstado   - Nuevo valor para el campo estadoInscripcion.
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

        // ==============================
        // VERIFICAR CRUCE DE HORARIOS
        // ==============================
        public bool TieneCruceHorario(int idAprendiz, DateTime fechaInicioNuevo)
        {
            using (SqlConnection conn = conexion.Conectar())
            {
                string query = @"SELECT COUNT(*) FROM Inscripcion i
                         INNER JOIN Evento e ON i.idEvento = e.idEvento
                         WHERE i.idAprendiz = @idAprendiz
                         AND i.estadoInscripcion = 'Activo'
                         AND e.fechaInicioEvento = @fechaInicio";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idAprendiz", idAprendiz);
                cmd.Parameters.AddWithValue("@fechaInicio", fechaInicioNuevo);

                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

    }
}