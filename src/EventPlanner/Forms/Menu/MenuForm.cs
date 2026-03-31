// ============================================================
// Archivo: MenuForm.cs
// Propósito: Ventana principal del menú de la aplicación.
// Muestra diferentes opciones según el rol del usuario 
// (Aprendiz o Administrador/Instructor). Permite navegar 
// hacia las secciones de Eventos, Usuarios, Reportes y Salir.
// ============================================================

using System;
using System.Windows.Forms;
using EventPlanner.Utils;

namespace EventPlanner
{
    public partial class MenuForm : Form
    {
        // Almacena el rol del usuario logueado ("Aprendiz", "Admin", "Instructor", etc.)
        private string rolUsuario;

        // Constructor: recibe el rol del usuario para personalizar el menú.
        // También aplica el tamaño de ventana definido en la configuración global.
        public MenuForm(string rol)
        {
            InitializeComponent();
            // Asigna el tamaño de la ventana desde AppConfig (centraliza la configuración)
            this.Size = AppConfig.TamanoVentana;
            rolUsuario = rol;
        }

        // Evento que se ejecuta al cargar el formulario.
        // Oculta botones a los que los aprendices no tienen acceso.
        private void MenuForm_Load(object sender, EventArgs e)
        {
            // Si el usuario es aprendiz, ocultar las opciones administrativas
            if (rolUsuario == "Aprendiz")
            {
                btnUsuarios.Visible = false;  // Gestión de usuarios (solo admin)
                btnReportes.Visible = false;  // Reportes (solo admin/instructor)
            }
        }

        // Botón: Abre el formulario de gestión de eventos.
        // Oculta el menú actual, muestra el formulario de eventos y al cerrarlo vuelve a mostrar el menú.
        private void btnEventos_Click(object sender, EventArgs e)
        {
            EventosForm eventos = new EventosForm(rolUsuario);
            this.Hide();          // Oculta el menú mientras se usa otro formulario
            eventos.ShowDialog(); // Muestra el formulario de eventos (modal)
            this.Show();          // Al cerrar eventos, vuelve a mostrar el menú
        }

        // Botón: Abre el formulario de gestión de usuarios (solo visible para admin).
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            UsuariosForm form = new UsuariosForm(rolUsuario);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        // Botón: Cierra la sesión actual y vuelve a la pantalla de login.
        private void btnSalir_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            this.Hide();          // Oculta el menú
            login.ShowDialog();   // Muestra la pantalla de inicio de sesión
            this.Close();         // Cierra el menú (ya no es necesario)
        }

        // Botón: Abre el formulario de reportes (solo visible para admin/instructor).
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesForm form = new ReportesForm(rolUsuario);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }
    }
}