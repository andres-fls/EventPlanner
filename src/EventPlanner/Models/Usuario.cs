// ============================================================
// Archivo: Usuario.cs
// Propósito: Define la estructura de la entidad Usuario.
// Representa a una persona que puede iniciar sesión en el sistema.
// Contiene propiedades para el identificador, nombre de usuario,
// contraseña y rol (Aprendiz, Admin, Instructor, etc.).
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    // Clase que representa un usuario del sistema.
    public class Usuario
    {
        // Identificador único del usuario (autogenerado por la BD).
        public int idUsuario { get; set; }

        // Nombre de cuenta para iniciar sesión.
        public string nombreUsuario { get; set; }

        // Contraseña del usuario (en producción debería almacenarse encriptada).
        public string passwordUsuario { get; set; }

        // Rol del usuario: define sus permisos en la aplicación.
        // Valores comunes: "Aprendiz", "Admin", "Instructor".
        public string rolUsuario { get; set; }
    }
}