// ============================================================
// Archivo: InscripcionDetalle.cs
// Propósito: Define la estructura de un objeto que combina 
//            información de una inscripción con datos relacionados
//            del aprendiz y el evento.
// Se utiliza para mostrar reportes o listados con nombres legibles
//            en lugar de solo IDs (ej: "Juan Pérez" en vez de idAprendiz).
// ============================================================

namespace EventPlanner.Models
{
    // Clase auxiliar para representar una inscripción con detalles completos.
    // No corresponde directamente a una tabla de la BD, sino a una vista o JOIN.
    public class InscripcionDetalle
    {
        // Identificador único de la inscripción (de la tabla Inscripcion).
        public int idInscripcion { get; set; }

        // ID del aprendiz (clave foránea hacia Aprendiz).
        public int idAprendiz { get; set; }

        // ID del evento (clave foránea hacia Evento).
        public int idEvento { get; set; }

        // Nombre completo del aprendiz (desde la tabla Aprendiz).
        public string nombreAprendiz { get; set; }

        // Nombre del evento (desde la tabla Evento).
        public string nombreEvento { get; set; }

        // Modalidad de la inscripción: "Presencial", "Virtual", etc.
        public string modalidad { get; set; }

        // Estado de la inscripción: "Activo", "Cancelado", etc.
        public string estadoInscripcion { get; set; }
    }
}