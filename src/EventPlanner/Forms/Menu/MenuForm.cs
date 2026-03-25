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
            if (rolUsuario == "Aprendiz")
            {
                btnUsuarios.Visible = false;
                btnReportes.Visible = false;
            }
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            EventosForm eventos = new EventosForm(rolUsuario);
            this.Hide();
            eventos.ShowDialog();
            this.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            UsuariosForm form = new UsuariosForm(rolUsuario);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
           LoginForm login = new LoginForm();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesForm form = new ReportesForm(rolUsuario);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }
    }
}
