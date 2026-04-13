// ============================================================
// Archivo: GrupoDAO.cs
// DAO completo y mantenible
// ============================================================

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class GrupoDAO
    {
        // ===============================
        // OBTENER TODOS
        // ===============================
        public List<Grupo> ObtenerGrupos()
        {
            List<Grupo> lista = new List<Grupo>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT idGrupo, nombreGrupo 
                                 FROM Grupo";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(MapearGrupo(reader));
                    }
                }
            }

            return lista;
        }

        // ===============================
        // OBTENER POR ID
        // ===============================
        public Grupo ObtenerPorId(int idGrupo)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT idGrupo, nombreGrupo 
                                 FROM Grupo
                                 WHERE idGrupo = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = idGrupo;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearGrupo(reader);
                        }
                    }
                }
            }

            return null;
        }

        // ===============================
        // EXISTE GRUPO
        // ===============================
        public bool ExisteGrupo(int idGrupo)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT COUNT(*) 
                                 FROM Grupo
                                 WHERE idGrupo = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = idGrupo;

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        // ===============================
        // MAPPER PRIVADO ⭐
        // ===============================
        private Grupo MapearGrupo(SqlDataReader reader)
        {
            return new Grupo
            {
                idGrupo = reader.GetInt32(0),
                nombreGrupo = reader.GetString(1)
            };
        }
    }
}