// ============================================================
// Archivo: EventosForm.cs
// Propósito: Ventana que muestra la lista de eventos y permite
//            gestionarlos según el rol del usuario.
// - Aprendiz: puede ver eventos, inscribirse y cancelar inscripción.
// - Admin: puede crear, editar, desactivar eventos (no se inscribe).
// - Instructor: tiene permisos similares a Admin (por diseño).
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
        // Almacena el rol del usuario logueado para controlar la UI
        private string rolUsuario;

        // Constructor: recibe el rol y aplica tamaño de ventana desde configuración
        public EventosForm(string rol)
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
            rolUsuario = rol;
        }

        // Evento que se ejecuta al cargar el formulario.
        // Oculta botones según el rol y carga la lista de eventos.
        private void EventosForm_Load(object sender, EventArgs e)
        {
            // Si es aprendiz: no puede crear, editar ni eliminar eventos
            if (rolUsuario == "Aprendiz")
            {
                btnCrear.Visible = false;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
            }
            // Si es administrador: no puede inscribirse ni cancelar inscripciones
            if (rolUsuario == "Admin")
            {
                btnInscribirse.Visible = false;
                btnCancelarInscripcion.Visible = false;
            }
            // Carga los eventos al abrir la ventana
            CargarEventos();
        }

        // Evento vacío (probablemente generado automáticamente por el diseñador)
        private void panelBase_Paint(object sender, PaintEventArgs e)
        {
            // Sin implementación específica
        }

        // Botón "Crear": abre el formulario de creación de eventos y refresca la lista
        private void btnCrear_Click(object sender, EventArgs e)
        {
            CrearEventoForm crearEventoForm = new CrearEventoForm(rolUsuario);
            crearEventoForm.ShowDialog(); // Ventana modal
            CargarEventos(); // Refresca el DataGridView después de crear
        }

        // Carga todos los eventos desde la base de datos y los muestra en el DataGridView
        private void CargarEventos()
        {
            try
            {
                EventoService service = new EventoService();
                List<Evento> eventos = service.ObtenerEventos();

                // Asigna la lista como origen de datos del DataGridView
                dgvEventos.DataSource = null;
                dgvEventos.DataSource = eventos;

                // Personaliza los encabezados de las columnas para que sean más amigables
                dgvEventos.Columns["idEvento"].HeaderText = "ID";
                dgvEventos.Columns["nombreEvento"].HeaderText = "Nombre";
                dgvEventos.Columns["tipoEvento"].HeaderText = "Tipo";
                dgvEventos.Columns["lugarEvento"].HeaderText = "Lugar";
                dgvEventos.Columns["fechaInicioEvento"].HeaderText = "Fecha";
                dgvEventos.Columns["cupoMaximo"].HeaderText = "Cupo";
                dgvEventos.Columns["activo"].HeaderText = "Activo";

                // Oculta columnas que no son necesarias para el usuario final
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

        // Botón "Volver": regresa al menú principal y cierra este formulario
        private void btnVolver_Click(object sender, EventArgs e)
        {
            MenuForm menu = new MenuForm(rolUsuario);
            menu.Show();
            this.Close();
        }

        // Botón "Editar": permite modificar el evento seleccionado (solo admin)
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verifica que haya una fila seleccionada
            if (dgvEventos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un evento para editar.");
                return;
            }

            // Obtiene el evento seleccionado desde el DataBoundItem
            Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem;
            // Abre el formulario de creación en modo edición
            CrearEventoForm editar = new CrearEventoForm(rolUsuario, seleccionado);
            editar.ShowDialog();
            CargarEventos(); // Refresca después de editar
        }

        // Botón "Eliminar" (en realidad desactiva el evento - soft delete)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un evento para eliminar.");
                return;
            }

            // Confirmación antes de desactivar
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
                CargarEventos(); // Refresca la lista
            }
        }

        // Botón "Inscribirse": permite a un aprendiz inscribirse en el evento seleccionado
        private void btnInscribirse_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un evento para inscribirte.");
                return;
            }

            Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem;

            // Valida que el evento esté activo
            if (!seleccionado.activo)
            {
                MessageBox.Show("Este evento no está activo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación del usuario
            DialogResult confirmacion = MessageBox.Show(
                $"¿Deseas inscribirte en el evento '{seleccionado.nombreEvento}'?",
                "Confirmar inscripción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Construye el objeto Inscripcion con datos por defecto
                    Inscripcion inscripcion = new Inscripcion()
                    {
                        idAprendiz = Session.IdAprendiz, // ID del aprendiz desde la sesión
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

        // Botón "Cancelar Inscripción": permite al aprendiz cancelar su inscripción en el evento seleccionado
        private void btnCancelarInscripcion_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un evento para cancelar tu inscripción.");
                return;
            }

            Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem;

            DialogResult confirmacion = MessageBox.Show(
                $"¿Deseas cancelar tu inscripción en '{seleccionado.nombreEvento}'?",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Busca la inscripción activa del aprendiz en ese evento
                    InscripcionService service = new InscripcionService();
                    List<InscripcionDetalle> inscripciones = service.ObtenerInscripcionesConDetalle();
                    // Filtra por idEvento y idAprendiz (almacenado en sesión)
                    InscripcionDetalle miInscripcion = inscripciones.Find(
                        i => i.idEvento == seleccionado.idEvento &&
                             i.idAprendiz == Session.IdAprendiz);

                    if (miInscripcion == null)
                    {
                        MessageBox.Show("No tienes una inscripción activa en este evento.");
                        return;
                    }

                    service.CancelarInscripcion(miInscripcion.idInscripcion);
                    MessageBox.Show("Inscripción cancelada correctamente.");
                    CargarEventos(); // Refresca (aunque no cambia la lista, actualiza estado visual)
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}