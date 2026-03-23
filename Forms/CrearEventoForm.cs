using System;
using System.Windows.Forms;
using EventPlanner.Utils;

namespace EventPlanner
{
    public partial class CrearEventoForm : Form
    {
        private string rolUsuario;

        public CrearEventoForm(string rol)
        {
            InitializeComponent();
            rolUsuario = rol;
        }

        private void CrearEventoForm_Load(object sender, EventArgs e)
        {
            // Restricciones en tiempo real
            txtNombre.KeyPress += Validator.SoloLetrasKeyPress;
            txtLugar.KeyPress += Validator.UsuarioKeyPress;

            // Opcional: dejar sin selección inicial
            cmbTipo.SelectedIndex = -1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Campos obligatorios
            if (Validator.CampoVacio(txtNombre, "Nombre del evento")) return;
            if (Validator.CampoVacio(txtLugar, "Lugar")) return;

            // Validar selección
            if (cmbTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de evento.");
                return;
            }

            // Simulación guardado (aún sin BD)
            MessageBox.Show("Evento guardado exitosamente.");

            this.Close();
        }
    }
}