// ============================================================
// Archivo: Inscripcion.cs
// Propósito: Define la estructura de la entidad Inscripcion.
// Representa la inscripción de un aprendiz a un evento.
// Contiene información sobre la fecha, tipo, modalidad, estado,
// y las relaciones con Evento, Aprendiz y opcionalmente Grupo.
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    // Clase que representa una inscripción en el sistema.
    public class Inscripcion
    {
        // Identificador único de la inscripción (autogenerado por la BD).
        public int idInscripcion { get; set; }

        // Fecha y hora en que se realizó la inscripción.
        // Por defecto, la BD asigna la fecha actual.
        public DateTime fechaInscripcion { get; set; }

        // Tipo de inscripción: "Individual" o "Grupal".
        public string tipoInscripcion { get; set; }

        // Modalidad del evento para esta inscripción: "Presencial", "Virtual", etc.
        public string modalidad { get; set; }

        // Estado actual de la inscripción: "Activo", "Cancelado", etc.
        public string estadoInscripcion { get; set; }

        // ID del evento al que se inscribe (clave foránea hacia Evento).
        public int idEvento { get; set; }

        // ID del aprendiz que se inscribe (clave foránea hacia Aprendiz).
        public int idAprendiz { get; set; }

        // ID del grupo (opcional). Solo aplica si tipoInscripcion es "Grupal".
        // Nullable (int?) porque puede ser NULL en la BD cuando es individual.
        public int? idGrupo { get; set; }
    }
}