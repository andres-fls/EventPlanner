// ============================================================
// Archivo: Grupo.cs
// Propósito: Define la estructura de la entidad Grupo.
// Representa un grupo de aprendices (por ejemplo, "Grupo A", "Grupo B")
// que pueden inscribirse de forma colectiva a un evento.
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    // Clase que representa un grupo de aprendices.
    public class Grupo
    {
        // Identificador único del grupo (autogenerado por la BD).
        public int idGrupo { get; set; }

        // Nombre descriptivo del grupo (ej: "Grupo 228106-1", "Grupo Tarde").
        public string nombreGrupo { get; set; }
    }
}