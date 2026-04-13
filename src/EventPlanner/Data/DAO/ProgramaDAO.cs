// ============================================================
// Archivo: ProgramaDAO.cs
// DAO limpio, reutilizable y mantenible
// ============================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class ProgramaDAO
    {
        // ===============================
        // OBTENER TODOS
        // ===============================
        public List<Programa> ObtenerProgramas()
        {
            List<Programa> lista = new List<Programa>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT idPrograma, codigoPrograma, nombrePrograma, 
                                        duracionPrograma, nivelPrograma 
                                 FROM Programa";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(MapearPrograma(reader));
                    }
                }
            }

            return lista;
        }

        // ===============================
        // OBTENER POR ID
        // ===============================
        public Programa ObtenerPorId(int idPrograma)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT idPrograma, codigoPrograma, nombrePrograma, 
                                        duracionPrograma, nivelPrograma 
                                 FROM Programa
                                 WHERE idPrograma = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = idPrograma;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearPrograma(reader);
                        }
                    }
                }
            }

            return null;
        }

        // ===============================
        // MAPPER PRIVADO ⭐ (CLAVE)
        // ===============================
        private Programa MapearPrograma(SqlDataReader reader)
        {
            return new Programa
            {
                idPrograma = reader.GetInt32(0),
                codigoPrograma = reader.GetInt32(1),
                nombrePrograma = reader.GetString(2),
                duracionPrograma = reader.GetString(3),
                nivelPrograma = reader.GetString(4)
            };
        }
    }
}