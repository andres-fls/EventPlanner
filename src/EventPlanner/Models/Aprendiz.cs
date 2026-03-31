// ============================================================
// Archivo: Aprendiz.cs
// Propósito: Define la estructura de la entidad Aprendiz.
// Representa a un estudiante o aprendiz registrado en el sistema.
// Contiene sus datos personales (cédula, nombre, edad, género,
// correo, teléfono), la ficha a la que pertenece y la relación
// con su usuario de acceso (idUsuario).
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    // Clase que representa un aprendiz (estudiante).
    public class Aprendiz
    {
        // Identificador único del aprendiz (autogenerado por la BD).
        public int idAprendiz { get; set; }

        // Número de cédula del aprendiz (único).
        public string cedulaAprendiz { get; set; }

        // Nombre completo del aprendiz.
        public string nombreAprendiz { get; set; }

        // Edad del aprendiz (en años).
        public int edadAprendiz { get; set; }

        // Género del aprendiz: "Masculino", "Femenino", "Otro".
        public string generoAprendiz { get; set; }

        // Correo electrónico del aprendiz (formato validado).
        public string correoAprendiz { get; set; }

        // Número de teléfono del aprendiz (10 dígitos).
        public string telefonoAprendiz { get; set; }

        // Código de la ficha de formación a la que pertenece.
        // Clave foránea que referencia a Ficha.codigoFicha.
        public int codigoFicha { get; set; }

        // ID del usuario asociado (credenciales de acceso).
        // Clave foránea que referencia a Usuario.idUsuario.
        public int idUsuario { get; set; }
    }
}