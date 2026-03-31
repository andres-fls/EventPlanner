// ============================================================
// Archivo: GrupoDAO.cs
// Propósito: Maneja las operaciones de acceso a datos para 
//            la entidad Grupo.
// Contiene métodos para obtener la lista de grupos disponibles
//            desde la base de datos.
// ============================================================

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EventPlanner.Models;
using EventPlanner.Data;

namespace EventPlanner.DAO
{
    public class GrupoDAO
    {
        // ===============================
        // OBTENER TODOS LOS GRUPOS
        // ===============================
        // Retorna una lista con todos los grupos registrados.
        // Cada grupo contiene idGrupo y nombreGrupo.
        public List<Grupo> ObtenerGrupos()
        {
            // Lista vacía para almacenar los resultados
            List<Grupo> lista = new List<Grupo>();

            // Establece conexión a la base de datos usando la clase Conexion
            using (SqlConnection conexion = new Conexion().Conectar())
            {
                conexion.Open(); // Abre la conexión

                // Consulta SQL: selecciona id y nombre de todos los grupos
                string query = @"SELECT idGrupo, nombreGrupo
                                 FROM Grupo";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader()) // Ejecuta y obtiene el lector
                {
                    // Recorre cada fila del resultado
                    while (reader.Read())
                    {
                        // Crea un objeto Grupo y asigna los valores leídos
                        Grupo grupo = new Grupo()
                        {
                            idGrupo = Convert.ToInt32(reader["idGrupo"]),
                            nombreGrupo = reader["nombreGrupo"].ToString()
                        };

                        lista.Add(grupo); // Agrega a la lista
                    }
                }
            }

            return lista; // Retorna la lista completa
        }
    }
}