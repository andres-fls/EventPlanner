// ============================================================
// Archivo: FichaDAO.cs
// DAO limpio, completo y reutilizable
// ============================================================

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class FichaDAO
    {
        // ===============================
        // OBTENER TODAS
        // ===============================
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
                        lista.Add(MapearFicha(reader));
                    }
                }
            }

            return lista;
        }

        // ===============================
        // OBTENER POR CÓDIGO
        // ===============================
        public Ficha ObtenerPorCodigo(int codigoFicha)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT codigoFicha, idPrograma 
                                 FROM Ficha
                                 WHERE codigoFicha = @codigo";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = codigoFicha;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearFicha(reader);
                        }
                    }
                }
            }

            return null;
        }

        // ===============================
        // EXISTE FICHA
        // ===============================
        public bool ExisteFicha(int codigoFicha)
        {
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                string query = @"SELECT COUNT(*) 
                                 FROM Ficha
                                 WHERE codigoFicha = @codigo";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = codigoFicha;

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        // ===============================
        // MAPPER PRIVADO ⭐
        // ===============================
        private Ficha MapearFicha(SqlDataReader reader)
        {
            return new Ficha
            {
                codigoFicha = reader.GetInt32(0),
                idPrograma = reader.GetInt32(1)
            };
        }
    }
}