// ============================================================
// Archivo: GrupoService.cs
// Propósito: Capa de servicio para la lógica de negocio
//            relacionada con los grupos de aprendices.
// Contiene un método para obtener la lista de todos los grupos
//            disponibles en el sistema.
// Actúa como intermediario entre los formularios y el DAO,
//            sin validaciones adicionales en este caso (solo delegación).
// ============================================================

using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class GrupoService
    {
        // Instancia del DAO para acceder a los datos de grupo
        private GrupoDAO grupoDAO = new GrupoDAO();

        // ==========================
        // LISTAR GRUPOS
        // ==========================
        // Retorna una lista con todos los grupos registrados.
        // No aplica validaciones de negocio adicionales; simplemente delega en el DAO.
        public List<Grupo> ObtenerGrupos()
        {
            return grupoDAO.ObtenerGrupos();
        }
    }
}