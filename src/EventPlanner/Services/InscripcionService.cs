using System;
using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class InscripcionService
    {
        private InscripcionDAO inscripcionDAO = new InscripcionDAO();

        // ==========================
        // CREAR INSCRIPCIÓN
        // ==========================
        public void CrearInscripcion(Inscripcion inscripcion)
        {
            ValidarInscripcion(inscripcion);
            inscripcionDAO.CrearInscripcion(inscripcion);
        }

        // ==========================
        // LISTAR INSCRIPCIONES
        // ==========================
        public List<Inscripcion> ObtenerInscripciones()
        {
            return inscripcionDAO.ObtenerInscripciones();
        }

        // ==========================
        // CANCELAR INSCRIPCIÓN
        // ==========================
        public void CancelarInscripcion(int idInscripcion)
        {
            if (idInscripcion <= 0)
                throw new Exception("ID de inscripción inválido.");

            inscripcionDAO.CancelarInscripcion(idInscripcion);
        }

        // ==========================
        // VALIDACIONES (REGLAS NEGOCIO)
        // ==========================
        private void ValidarInscripcion(Inscripcion inscripcion)
        {
            if (inscripcion.idAprendiz <= 0)
                throw new Exception("Debe especificarse un aprendiz válido.");

            if (inscripcion.idEvento <= 0)
                throw new Exception("Debe especificarse un evento válido.");

            if (string.IsNullOrWhiteSpace(inscripcion.tipoInscripcion))
                throw new Exception("El tipo de inscripción es obligatorio.");

            if (string.IsNullOrWhiteSpace(inscripcion.modalidad))
                throw new Exception("La modalidad es obligatoria.");
        }
        public void ActualizarEstado(int idInscripcion, string nuevoEstado)
        {
            inscripcionDAO.ActualizarEstado(idInscripcion, nuevoEstado);
        }
    }
}