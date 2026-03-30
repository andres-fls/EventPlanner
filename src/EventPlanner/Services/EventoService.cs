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
        // LISTAR EVENTOS
        // ==========================
        public List<Evento> ObtenerEventos()
        {
            return eventoDAO.ObtenerEventos();
        }

        // ==========================
        // DESACTIVAR EVENTO
        // ==========================
        public bool DesactivarEvento(int idEvento)
        {
            return eventoDAO.DesactivarEvento(idEvento);
        }

        // ==========================
        // VALIDACIONES (REGLAS NEGOCIO)
        // ==========================
        private void ValidarEvento(Evento evento)
        {

            if (evento.fechaFinInscripcion < evento.fechaInicioInscripcion)
                throw new Exception("Rango de inscripción inválido.");

            if (evento.cupoMaximo < 0)
                throw new Exception("El cupo no puede ser negativo.");
        }

        // ==========================
        // ACTUALIZAR EVENTO
        // ==========================
        public bool ActualizarEvento(Evento evento)
        {
            ValidarEvento(evento);
            return eventoDAO.ActualizarEvento(evento);
        }

        public List<Evento> ObtenerEventosPorFecha(DateTime desde, DateTime hasta)
        {
            return eventoDAO.ObtenerEventosPorFecha(desde, hasta);
        }

    }
}