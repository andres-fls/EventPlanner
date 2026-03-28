using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class GrupoDAO
    {
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
                        Grupo grupo = new Grupo()
                        {
                            idGrupo = Convert.ToInt32(reader["idGrupo"]),
                            nombreGrupo = reader["nombreGrupo"].ToString()
                        };

                        lista.Add(grupo);
                    }
                }
            }

            return lista;
        }
    }
}