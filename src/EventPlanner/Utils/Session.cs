// ============================================================
// Archivo: Session.cs
// Propósito: Almacena información de la sesión actual del usuario.
// Mantiene datos globales mientras la aplicación está en ejecución:
// el ID del usuario logueado, su rol, y el ID del aprendiz asociado
// (si aplica). Estos datos se utilizan en toda la aplicación para
// identificar al usuario activo y sus permisos.
// ============================================================

namespace EventPlanner.Utils
{
    // Clase estática que guarda el estado de la sesión actual.
    public static class Session
    {
        // ID del usuario que ha iniciado sesión (desde la tabla Usuario).
        public static int IdUsuario { get; set; }

        // Rol del usuario: "Aprendiz", "Admin", "Instructor", etc.
        public static string Rol { get; set; }

        // ID del aprendiz asociado al usuario (solo si el rol es "Aprendiz").
        // Se utiliza para realizar operaciones específicas del aprendiz,
        // como inscribirse a eventos o cancelar inscripciones.
        public static int IdAprendiz { get; set; }
    }
}