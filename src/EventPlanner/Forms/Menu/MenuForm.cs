// ============================================================
// Archivo: MenuForm.cs
// Propósito: Ventana principal del menú de la aplicación.
// Muestra diferentes opciones según el rol del usuario 
// (Aprendiz o Administrador/Instructor). Permite navegar 
// hacia las secciones de Eventos, Usuarios, Reportes y Salir.
// ============================================================

using System; // Importa base de .NET
using System.Windows.Forms; // Importa Windows Forms
using EventPlanner.Utils; // Importa utilidades

namespace EventPlanner // Espacio de nombres de la aplicación
{
    public partial class MenuForm : Form // Clase del formulario de menú
    {
        // Almacena el rol del usuario logueado ("Aprendiz", "Admin", "Instructor", etc.)
        private string rolUsuario; // Variable para rol del usuario

        // Constructor: recibe el rol del usuario para personalizar el menú.
        // También aplica el tamaño de ventana definido en la configuración global.
        public MenuForm(string rol) // Constructor público
        {
            InitializeComponent(); // Inicializa componentes
            // Asigna el tamaño de la ventana desde AppConfig (centraliza la configuración)
            this.Size = AppConfig.TamanoVentana; // Aplica tamaño desde config
            rolUsuario = rol; // Asigna rol
        }

        // Evento que se ejecuta al cargar el formulario.
        // Oculta botones a los que los usuarios no tienen acceso.
        private void MenuForm_Load(object sender, EventArgs e) // Evento Load
        {
            // Si el usuario es admin, ocultar el botón de eventos (solo para aprendices)
            if (rolUsuario == "Admin") // Verifica rol admin
            {
                btnEventos.Visible = false; // Oculta botón eventos
            }
            // Si el usuario es aprendiz, ocultar las opciones administrativas
            else if (rolUsuario == "Aprendiz") // Verifica rol aprendiz
            {
                btnUsuarios.Visible = false;  // Oculta botón usuarios
                btnReportes.Visible = false;  // Oculta botón reportes
            }
        }

        // Botón: Abre el formulario de gestión de eventos.
        // Oculta el menú actual, muestra el formulario de eventos y al cerrarlo vuelve a mostrar el menú.
        private void btnEventos_Click(object sender, EventArgs e) // Evento Click eventos
        {
            EventosForm eventos = new EventosForm(rolUsuario); // Instancia eventos
            this.Hide();          // Oculta menú
            eventos.ShowDialog(); // Muestra modal
            this.Show();          // Vuelve a mostrar menú
        }

        // Botón: Abre el formulario de gestión de usuarios (solo visible para admin).
        private void btnUsuarios_Click(object sender, EventArgs e) // Evento Click usuarios
        {
            UsuariosForm form = new UsuariosForm(rolUsuario); // Instancia usuarios
            this.Hide(); // Oculta menú
            form.ShowDialog(); // Muestra modal
            this.Show(); // Vuelve a mostrar menú
        }

        // Botón: Cierra la sesión actual y vuelve a la pantalla de login.
        private void btnSalir_Click(object sender, EventArgs e) // Evento Click salir
        {
            LoginForm login = new LoginForm(); // Instancia login
            this.Hide();          // Oculta menú
            login.ShowDialog();   // Muestra modal
            this.Close();         // Cierra menú
        }

        // Botón: Abre el formulario de reportes (solo visible para admin/instructor).
        private void btnReportes_Click(object sender, EventArgs e) // Evento Click reportes
        {
            ReportesForm form = new ReportesForm(rolUsuario); // Instancia reportes
            this.Hide(); // Oculta menú
            form.ShowDialog(); // Muestra modal
            this.Show(); // Vuelve a mostrar menú
        }
    }
}