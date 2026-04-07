// ============================================================
// Archivo: AppConfig.cs
// Propósito: Configuración global de la aplicación.
// Contiene valores estáticos que pueden ser utilizados desde
// cualquier parte del sistema para mantener consistencia
// (por ejemplo, tamaño de ventanas).
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EventPlanner.Utils
{
    // Clase estática que almacena parámetros de configuración de la aplicación.
    public static class AppConfig
    {
        // Tamaño por defecto para las ventanas del formulario.
        // Se aplica en cada formulario al cargarse (this.Size = AppConfig.TamanoVentana).
        // Ancho: 800 píxeles, Alto: 500 píxeles.
        public static Size TamanoVentana = new Size(800, 500);
    }
}