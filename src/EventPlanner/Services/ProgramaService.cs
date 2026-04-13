// ============================================================
// Archivo: ProgramaService.cs
// RESPONSABILIDAD:
// Lógica de negocio para Programas
// ============================================================

using EventPlanner.DAO;
using EventPlanner.Models;
using System;
using System.Collections.Generic;

namespace EventPlanner.Services
{
    public class ProgramaService
    {
        private ProgramaDAO programaDAO = new ProgramaDAO();

        // ==========================
        // OBTENER TODOS LOS PROGRAMAS
        // ==========================
        public List<Programa> ObtenerProgramas()
        {
            List<Programa> lista = programaDAO.ObtenerProgramas();

            // Validación mínima (opcional pero profesional)
            if (lista == null)
                throw new Exception("Error al obtener los programas.");

            return lista;
        }

        // ==========================
        // OBTENER POR ID
        // ==========================
        public Programa ObtenerPorId(int idPrograma)
        {
            if (idPrograma <= 0)
                throw new Exception("ID de programa inválido.");

            Programa programa = programaDAO.ObtenerPorId(idPrograma);

            if (programa == null)
                throw new Exception("El programa no existe.");

            return programa;
        }

        // ==========================
        // VALIDAR EXISTENCIA (ÚTIL PARA OTROS SERVICES)
        // ==========================
        public bool ExistePrograma(int idPrograma)
        {
            if (idPrograma <= 0)
                return false;

            Programa programa = programaDAO.ObtenerPorId(idPrograma);

            return programa != null;
        }
    }
}