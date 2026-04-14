// ============================================================
// Archivo: EventosForm.cs
// RESPONSABILIDAD:
// SOLO interfaz gráfica.
// NO contiene reglas de negocio.
// ============================================================

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

        private EventoService eventoService = new EventoService();
        private InscripcionService inscripcionService = new InscripcionService();

        public EventosForm(string rol)
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
            rolUsuario = rol;

            // Detecta cambio de selección en el grid
            dgvEventos.SelectionChanged += dgvEventos_SelectionChanged;
        }

        // ==========================
        // LOAD
        // ==========================
        private void EventosForm_Load(object sender, EventArgs e)
        {
            ConfigurarVistaPorRol();
            CargarEventos();
        }

        // ==========================
        // CONFIGURACIÓN SEGÚN ROL
        // ==========================
        private void ConfigurarVistaPorRol()
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
                btnCancelarInscripcion.Visible = false;
            }
        }

        // ==========================
        // EVENTO SELECCIÓN GRID
        // ==========================
        private void dgvEventos_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoInscripcion();
        }

        // ==========================
        // OBTENER EVENTO SELECCIONADO
        // ==========================
        private Evento ObtenerEventoSeleccionado()
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un evento.");
                return null;
            }

            return (Evento)dgvEventos.SelectedRows[0].DataBoundItem;
        }

        // ==========================
        // ACTUALIZAR ESTADO BOTONES
        // ==========================
        private void ActualizarEstadoInscripcion()
        {
            if (rolUsuario != "Aprendiz")
                return;

            if (dgvEventos.SelectedRows.Count == 0)
                return;

            if (Session.IdAprendiz <= 0)
                return;

            Evento evento =
                (Evento)dgvEventos.SelectedRows[0].DataBoundItem;

            try
            {
                bool yaInscrito =
                    inscripcionService.AprendizYaInscrito(
                        Session.IdAprendiz,
                        evento.idEvento);

                btnInscribirse.Enabled = !yaInscrito;
                btnCancelarInscripcion.Enabled = yaInscrito;
            }
            catch
            {
                btnInscribirse.Enabled = false;
                btnCancelarInscripcion.Enabled = false;
            }
        }

        // ==========================
        // CARGAR EVENTOS
        // ==========================
        private void CargarEventos()
        {
            try
            {
                List<Evento> eventos =
                    eventoService.ObtenerEventosDisponibles();

                dgvEventos.DataSource = null;
                dgvEventos.DataSource = eventos;

                dgvEventos.Columns["idEvento"].HeaderText = "ID";
                dgvEventos.Columns["nombreEvento"].HeaderText = "Nombre";
                dgvEventos.Columns["categoriaEvento"].HeaderText = "Categoria";
                dgvEventos.Columns["lugarEvento"].HeaderText = "Lugar";
                dgvEventos.Columns["fechaInicioEvento"].HeaderText = "Fecha";
                dgvEventos.Columns["cupoMaximo"].HeaderText = "Cupo";
                dgvEventos.Columns["activo"].HeaderText = "Activo";

                dgvEventos.Columns["descripcionEvento"].Visible = false;
                dgvEventos.Columns["fechaFinEvento"].Visible = false;
                dgvEventos.Columns["fechaInicioInscripcion"].Visible = false;
                dgvEventos.Columns["fechaFinInscripcion"].Visible = false;
                dgvEventos.Columns["idUsuarioCreador"].Visible = false;

                ActualizarEstadoInscripcion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar eventos: " + ex.Message);
            }
        }

        // ==========================
        // CREAR EVENTO
        // ==========================
        private void btnCrear_Click(object sender, EventArgs e)
        {
            CrearEventoForm form = new CrearEventoForm(rolUsuario);
            form.ShowDialog();
            CargarEventos();
        }

        // ==========================
        // EDITAR EVENTO
        // ==========================
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Evento seleccionado = ObtenerEventoSeleccionado();
            if (seleccionado == null) return;

            CrearEventoForm editar =
                new CrearEventoForm(rolUsuario, seleccionado);

            editar.ShowDialog();
            CargarEventos();
        }

        // ==========================
        // ELIMINAR (SOFT DELETE)
        // ==========================
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Evento seleccionado = ObtenerEventoSeleccionado();
            if (seleccionado == null) return;

            DialogResult confirmacion = MessageBox.Show(
                "¿Deseas desactivar este evento?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                eventoService.DesactivarEvento(seleccionado.idEvento);

                MessageBox.Show("Evento desactivado correctamente.");
                CargarEventos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ==========================
        // INSCRIBIRSE
        // ==========================
        private void btnInscribirse_Click(object sender, EventArgs e)
        {
            Evento seleccionado = ObtenerEventoSeleccionado();
            if (seleccionado == null) return;

            try
            {
                inscripcionService.InscribirAprendiz(
                    Session.IdAprendiz,
                    seleccionado.idEvento);

                MessageBox.Show("Inscripción realizada correctamente.");
                CargarEventos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ==========================
        // CANCELAR INSCRIPCIÓN
        // ==========================
        private void btnCancelarInscripcion_Click(object sender, EventArgs e)
        {
            Evento seleccionado = ObtenerEventoSeleccionado();
            if (seleccionado == null) return;

            try
            {
                inscripcionService.CancelarInscripcionAprendiz(
                    Session.IdAprendiz,
                    seleccionado.idEvento);

                MessageBox.Show("Inscripción cancelada.");
                CargarEventos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ==========================
        // VOLVER
        // ==========================
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}