using System;
using System.Windows.Forms;
using EventPlanner.Utils;


namespace EventPlanner
{
    public partial class UsuariosForm : Form
    {
        private string rolUsuario;
        public UsuariosForm(string rol)
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
            rolUsuario = rol;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UsuariosForm_Load(object sender, EventArgs e)
        {
            if (rolUsuario == "Aprendiz")
            {
                btnAgregar.Visible = false;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
            }
            if (dgvUsuarios.Columns.Count == 0)
            {
                dgvUsuarios.Columns.Add("Nombre", "Nombre");
                dgvUsuarios.Columns.Add("Rol", "Rol");
            }
                dgvUsuarios.Rows.Add("Juan Pérez", "Administrador");
                dgvUsuarios.Rows.Add("María Gómez", "Aprendiz");
                dgvUsuarios.Rows.Add("Carlos Rodríguez", "Administrador");
                dgvUsuarios.Rows.Add("Ana Martínez", "Aprendiz");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
