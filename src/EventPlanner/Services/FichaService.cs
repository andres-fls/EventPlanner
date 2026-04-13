// ============================================================
// Archivo: FichaService.cs
// RESPONSABILIDAD:
// Lógica de negocio para Fichas
// ============================================================

using System;
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
            List<Ficha> lista = fichaDAO.ObtenerFichas();

            if (lista == null)
                throw new Exception("Error al obtener las fichas.");

            return lista;
        }

        // ==========================
        // OBTENER POR CÓDIGO
        // ==========================
        public Ficha ObtenerPorCodigo(int codigoFicha)
        {
            if (codigoFicha <= 0)
                throw new Exception("Código de ficha inválido.");

            Ficha ficha = fichaDAO.ObtenerPorCodigo(codigoFicha);

            if (ficha == null)
                throw new Exception("La ficha no existe.");

            return ficha;
        }

        // ==========================
        // VALIDAR EXISTENCIA
        // ==========================
        public bool ExisteFicha(int codigoFicha)
        {
            if (codigoFicha <= 0)
                return false;

            return fichaDAO.ExisteFicha(codigoFicha);
        }
    }
}