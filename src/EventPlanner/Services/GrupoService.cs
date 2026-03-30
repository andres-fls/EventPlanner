using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class GrupoService
    {
        private GrupoDAO grupoDAO = new GrupoDAO();

        // ==========================
        // LISTAR GRUPOS
        // ==========================
        public List<Grupo> ObtenerGrupos()
        {
            return grupoDAO.ObtenerGrupos();
        }
    }
}