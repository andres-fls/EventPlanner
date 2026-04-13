// ============================================================
// Archivo: GrupoService.cs
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

        public List<Grupo> ObtenerGrupos()
        {
            List<Grupo> lista = grupoDAO.ObtenerGrupos();

            if (lista == null || lista.Count == 0)
                throw new Exception("No hay grupos registrados.");

            return lista;
        }

        public Grupo ObtenerPorId(int idGrupo)
        {
            if (idGrupo <= 0)
                throw new Exception("ID de grupo inválido.");

            // ← antes traía todos y filtraba con lista.Find(...)
            Grupo grupo = grupoDAO.ObtenerPorId(idGrupo);

            if (grupo == null)
                throw new Exception("El grupo no existe.");

            return grupo;
        }
    }
}