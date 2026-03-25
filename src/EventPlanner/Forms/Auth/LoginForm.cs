using System;
using System.Windows.Forms;
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
            cmbRol.Items.Clear();

            cmbRol.Items.Add("Admin");
            cmbRol.Items.Add("Aprendiz");
            cmbRol.SelectedIndex = 0;

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

            string rol = cmbRol.SelectedItem.ToString();

            MenuForm menu = new MenuForm(rol);
            menu.Show();

            this.Hide();
        }
        private void linkRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistroForm registro = new RegistroForm();
            this.Hide();
            registro.ShowDialog();
            this.Show();
        }

    }
}
