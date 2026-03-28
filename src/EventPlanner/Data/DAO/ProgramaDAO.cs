using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EventPlanner.Models;

namespace EventPlanner.Data
{
    public class ProgramaDAO
    {
        // ===============================
        // OBTENER TODOS LOS PROGRAMAS
        // ===============================
        public List<Programa> ObtenerProgramas()
        {
            List<Programa> lista = new List<Programa>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = "SELECT * FROM Programa";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Programa programa = new Programa()
                        {
                            idPrograma = Convert.ToInt32(reader["idPrograma"]),
                            codigoPrograma = Convert.ToInt32(reader["codigoPrograma"]),
                            nombrePrograma = reader["nombrePrograma"].ToString(),
                            duracionPrograma = reader["duracionPrograma"].ToString(),
                            nivelPrograma = reader["nivelPrograma"].ToString()
                        };

                        lista.Add(programa);
                    }
                }
            }

            return lista;
        }

        // ===============================
        // OBTENER PROGRAMA POR ID
        // ===============================
        public Programa ObtenerPorId(int idPrograma)
        {
            Programa programa = null;

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT * 
                                 FROM Programa 
                                 WHERE idPrograma = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idPrograma);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            programa = new Programa()
                            {
                                idPrograma = Convert.ToInt32(reader["idPrograma"]),
                                codigoPrograma = Convert.ToInt32(reader["codigoPrograma"]),
                                nombrePrograma = reader["nombrePrograma"].ToString(),
                                duracionPrograma = reader["duracionPrograma"].ToString(),
                                nivelPrograma = reader["nivelPrograma"].ToString()
                            };
                        }
                    }
                }
            }

            return programa;
        }
    }
}