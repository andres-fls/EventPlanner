// ============================================================
// Archivo: EventoService.cs
// Propósito: Lógica de negocio relacionada con los eventos.
// Aquí viven TODAS las reglas del sistema sobre eventos.
// El Form solo envía datos.
// ============================================================

using System;
using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class EventoService
    {
        private EventoDAO eventoDAO = new EventoDAO();

        // ==========================
        // CREAR EVENTO
        // ==========================
        public bool CrearEvento(Evento evento)
        {
            ValidarEvento(evento);
            return eventoDAO.CrearEvento(evento);
        }

        // ==========================
        // ACTUALIZAR EVENTO
        // ==========================
        public bool ActualizarEvento(Evento evento)
        {
            ValidarEvento(evento);
            return eventoDAO.ActualizarEvento(evento);
        }

        // ==========================
        // LISTAR TODOS LOS EVENTOS
        // ==========================
        public List<Evento> ObtenerEventos()
        {
            return eventoDAO.ObtenerEventos();
        }

        // ==========================
        // EVENTOS DISPONIBLES
        // (Solo eventos actuales o futuros)
        // ==========================
        public List<Evento> ObtenerEventosDisponibles()
        {
            List<Evento> eventos = eventoDAO.ObtenerEventos();
            List<Evento> eventosValidos = new List<Evento>();

            foreach (var evento in eventos)
            {
                if (evento.activo &&
                    evento.fechaInicioEvento.Date >= DateTime.Now.Date)
                {
                    eventosValidos.Add(evento);
                }
            }

            return eventosValidos;
        }

        // ==========================
        // DESACTIVAR EVENTO
        // ==========================
        public bool DesactivarEvento(int idEvento)
        {
            return eventoDAO.DesactivarEvento(idEvento);
        }

        // ==========================
        // OBTENER EVENTO POR ID
        // ==========================
        public Evento ObtenerEventoPorId(int idEvento)
        {
            if (idEvento <= 0)
                throw new Exception("ID de evento inválido.");

            return eventoDAO.ObtenerEventoPorId(idEvento);
        }

        // ==========================
        // FILTRAR POR FECHA
        // ==========================
        public List<Evento> ObtenerEventosPorFecha(DateTime desde, DateTime hasta)
        {
            return eventoDAO.ObtenerEventosPorFecha(desde, hasta);
        }

        // =====================================================
        // VALIDACIONES DE NEGOCIO (EL CORAZÓN DEL SERVICE)
        // =====================================================
        private void ValidarEvento(Evento evento)
        {
            if (evento == null)
                throw new Exception("El evento no puede ser nulo.");

            // --------------------------
            // Nombre obligatorio
            // --------------------------
            if (string.IsNullOrWhiteSpace(evento.nombreEvento))
                throw new Exception("El nombre del evento es obligatorio.");

            // --------------------------
            // Lugar obligatorio
            // --------------------------
            if (string.IsNullOrWhiteSpace(evento.lugarEvento))
                throw new Exception("El lugar del evento es obligatorio.");

            // --------------------------
            // No permitir eventos pasados
            // --------------------------
            if (evento.fechaInicioEvento < DateTime.Now)
                throw new Exception("No se pueden crear eventos en fechas pasadas.");

            // --------------------------
            // Horario permitido (7am - 7pm)
            // --------------------------
            TimeSpan horaEvento = evento.fechaInicioEvento.TimeOfDay;

            if (horaEvento < new TimeSpan(7, 0, 0) ||
                horaEvento >= new TimeSpan(19, 0, 0))
            {
                throw new Exception(
                    "Los eventos solo pueden realizarse entre 7:00 AM y 7:00 PM.");
            }

            // --------------------------
            // Validar rango inscripción
            // --------------------------
            if (evento.fechaFinInscripcion <= evento.fechaInicioInscripcion)
                throw new Exception(
                    "La fecha de cierre debe ser posterior al inicio de inscripción.");

            // --------------------------
            // Inscripciones deben cerrar antes del evento
            // --------------------------
            if (evento.fechaFinInscripcion >= evento.fechaInicioEvento)
                throw new Exception(
                    "Las inscripciones deben cerrarse antes del evento.");

            // --------------------------
            // Cupo válido
            // --------------------------
            if (evento.cupoMaximo <= 0)
                throw new Exception("El cupo debe ser mayor a cero.");
        }
    }
}