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
            ConfigurarGrid();
            ConfigurarFiltros();
            CargarDatos();
        }

        private void ConfigurarGrid()
        {
            dgvInscripciones.Columns.Clear ();

            dgvInscripciones.Columns.Add("Nombre", "Nombre");
            dgvInscripciones.Columns.Add("Evento", "Evento");
            dgvInscripciones.Columns.Add("Modalidad", "Modalidad");
            dgvInscripciones.Columns.Add("Estado", "Estado");

            dgvInscripciones.ReadOnly = true;

            dgvInscripciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInscripciones.MultiSelect = false;
        }

        private void ConfigurarFiltros()
        {
            cmbEvento.Items.Clear();

            cmbEvento.Items.Add("Todos");
            cmbEvento.Items.Add("Hackathon");
            cmbEvento.Items.Add("Feria Tech");

            cmbEvento.SelectedIndex = 0;
        }

        private void CargarDatos()
        {
            dgvInscripciones.Rows.Clear();

            dgvInscripciones.Rows.Add("Juan Pérez", "Hackathon", "Presencial", "Activo");
            dgvInscripciones.Rows.Add("María Gómez", "Feria Tech", "Virtual", "Cancelado");
            dgvInscripciones.Rows.Add("Carlos López", "Hackathon", "Presencial", "Activo");
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarDatos();

            MessageBox.Show("Datos actualizados.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        private void btnDetalle_Click(object sender, EventArgs e)
        {
           
            if (dgvInscripciones.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro.");
                return;
            }

            string nombre = dgvInscripciones.CurrentRow.Cells[0].Value.ToString();
            string evento = dgvInscripciones.CurrentRow.Cells[1].Value.ToString();
            string modalidad = dgvInscripciones.CurrentRow.Cells[2].Value.ToString();
            string estado = dgvInscripciones.CurrentRow.Cells[3].Value.ToString();

            MessageBox.Show($"Nombre: {nombre}\nEvento: {evento}\nModalidad: {modalidad}\nEstado: {estado}", "Detalle de Inscripción");
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
     
            if (dgvInscripciones.CurrentRow == null)
                return;

            DialogResult r = MessageBox.Show(
                "¿Desea ACTIVAR esta inscripción?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                dgvInscripciones.CurrentRow.Cells[3].Value = "Activo";

                // FUTURO:
                // usuarioDAO.ActualizarEstado(id, "Activo");

                MessageBox.Show("Estado actualizado.");
            }
        }


        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (dgvInscripciones.CurrentRow == null)
                return;

            DialogResult r = MessageBox.Show(
                "¿Desea DESACTIVAR esta inscripción?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                dgvInscripciones.CurrentRow.Cells[3].Value = "Cancelado";

                // FUTURO:
                // usuarioDAO.ActualizarEstado(id, "Cancelado");

                MessageBox.Show("Estado actualizado.");
            }
        }
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string evento = cmbEvento.SelectedItem.ToString();

            foreach (DataGridViewRow fila in dgvInscripciones.Rows)
            {
                if (fila.IsNewRow) continue;
                if (evento == "Todos") fila.Visible = true;
                else fila.Visible = fila.Cells[1].Value.ToString() == evento;
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
