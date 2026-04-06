// ============================================================
// Archivo: Program.cs
// Propósito: Punto de entrada principal de la aplicación.
// Configura los estilos visuales de Windows Forms e inicia
// la aplicación mostrando el formulario de inicio de sesión
// (LoginForm). Es el primer código que se ejecuta al lanzar
// el programa.
// ============================================================

using System; // Importa base de .NET
using System.Windows.Forms; // Importa Windows Forms

namespace EventPlanner
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread] // Indica que el modelo de subprocesos para la aplicación es de un solo subproceso (STA)
        static void Main()
        {
            // Habilita los estilos visuales (como los temas de Windows) para los controles
            Application.EnableVisualStyles();
            // Establece que los controles usen el motor de renderizado de texto compatible con versiones anteriores
            Application.SetCompatibleTextRenderingDefault(false);
            // Ejecuta la aplicación y muestra el formulario principal (LoginForm)
            Application.Run(new LoginForm());
        }
    }
}