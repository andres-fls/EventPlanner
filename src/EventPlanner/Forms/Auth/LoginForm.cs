using EventPlanner.Services;
using EventPlanner.Utils;
using System;
using System.Windows.Forms;

namespace EventPlanner
{
    public partial class LoginForm : Form
    {
        private UsuarioService usuarioService = new UsuarioService();

        public LoginForm()
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            txtUsuario.Focus();
        }

        // ==========================
        // LOGIN
        // ==========================
        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = txtUsuario.Text.Trim();
            txtPassword.Text = txtPassword.Text.Trim();

            // VALIDACIONES UI (correcto que estén aquí)
            if (Validator.CampoVacio(txtUsuario, "Usuario"))
                return;

            if (Validator.CampoVacio(txtPassword, "Contraseña"))
                return;

            try
            {
                string rol = usuarioService.IniciarSesion(
                    txtUsuario.Text,
                    txtPassword.Text
                );

                // ⚠️ OPCIONAL (debug útil)
                // Puedes quitarlo después
                // MessageBox.Show($"UsuarioID: {Session.IdUsuario} - AprendizID: {Session.IdAprendiz}");

                MessageBox.Show(
                    $"Bienvenido ({rol})",
                    "Login exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                MenuForm menu = new MenuForm(rol);
                menu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error de autenticación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                txtPassword.Clear();
                txtUsuario.Focus();
            }
        }

        // ==========================
        // REGISTRO
        // ==========================
        private void linkRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistroForm registro = new RegistroForm();

            this.Hide();
            registro.ShowDialog();
            this.Show();

            txtUsuario.Clear();
            txtPassword.Clear();
            txtUsuario.Focus();
        }

        // ==========================
        // SALIR
        // ==========================
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Deseas salir?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
                Application.Exit();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            // Se deja vacío intencionalmente
        }
    }
}