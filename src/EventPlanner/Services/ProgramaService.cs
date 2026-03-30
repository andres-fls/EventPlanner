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
        // LISTAR PROGRAMAS
        // ==========================
        public List<Programa> ObtenerProgramas()
        {
            return programaDAO.ObtenerProgramas();
        }

        // ==========================
        // OBTENER POR ID
        // ==========================
        public Programa ObtenerPorId(int idPrograma)
        {
            if (idPrograma <= 0)
                throw new Exception("El ID del programa no es válido.");

            return programaDAO.ObtenerPorId(idPrograma);
        }
    }
}