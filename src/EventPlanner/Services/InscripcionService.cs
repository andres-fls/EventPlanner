// ============================================================
// Archivo: InscripcionService.cs
// Propósito: Lógica de negocio para inscripciones
// ============================================================

using System;
using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class InscripcionService
    {
        private InscripcionDAO inscripcionDAO = new InscripcionDAO();

        // =====================================================
        // LISTADO BÁSICO (USA IDs - lógica interna)
        // =====================================================
        public List<Inscripcion> ObtenerInscripciones()
        {
            return inscripcionDAO.ObtenerInscripciones();
        }

        // =====================================================
        // LISTADO CON DETALLE (USA NOMBRES - UI)
        // =====================================================
        public List<InscripcionDetalle> ObtenerInscripcionesConDetalle()
        {
            return inscripcionDAO.ObtenerInscripcionesConDetalle();
        }

        // =====================================================
        // CREAR INSCRIPCIÓN
        // =====================================================
        public void CrearInscripcion(Inscripcion inscripcion)
        {
            ValidarInscripcion(inscripcion);
            inscripcionDAO.CrearInscripcion(inscripcion);
        }

        // =====================================================
        // CANCELAR INSCRIPCIÓN
        // =====================================================
        public void CancelarInscripcion(int idInscripcion)
        {
            if (idInscripcion <= 0)
                throw new Exception("ID de inscripción inválido.");

            inscripcionDAO.CancelarInscripcion(idInscripcion);
        }

        // =====================================================
        // ACTUALIZAR ESTADO
        // =====================================================
        public void ActualizarEstado(int idInscripcion, string nuevoEstado)
        {
            if (idInscripcion <= 0)
                throw new Exception("ID inválido.");

            if (string.IsNullOrWhiteSpace(nuevoEstado))
                throw new Exception("Estado inválido.");

            inscripcionDAO.ActualizarEstado(idInscripcion, nuevoEstado);
        }

        // =====================================================
        // VALIDACIONES DE NEGOCIO
        // =====================================================
        private void ValidarInscripcion(Inscripcion inscripcion)
        {
            if (inscripcion == null)
                throw new Exception("La inscripción no puede ser nula.");

            if (inscripcion.idAprendiz <= 0)
                throw new Exception("Debe especificarse un aprendiz válido.");

            if (inscripcion.idEvento <= 0)
                throw new Exception("Debe especificarse un evento válido.");

            if (string.IsNullOrWhiteSpace(inscripcion.tipoInscripcion))
                throw new Exception("El tipo de inscripción es obligatorio.");

            if (string.IsNullOrWhiteSpace(inscripcion.modalidad))
                throw new Exception("La modalidad es obligatoria.");

            EventoService eventoService = new EventoService();
            List<Evento> eventos = eventoService.ObtenerEventos();

            Evento evento = eventos.Find(ev => ev.idEvento == inscripcion.idEvento);

            if (evento == null)
                throw new Exception("El evento no existe.");

            DateTime ahora = DateTime.Now;

            // RF06 — período inscripción
            if (ahora < evento.fechaInicioInscripcion)
                throw new Exception("El período de inscripción aún no inicia.");

            if (ahora > evento.fechaFinInscripcion)
                throw new Exception("El período de inscripción ya finalizó.");

            // RF19 — cruce horario
            if (inscripcionDAO.TieneCruceHorario(
                    inscripcion.idAprendiz,
                    evento.fechaInicioEvento))
            {
                throw new Exception("Ya existe una inscripción en ese horario.");
            }
        }
    }
}