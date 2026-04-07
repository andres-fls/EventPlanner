// ============================================================
// Archivo: ProgramaService.cs
// Propósito: Capa de servicio para la lógica de negocio
//            relacionada con los programas de formación.
// Contiene métodos para obtener la lista de todos los programas
//            y para buscar un programa por su ID.
// Actúa como intermediario entre los formularios y el DAO,
//            aplicando validaciones básicas antes de llamar a la capa de datos.
// ============================================================

using EventPlanner.DAO;
using EventPlanner.Models;
using System;
using System.Collections.Generic;

namespace EventPlanner.Services
{
    public class ProgramaService
    {
        // Instancia del DAO para acceder a los datos de programa
        private ProgramaDAO programaDAO = new ProgramaDAO();

        // ==========================
        // LISTAR PROGRAMAS
        // ==========================
        // Retorna una lista con todos los programas registrados.
        // No aplica validaciones adicionales, solo delega en el DAO.
        public List<Programa> ObtenerProgramas()
        {
            return programaDAO.ObtenerProgramas();
        }

        // ==========================
        // OBTENER PROGRAMA POR ID
        // ==========================
        // Busca y retorna un programa específico según su identificador.
        // Parámetro: idPrograma - ID del programa a buscar (debe ser mayor a 0)
        // Retorna: Objeto Programa si se encuentra, o null si no existe.
        // Lanza excepción si el ID no es válido (menor o igual a 0).
        public Programa ObtenerPorId(int idPrograma)
        {
            // Validación de negocio: el ID debe ser positivo
            if (idPrograma <= 0)
                throw new Exception("El ID del programa no es válido.");

            // Delega la búsqueda en el DAO
            return programaDAO.ObtenerPorId(idPrograma);
        }
    }
}