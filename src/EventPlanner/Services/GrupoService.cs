// ============================================================
// Archivo: GrupoService.cs
// RESPONSABILIDAD:
// Contiene la lógica de negocio relacionada con los grupos.
// ============================================================

using System;
using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class GrupoService
    {
        private GrupoDAO grupoDAO = new GrupoDAO();

        // =====================================================
        // OBTENER TODOS LOS GRUPOS
        // =====================================================
        public List<Grupo> ObtenerGrupos()
        {
            List<Grupo> lista = grupoDAO.ObtenerGrupos();

            // Validación de negocio (opcional pero profesional)
            if (lista == null || lista.Count == 0)
                throw new Exception("No hay grupos registrados.");

            return lista;
        }

        // =====================================================
        // OBTENER GRUPO POR ID
        // =====================================================
        public Grupo ObtenerPorId(int idGrupo)
        {
            if (idGrupo <= 0)
                throw new Exception("ID de grupo inválido.");

            List<Grupo> lista = grupoDAO.ObtenerGrupos();

            Grupo grupo = lista.Find(g => g.idGrupo == idGrupo);

            if (grupo == null)
                throw new Exception("El grupo no existe.");

            return grupo;
        }
    }
}