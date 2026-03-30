using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


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
            CargarEventos();
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
            try
            {
                EventoService service = new EventoService();
                List<Evento> eventos = service.ObtenerEventos();

                dgvEventos.DataSource = null;
                dgvEventos.DataSource = eventos;

                // Nombres de columnas más legibles
                dgvEventos.Columns["idEvento"].HeaderText = "ID";
                dgvEventos.Columns["nombreEvento"].HeaderText = "Nombre";
                dgvEventos.Columns["tipoEvento"].HeaderText = "Tipo";
                dgvEventos.Columns["lugarEvento"].HeaderText = "Lugar";
                dgvEventos.Columns["fechaInicioEvento"].HeaderText = "Fecha";
                dgvEventos.Columns["cupoMaximo"].HeaderText = "Cupo";
                dgvEventos.Columns["activo"].HeaderText = "Activo";

                // Ocultar columnas que no necesita ver el usuario
                dgvEventos.Columns["descripcionEvento"].Visible = false;
                dgvEventos.Columns["fechaFinEvento"].Visible = false;
                dgvEventos.Columns["fechaInicioInscripcion"].Visible = false;
                dgvEventos.Columns["fechaFinInscripcion"].Visible = false;
                dgvEventos.Columns["idUsuarioCreador"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar eventos: " + ex.Message);
            }
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
                MessageBox.Show("Selecciona un evento para editar.");
                return;
            }

            Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem;
            CrearEventoForm editar = new CrearEventoForm(rolUsuario, seleccionado);
            editar.ShowDialog();
            CargarEventos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un evento para eliminar.");
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Estás seguro de que deseas desactivar este evento?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem;
                EventoService service = new EventoService();
                service.DesactivarEvento(seleccionado.idEvento);
                MessageBox.Show("Evento desactivado correctamente.");
                CargarEventos();
            }
        }

        private void btnInscribirse_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un evento para inscribirte.");
                return;
            }

            Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem;

            if (!seleccionado.activo)
            {
                MessageBox.Show("Este evento no está activo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                $"¿Deseas inscribirte en el evento '{seleccionado.nombreEvento}'?",
                "Confirmar inscripción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    Inscripcion inscripcion = new Inscripcion()
                    {
                        idAprendiz = Session.IdAprendiz,
                        idEvento = seleccionado.idEvento,
                        tipoInscripcion = "Individual",
                        modalidad = "Presencial",
                        estadoInscripcion = "Activo"
                    };

                    InscripcionService service = new InscripcionService();
                    service.CrearInscripcion(inscripcion);

                    MessageBox.Show("Te has inscrito exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al inscribirse: " + ex.Message);
                }
            }
        }
    }
}
