// ============================================================
// Archivo: FichaService.cs
// Propósito: Capa de servicio para la lógica de negocio
//            relacionada con las fichas de formación.
// Contiene un método para obtener la lista de todas las fichas
//            disponibles en el sistema.
// Actúa como intermediario entre los formularios y el DAO,
//            sin validaciones adicionales en este caso (solo delegación).
// ============================================================

using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class FichaService
    {
        // Instancia del DAO para acceder a los datos de ficha
        private FichaDAO fichaDAO = new FichaDAO();

        // ==========================
        // LISTAR FICHAS
        // ==========================
        // Retorna una lista con todas las fichas registradas.
        // No aplica validaciones de negocio adicionales; simplemente delega en el DAO.
        public List<Ficha> ObtenerFichas()
        {
            return fichaDAO.ObtenerFichas();
        }
    }
}