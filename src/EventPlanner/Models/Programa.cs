// ============================================================
// Archivo: Programa.cs
// Propósito: Define la estructura de la entidad Programa.
// Representa un programa de formación académica (como Tecnólogo en Sistemas, etc.).
// Contiene información sobre el código, nombre, duración y nivel del programa.
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    // Clase que representa un programa de formación.
    public class Programa
    {
        // Identificador único del programa (autogenerado por la BD).
        public int idPrograma { get; set; }

        // Código numérico del programa (ej: 228106).
        public int codigoPrograma { get; set; }

        // Nombre completo del programa (ej: "Análisis y Desarrollo de Software").
        public string nombrePrograma { get; set; }

        // Duración del programa en meses, semestres o años (formato texto: "24 meses", "3 años").
        public string duracionPrograma { get; set; }

        // Nivel de formación: "Tecnólogo", "Técnico", "Especialización", etc.
        public string nivelPrograma { get; set; }
    }
}