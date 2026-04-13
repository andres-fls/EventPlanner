using System;
using System.Windows.Forms;
using EventPlanner.Utils;

namespace EventPlanner
{
    public partial class MenuForm : Form
    {
        private string rolUsuario;

        public MenuForm(string rol)
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
            rolUsuario = rol;
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            ConfigurarVistaPorRol();
        }

        // ==========================
        // CONFIGURAR SEGÚN ROL
        // ==========================
        private void ConfigurarVistaPorRol()
        {
            // 🔥 Usa UN SOLO estándar de rol
            if (rolUsuario == "Admin")
            {
                // Admin ve todo
                btnEventos.Visible = true;
                btnUsuarios.Visible = true;
                btnReportes.Visible = true;
            }
            else if (rolUsuario == "Aprendiz")
            {
                // Aprendiz limitado
                btnUsuarios.Visible = false;
                btnReportes.Visible = false;
            }
            else
            {
                // Seguridad básica
                MessageBox.Show("Rol no reconocido.");
                this.Close();
            }
        }

        // ==========================
        // EVENTOS
        // ==========================
        private void btnEventos_Click(object sender, EventArgs e)
        {
            EventosForm eventos = new EventosForm(rolUsuario);
            this.Hide();
            eventos.ShowDialog();
            this.Show();
        }

        // ==========================
        // USUARIOS (solo admin)
        // ==========================
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (rolUsuario != "Admin")
            {
                MessageBox.Show("Acceso no autorizado.");
                return;
            }

            UsuariosForm form = new UsuariosForm(rolUsuario);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        // ==========================
        // REPORTES (solo admin)
        // ==========================
        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (rolUsuario != "Admin")
            {
                MessageBox.Show("Acceso no autorizado.");
                return;
            }

            ReportesForm form = new ReportesForm(rolUsuario);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        // ==========================
        // SALIR
        // ==========================
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Seguro que deseas cerrar sesión?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                Session.LimpiarSesion();
                this.Hide();

                LoginForm login = new LoginForm();
                login.ShowDialog();

                this.Close();
            }
        }
    }
}