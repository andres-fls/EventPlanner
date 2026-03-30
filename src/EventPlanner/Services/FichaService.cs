using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class FichaService
    {
        private FichaDAO fichaDAO = new FichaDAO();

        // ==========================
        // LISTAR FICHAS
        // ==========================
        public List<Ficha> ObtenerFichas()
        {
            return fichaDAO.ObtenerFichas();
        }
    }
}