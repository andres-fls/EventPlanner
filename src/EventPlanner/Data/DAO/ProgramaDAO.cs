// ============================================================
// Archivo: ProgramaDAO.cs
// Propósito: Maneja las operaciones de acceso a datos para 
//            la entidad Programa (programas académicos).
// Contiene métodos para obtener todos los programas y 
//            para buscar un programa por su ID.
// ============================================================

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class ProgramaDAO
    {
        // ===============================
        // OBTENER TODOS LOS PROGRAMAS
        // ===============================
        // Retorna una lista con todos los programas registrados en la base de datos.
        public List<Programa> ObtenerProgramas()
        {
            List<Programa> lista = new List<Programa>(); // Lista vacía para almacenar resultados

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open(); // Abre conexión a BD

                // Consulta SQL: selecciona todas las columnas de la tabla Programa
                string query = "SELECT * FROM Programa";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader()) // Ejecuta y obtiene lector de datos
                {
                    // Recorre cada fila del resultado
                    while (reader.Read())
                    {
                        // Crea un objeto Programa y asigna los valores de cada columna
                        Programa programa = new Programa()
                        {
                            idPrograma = Convert.ToInt32(reader["idPrograma"]),
                            codigoPrograma = Convert.ToInt32(reader["codigoPrograma"]),
                            nombrePrograma = reader["nombrePrograma"].ToString(),
                            duracionPrograma = reader["duracionPrograma"].ToString(),
                            nivelPrograma = reader["nivelPrograma"].ToString()
                        };

                        lista.Add(programa); // Agrega el programa a la lista
                    }
                }
            }

            return lista; // Retorna la lista completa
        }

        // ===============================
        // OBTENER PROGRAMA POR ID
        // ===============================
        // Busca y retorna un programa específico según su identificador único.
        // Parámetro: idPrograma - ID del programa a buscar.
        // Retorna: Objeto Programa si se encuentra, o null si no existe.
        public Programa ObtenerPorId(int idPrograma)
        {
            Programa programa = null; // Inicializa como null (no encontrado)

            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open();

                // Consulta parametrizada para evitar inyección SQL
                string query = @"SELECT * 
                                 FROM Programa 
                                 WHERE idPrograma = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    // Reemplaza @id con el valor recibido
                    cmd.Parameters.AddWithValue("@id", idPrograma);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Si se encontró al menos una fila
                        if (reader.Read())
                        {
                            // Construye el objeto Programa con los datos leídos
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

            return programa; // Retorna el programa encontrado o null
        }
    }
}