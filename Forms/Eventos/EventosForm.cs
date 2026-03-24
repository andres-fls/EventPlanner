using System;
using System.Windows.Forms;
using EventPlanner.Utils;


namespace EventPlanner
{
    public partial class EventosForm : Form
    {
        private string rolUsuario;
        public EventosForm(string rol)
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
            rolUsuario = rol;
        }

        private void EventosForm_Load(object sender, EventArgs e)
        {
            if (rolUsuario == "Aprendiz")
            {
                btnCrear.Visible = false;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
            }
            if (rolUsuario == "Admin")
            {
                btnInscribirse.Visible = false;
            }
            
        }

        private void panelBase_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            CrearEventoForm crearEventoForm = new CrearEventoForm(rolUsuario);
            crearEventoForm.ShowDialog();
            CargarEventos();
        }
        private void CargarEventos()
        {
            // Aquí puedes cargar los eventos desde tu fuente de datos y mostrarlos en el panel
            // Por ejemplo, podrías usar un List<Evento> para almacenar los eventos y luego mostrarlos en un ListBox o DataGridView
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            MenuForm menu = new MenuForm(rolUsuario);
            menu.Show();

            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
                {
                MessageBox.Show("Por favor, selecciona un evento para editar.");
                return;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un evento para eliminar.");
                return;
            }
        }

        private void btnInscribirse_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un evento para inscribirte.");
                return;
            }
        }
    }
}
