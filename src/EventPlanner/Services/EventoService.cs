// ============================================================
// Archivo: EventoService.cs
// Propósito: Capa de servicio para la lógica de negocio
//            relacionada con los eventos.
// Contiene métodos para crear, listar, actualizar, desactivar
//            y filtrar eventos por fechas.
// Aplica validaciones de reglas de negocio (rango de inscripción,
//            cupo no negativo, etc.) antes de llamar al DAO.
// ============================================================

using System;
using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class EventoService
    {
        // Instancia del DAO para acceder a los datos de evento
        private EventoDAO eventoDAO = new EventoDAO();

        // ==========================
        // CREAR EVENTO
        // ==========================
        // Valida los datos del evento y lo guarda en la base de datos.
        // Parámetro: evento - Objeto Evento con los datos a registrar.
        // Retorna: true si la inserción fue exitosa, false en caso contrario.
        public bool CrearEvento(Evento evento)
        {
            // Aplica reglas de negocio antes de insertar
            ValidarEvento(evento);
            // Llama al DAO para realizar la inserción
            return eventoDAO.CrearEvento(evento);
        }

        // ==========================
        // LISTAR EVENTOS
        // ==========================
        // Retorna una lista con todos los eventos registrados.
        public List<Evento> ObtenerEventos()
        {
            return eventoDAO.ObtenerEventos();
        }

        // ==========================
        // DESACTIVAR EVENTO (SOFT DELETE)
        // ==========================
        // Marca un evento como inactivo (activo = false) y cancela sus inscripciones asociadas.
        // Parámetro: idEvento - Identificador del evento a desactivar.
        // Retorna: true si la desactivación fue exitosa.
        public bool DesactivarEvento(int idEvento)
        {
            return eventoDAO.DesactivarEvento(idEvento);
        }

        // ==========================
        // VALIDACIONES (REGLAS DE NEGOCIO)
        // ==========================
        // Método privado que verifica que el evento cumpla con las reglas del negocio.
        private void ValidarEvento(Evento evento)
        {
            // Validación: la fecha de cierre de inscripción no puede ser anterior a la fecha de inicio
            if (evento.fechaFinInscripcion < evento.fechaInicioInscripcion)
                throw new Exception("Rango de inscripción inválido.");

            // Validación: el cupo máximo no puede ser negativo
            if (evento.cupoMaximo < 0)
                throw new Exception("El cupo no puede ser negativo.");
        }

        // ==========================
        // ACTUALIZAR EVENTO
        // ==========================
        // Modifica los datos de un evento existente.
        // Parámetro: evento - Objeto Evento con los nuevos valores (debe tener idEvento válido).
        // Retorna: true si la actualización afectó al menos una fila.
        public bool ActualizarEvento(Evento evento)
        {
            // Aplica las mismas validaciones que en la creación
            ValidarEvento(evento);
            return eventoDAO.ActualizarEvento(evento);
        }

        // ==========================
        // OBTENER EVENTOS POR RANGO DE FECHAS
        // ==========================
        // Filtra eventos cuya fecha de inicio esté dentro del rango [desde, hasta].
        // Parámetros:
        //   desde - Fecha límite inferior (incluida)
        //   hasta - Fecha límite superior (incluida)
        // Retorna: Lista de eventos que cumplen la condición.
        public List<Evento> ObtenerEventosPorFecha(DateTime desde, DateTime hasta)
        {
            return eventoDAO.ObtenerEventosPorFecha(desde, hasta);
        }
    }
}