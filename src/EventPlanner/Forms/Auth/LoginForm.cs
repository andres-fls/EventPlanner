using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.Utils;
using System;
using System.Windows.Forms;

namespace EventPlanner
{
    public partial class LoginForm : Form
    {
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = txtUsuario.Text.Trim();
            txtPassword.Text = txtPassword.Text.Trim();

            if (Validator.CampoVacio(txtUsuario, "Usuario")) return;
            if (Validator.CampoVacio(txtPassword, "Contraseña")) return;

            try
            {
                UsuarioService service = new UsuarioService();
                string rol = service.IniciarSesion(txtUsuario.Text, txtPassword.Text);

                Session.Rol = rol;

                // Si es aprendiz, guardar su idAprendiz en sesión
                if (rol == "Aprendiz")
                {
                    AprendizService aprendizService = new AprendizService();
                    Aprendiz aprendiz = aprendizService.BuscarPorIdUsuario(Session.IdUsuario);
                    if (aprendiz != null)
                        Session.IdAprendiz = aprendiz.idAprendiz;
                }

                MessageBox.Show("Bienvenido, iniciaste sesión como: " + rol);

                MenuForm menu = new MenuForm(rol);
                menu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void linkRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistroForm registro = new RegistroForm();
            this.Hide();
            registro.ShowDialog();
            this.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
