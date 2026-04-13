// ============================================================
// Archivo: Evento.cs
// Propósito: Define la estructura de la entidad Evento.
// Representa un evento organizado en el sistema (conferencia,
// taller, hackathon, feria, etc.). Contiene información sobre
// nombre, tipo, lugar, descripción, fechas del evento y del
// período de inscripción, cupo máximo, estado activo y el
// usuario que lo creó.
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    // Clase que representa un evento en el planificador.
    public class Evento
    {
        // Identificador único del evento (autogenerado por la BD).
        public int idEvento { get; set; }

        // Nombre o título del evento (ej: "Hackathon 2025").
        public string nombreEvento { get; set; }

        public string categoriaEvento { get; set; }

        // Lugar físico o virtual donde se realiza el evento.
        public string lugarEvento { get; set; }

        // Descripción detallada del evento (opcional, texto largo).
        public string descripcionEvento { get; set; }

        // Fecha y hora de inicio del evento.
        public DateTime fechaInicioEvento { get; set; }

        // Fecha y hora de finalización del evento.
        public DateTime fechaFinEvento { get; set; }

        // Fecha y hora de inicio del período de inscripciones.
        public DateTime fechaInicioInscripcion { get; set; }

        // Fecha y hora de cierre del período de inscripciones.
        public DateTime fechaFinInscripcion { get; set; }

        // Número máximo de participantes permitidos.
        public int cupoMaximo { get; set; }

        // Estado del evento: true = activo (visible/inscribible), false = desactivado.
        public bool activo { get; set; }

        // ID del usuario que creó el evento (clave foránea hacia Usuario).
        public int idUsuarioCreador { get; set; }
    }
}