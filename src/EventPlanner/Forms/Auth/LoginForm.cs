using System;
using System.Windows.Forms;
using EventPlanner.Data;
using EventPlanner.Utils;

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

            if (Validator.CampoVacio(txtUsuario, "Usuario"))
                return;

            if (Validator.CampoVacio(txtPassword, "Contraseña"))
                return;

            UsuarioDAO dao = new UsuarioDAO();

            string rol = dao.ValidarLogin(txtUsuario.Text, txtPassword.Text);

            if (rol != null)
            {
                MessageBox.Show("Bienvenido iniciaste sesion como: " + rol);

                MenuForm menu = new MenuForm(rol);
                menu.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");     
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
