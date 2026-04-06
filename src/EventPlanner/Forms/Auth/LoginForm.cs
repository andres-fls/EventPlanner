// Archivo: LoginForm.csPropósito: Formulario de inicio de sesión de la aplicación.
// 1--Permite a los usuarios autenticarse con usuario y contraseña.2-- Valida credenciales, guarda información en la sesión global
//y abre el menú principal según el rol. También permite navegar al registro de nuevos usuarios y salir de la aplicación.//

using EventPlanner.Models; // Importa modelos de datos
using EventPlanner.Services; // Importa servicios de negocio
using EventPlanner.Utils; // Importa utilidades
using System; // Importa base de .NET
using System.Drawing; // Importa manejo de gráficos
using System.Windows.Forms; // Importa Windows Forms

namespace EventPlanner // Espacio de nombres de la aplicación
{
    public partial class LoginForm : Form // Clase del formulario de login
    {
        public LoginForm() // Constructor público
        {
            InitializeComponent(); // Inicializa componentes
            this.Size = AppConfig.TamanoVentana; // Aplica tamaño desde config
        }

        private void LoginForm_Load(object sender, EventArgs e) // Evento Load del formulario
        {
            txtPassword.UseSystemPasswordChar = true; // Oculta contraseña con asteriscos

            // poner foco en el textbox de usuario
            txtUsuario.Focus(); // Enfoca el campo usuario
        }

        private void btnLogin_Click(object sender, EventArgs e) // Evento Click del botón login
        {
            // Limpia espacios en blanco al inicio y final
            txtUsuario.Text = txtUsuario.Text.Trim(); // Limpia usuario
            txtPassword.Text = txtPassword.Text.Trim(); // Limpia contraseña

            // Validación de campos vacíos
            if (Validator.CampoVacio(txtUsuario, "Usuario")) return; // Valida usuario no vacío
            if (Validator.CampoVacio(txtPassword, "Contraseña")) return; // Valida contraseña no vacía

            try // Bloque try
            {
                UsuarioService service = new UsuarioService(); // Instancia servicio usuario
                // Llama al servicio para validar usuario y contraseña; retorna el rol si es exitoso
                string rol = service.IniciarSesion(txtUsuario.Text, txtPassword.Text); // Inicia sesión

                // Guarda el rol en la sesión global para usarlo en otros formularios
                Session.Rol = rol; // Asigna rol a sesión

                // Si el usuario es aprendiz, también guarda su idAprendiz en la sesión
                if (rol == "Aprendiz") // Verifica si es aprendiz
                {
                    AprendizService aprendizService = new AprendizService(); // Instancia servicio aprendiz
                    Aprendiz aprendiz = aprendizService.BuscarPorIdUsuario(Session.IdUsuario); // Busca aprendiz
                    if (aprendiz != null) // Si encuentra
                        Session.IdAprendiz = aprendiz.idAprendiz; // Asigna ID aprendiz
                }

                // Mensaje de bienvenida con el rol del usuario
                MessageBox.Show("Bienvenido, iniciaste sesión como: " + rol); // Muestra bienvenida

                // Abre el menú principal y oculta el formulario de login
                MenuForm menu = new MenuForm(rol); // Instancia menú
                menu.Show(); // Muestra menú
                this.Hide(); // Oculta login
            }
            catch (Exception ex) // Captura excepciones
            {
                // Muestra el error (por ejemplo, "Usuario o contraseña incorrectos")
                MessageBox.Show(ex.Message); // Muestra error
            }
        }

        private void linkRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Evento LinkClicked del enlace registro
        {
            RegistroForm registro = new RegistroForm(); // Instancia registro
            this.Hide(); registro.ShowDialog(); this.Show(); // Oculta, muestra modal, vuelve
        }

        private void btnSalir_Click(object sender, EventArgs e) // Evento Click del botón salir
        {
            Application.Exit(); // Cierra aplicación
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e) // Evento TextChanged del textbox usuario
        {
            // No mover el foco ni poner cursor de espera aquí. Mantener cursor de texto.
            Cursor = Cursors.IBeam; // Establece cursor IBeam
        }
    }
}