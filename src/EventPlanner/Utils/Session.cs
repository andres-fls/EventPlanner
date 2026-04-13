// ============================================================
// Archivo: Session.cs
// Propósito: Almacena información de la sesión del usuario.
// Mantiene datos globales: IdUsuario, Rol, IdAprendiz
// ============================================================

namespace EventPlanner.Utils
{
    /// <summary>
    /// Clase estática que gestiona la sesión del usuario logueado.
    /// Almacena información del usuario activo durante la ejecución.
    /// Debe ser limpiada al cerrar sesión por motivos de seguridad.
    /// </summary>
    public static class Session
    {
        // ==========================
        // PROPIEDADES DE SESIÓN
        // ==========================

        /// <summary>
        /// ID del usuario logueado (desde tabla Usuario)
        /// </summary>
        public static int IdUsuario { get; set; }

        /// <summary>
        /// Rol del usuario: "Aprendiz", "Admin", "Instructor", etc.
        /// </summary>
        public static string Rol { get; set; }

        /// <summary>
        /// ID del aprendiz asociado al usuario (solo si rol es "Aprendiz")
        /// Se utiliza para inscripciones, cancelaciones, etc.
        /// </summary>
        public static int IdAprendiz { get; set; }

        // ==========================
        // MÉTODO: LIMPIAR SESIÓN
        // ==========================
        /// <summary>
        /// Limpia toda la información de la sesión.
        /// DEBE ser llamado al cerrar sesión por SEGURIDAD.
        /// Evita que el siguiente usuario vea datos del anterior.
        /// </summary>
        public static void LimpiarSesion()
        {
            IdUsuario = 0;
            Rol = null;
            IdAprendiz = 0;
        }

        // ==========================
        // MÉTODO: VERIFICAR SESIÓN ACTIVA
        // ==========================
        /// <summary>
        /// Verifica si hay una sesión activa
        /// </summary>
        /// <returns>true si hay usuario logueado, false en caso contrario</returns>
        public static bool HaySesionActiva()
        {
            return IdUsuario > 0 && !string.IsNullOrEmpty(Rol);
        }

        // ==========================
        // MÉTODO: VERIFICAR SI ES APRENDIZ
        // ==========================
        /// <summary>
        /// Verifica si el usuario actual es un aprendiz
        /// </summary>
        /// <returns>true si el rol es "Aprendiz"</returns>
        public static bool EsAprendiz()
        {
            return Rol == "Aprendiz";
        }

        // ==========================
        // MÉTODO: VERIFICAR SI ES ADMIN
        // ==========================
        /// <summary>
        /// Verifica si el usuario actual es administrador
        /// </summary>
        /// <returns>true si el rol es "Admin"</returns>
        public static bool EsAdmin()
        {
            return Rol == "Admin";
        }
    }
}