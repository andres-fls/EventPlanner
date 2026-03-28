using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class FichaDAO
    {
        public List<Ficha> ObtenerFichas()
        {
            List<Ficha> lista = new List<Ficha>();

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT codigoFicha, idPrograma
                                 FROM Ficha";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ficha ficha = new Ficha()
                        {
                            codigoFicha = Convert.ToInt32(reader["codigoFicha"]),
                            idPrograma = Convert.ToInt32(reader["idPrograma"])
                        };

                        lista.Add(ficha);
                    }
                }
            }

            return lista;
        }
    }
}