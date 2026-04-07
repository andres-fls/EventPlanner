// ============================================================
// Archivo: FichaDAO.cs
// Propósito: Maneja las operaciones de acceso a datos para 
//            la entidad Ficha (fichas de formación académica).
// Contiene métodos para obtener la lista de fichas disponibles
//            desde la base de datos, incluyendo su código y el programa asociado.
// ============================================================

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class FichaDAO
    {
        // ===============================
        // OBTENER TODAS LAS FICHAS
        // ===============================
        // Retorna una lista con todas las fichas registradas.
        // Cada ficha contiene codigoFicha (identificador numérico) y idPrograma (programa al que pertenece).
        public List<Ficha> ObtenerFichas()
        {
            // Lista vacía para almacenar los resultados
            List<Ficha> lista = new List<Ficha>();

            // Establece conexión a la base de datos usando la clase Conexion
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open(); // Abre la conexión

                // Consulta SQL: selecciona código de ficha y programa asociado
                string query = @"SELECT codigoFicha, idPrograma
                                 FROM Ficha";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader()) // Ejecuta y obtiene el lector
                {
                    // Recorre cada fila del resultado
                    while (reader.Read())
                    {
                        // Crea un objeto Ficha y asigna los valores leídos
                        Ficha ficha = new Ficha()
                        {
                            codigoFicha = Convert.ToInt32(reader["codigoFicha"]),
                            idPrograma = Convert.ToInt32(reader["idPrograma"])
                        };

                        lista.Add(ficha); // Agrega a la lista
                    }
                }
            }

            return lista; // Retorna la lista completa
        }
    }
}