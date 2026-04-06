// ============================================================
// Archivo: InscripcionService.cs
// Propósito: Capa de servicio para la lógica de negocio
//            relacionada con las inscripciones.
// Contiene métodos para crear, listar, cancelar, actualizar estado
//            y obtener inscripciones con detalle.
// Aplica validaciones de reglas de negocio (fechas de inscripción,
//            existencia de aprendiz/evento, etc.) antes de llamar al DAO.
// ============================================================

using System;
using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class InscripcionService
    {
        // Instancia del DAO para acceder a los datos de inscripción
        private InscripcionDAO inscripcionDAO = new InscripcionDAO();

        // ==========================
        // CREAR INSCRIPCIÓN
        // ==========================
        // Valida los datos de la inscripción y la guarda en la base de datos.
        // Parámetro: inscripcion - Objeto Inscripcion con los datos a registrar.
        public void CrearInscripcion(Inscripcion inscripcion)
        {
            // Aplica reglas de negocio antes de insertar
            ValidarInscripcion(inscripcion);
            // Llama al DAO para realizar la inserción
            inscripcionDAO.CrearInscripcion(inscripcion);
        }

        // ==========================
        // LISTAR INSCRIPCIONES
        // ==========================
        // Retorna una lista con todas las inscripciones (sin detalles de nombres).
        public List<Inscripcion> ObtenerInscripciones()
        {
            return inscripcionDAO.ObtenerInscripciones();
        }

        // ==========================
        // CANCELAR INSCRIPCIÓN
        // ==========================
        // Cambia el estado de una inscripción a "Cancelado" (soft delete).
        // Parámetro: idInscripcion - Identificador de la inscripción a cancelar.
        public void CancelarInscripcion(int idInscripcion)
        {
            // Validación básica: el ID debe ser positivo
            if (idInscripcion <= 0)
                throw new Exception("ID de inscripción inválido.");

            inscripcionDAO.CancelarInscripcion(idInscripcion);
        }

        // ==========================
        // VALIDACIONES (REGLAS DE NEGOCIO)
        // ==========================
        // Método privado que verifica que la inscripción cumpla con las reglas del negocio.
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

            // Buscar el evento una sola vez
            EventoService eventoService = new EventoService();
            List<Evento> eventos = eventoService.ObtenerEventos();
            Evento evento = eventos.Find(ev => ev.idEvento == inscripcion.idEvento);

            if (evento != null)
            {
                DateTime ahora = DateTime.Now;

                // RF06 — Validar período de inscripción
                if (ahora < evento.fechaInicioInscripcion)
                    throw new Exception("El período de inscripción aún no ha abierto.");

                if (ahora > evento.fechaFinInscripcion)
                    throw new Exception("El período de inscripción ya cerró.");

                // RF19 — Validar cruce de horarios
                InscripcionDAO dao = new InscripcionDAO();
                if (dao.TieneCruceHorario(inscripcion.idAprendiz, evento.fechaInicioEvento))
                    throw new Exception("Ya tienes un evento inscrito en esa misma fecha y hora.");
            }
        }


        // ==========================
        // ACTUALIZAR ESTADO DE INSCRIPCIÓN
        // ==========================
        // Cambia el estado de una inscripción (ej: "Activo", "Cancelado", "Pendiente").
        // Parámetros:
        //   idInscripcion - ID de la inscripción a modificar.
        //   nuevoEstado   - Nuevo valor para el campo estadoInscripcion.
        public void ActualizarEstado(int idInscripcion, string nuevoEstado)
        {
            inscripcionDAO.ActualizarEstado(idInscripcion, nuevoEstado);
        }

        // ==========================
        // OBTENER INSCRIPCIONES CON DETALLE
        // ==========================
        // Retorna una lista de inscripciones con nombres del aprendiz y evento (JOIN).
        // Útil para mostrar en formularios o reportes con información legible.
        public List<InscripcionDetalle> ObtenerInscripcionesConDetalle()
        {
            return inscripcionDAO.ObtenerInscripcionesConDetalle();
        }
    }
}