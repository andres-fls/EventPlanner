// ============================================================
// Archivo: Ficha.cs
// Propósito: Define la estructura de la entidad Ficha.
// Representa una ficha de formación (grupo de aprendices asociado
// a un programa académico). Cada ficha tiene un código único y
// pertenece a un programa de formación.
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    // Clase que representa una ficha de aprendices.
    public class Ficha
    {
        // Código numérico de la ficha (ej: 2281061).
        // Es la clave primaria en la tabla Ficha.
        public int codigoFicha { get; set; }

        // ID del programa de formación al que pertenece esta ficha.
        // Clave foránea que referencia a Programa.idPrograma.
        public int idPrograma { get; set; }
    }
}