// ============================================================
// Archivo: LoginForm.cs
// Propósito: Formulario de inicio de sesión de la aplicación.
// Permite a los usuarios autenticarse con usuario y contraseña.
// Valida credenciales, guarda información en la sesión global
// (rol, idUsuario, idAprendiz si corresponde) y abre el menú
// principal según el rol. También permite navegar al registro
// de nuevos usuarios y salir de la aplicación.
// ============================================================

using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.Utils;
using System;
using System.Windows.Forms;

namespace EventPlanner
{
    public partial class LoginForm : Form
    {
        // Constructor: inicializa componentes y aplica tamaño desde configuración
        public LoginForm()
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
        }

        // Al cargar el formulario, configura el campo de contraseña para ocultar caracteres
        // y coloca el foco en el campo de usuario.
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Muestra asteriscos en lugar de texto plano
            txtPassword.UseSystemPasswordChar = true;

            // Posiciona el cursor en el campo de usuario para facilitar el ingreso
            txtUsuario.Focus();
        }

        // Botón "Iniciar Sesión": valida credenciales y autentica al usuario
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Limpia espacios en blanco al inicio y final
            txtUsuario.Text = txtUsuario.Text.Trim();
            txtPassword.Text = txtPassword.Text.Trim();

            // Validación de campos vacíos
            if (Validator.CampoVacio(txtUsuario, "Usuario")) return;
            if (Validator.CampoVacio(txtPassword, "Contraseña")) return;

            try
            {
                UsuarioService service = new UsuarioService();
                // Llama al servicio para validar usuario y contraseña; retorna el rol si es exitoso
                string rol = service.IniciarSesion(txtUsuario.Text, txtPassword.Text);

                // Guarda el rol en la sesión global para usarlo en otros formularios
                Session.Rol = rol;

                // Si el usuario es aprendiz, también guarda su idAprendiz en la sesión
                if (rol == "Aprendiz")
                {
                    AprendizService aprendizService = new AprendizService();
                    Aprendiz aprendiz = aprendizService.BuscarPorIdUsuario(Session.IdUsuario);
                    if (aprendiz != null)
                        Session.IdAprendiz = aprendiz.idAprendiz;
                }

                // Mensaje de bienvenida con el rol del usuario
                MessageBox.Show("Bienvenido, iniciaste sesión como: " + rol);

                // Abre el menú principal y oculta el formulario de login
                MenuForm menu = new MenuForm(rol);
                menu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                // Muestra el error (por ejemplo, "Usuario o contraseña incorrectos")
                MessageBox.Show(ex.Message);
            }
        }

        // Enlace "Registrarse": abre el formulario de registro de nuevos aprendices
        private void linkRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistroForm registro = new RegistroForm();
            this.Hide();          // Oculta el login mientras se registra
            registro.ShowDialog(); // Muestra el registro de forma modal
            this.Show();          // Al cerrar registro, vuelve a mostrar el login
        }

        // Botón "Salir": cierra completamente la aplicación
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}