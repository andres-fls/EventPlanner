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
    public static class tipoEvento
    {
        public const string GRUPAL = "Grupal";
        public const string INDIVIDUAL = "Individual";

    }
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


        public List<Evento> ObtenerEventosDisponibles()
        {
            List<Evento> eventos = eventoDAO.ObtenerEventos();
            List<Evento> eventosValidos = new List<Evento>();
            foreach (var evento in eventos)

            {
                if (evento.fechaInicioEvento >= DateTime.Now.Date)
                {
                    eventosValidos.Add(evento);
                }
            }
            return eventosValidos;

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
            if (evento == null)
                throw new Exception("El evento no puede ser nulo.");

            // --------------------------
            // Validar nombre
            // --------------------------
            if (string.IsNullOrWhiteSpace(evento.nombreEvento))
                throw new Exception("El nombre del evento es obligatorio.");

            // --------------------------
            // Validar fecha del evento
            // (No permitir fechas pasadas)
            // --------------------------
            if (evento.fechaInicioEvento.Date < DateTime.Now.Date)
                throw new Exception("No se puede crear un evento en fechas anteriores.");

            // --------------------------
            // Validar horario permitido
            // (7 AM - 7 PM)
            // --------------------------
            TimeSpan hora = evento.fechaInicioEvento.TimeOfDay;

            if (hora < new TimeSpan(7, 0, 0) ||
                hora > new TimeSpan(19, 0, 0))
            {
                throw new Exception("Los eventos solo pueden realizarse entre 7am y 7pm.");
            }

            // --------------------------
            // Validar rango inscripción
            // --------------------------
            if (evento.fechaFinInscripcion < evento.fechaInicioInscripcion)
                throw new Exception("Rango de inscripción inválido.");

            // --------------------------
            // Validar cupo
            // --------------------------
            if (evento.cupoMaximo < 0)
                throw new Exception("El cupo no puede ser negativo.");

            // --------------------------
            // ✅ VALIDAR TIPO EVENTO (NUEVO)
            // --------------------------
            if (string.IsNullOrWhiteSpace(evento.tipoEvento))
                throw new Exception("Debe seleccionar el tipo de evento.");

            if (evento.tipoEvento != tipoEvento.GRUPAL &&
                evento.tipoEvento != tipoEvento.INDIVIDUAL)
            {
                throw new Exception("Tipo de evento inválido.");
            }
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