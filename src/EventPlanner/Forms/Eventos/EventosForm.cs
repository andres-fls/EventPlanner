// ============================================================
// Archivo: EventosForm.cs
// Propósito: Ventana que muestra la lista de eventos y permite
//            gestionarlos según el rol del usuario.
// - Aprendiz: puede ver eventos, inscribirse y cancelar inscripción.
// - Admin: puede crear, editar, desactivar eventos (no se inscribe).
// - Instructor: tiene permisos similares a Admin (por diseño).
// ============================================================

using EventPlanner.Models; // Importa modelos de datos
using EventPlanner.Services; // Importa servicios de negocio
using EventPlanner.Utils; // Importa utilidades
using System; // Importa base de .NET
using System.Collections.Generic; // Importa colecciones genéricas
using System.Windows.Forms; // Importa Windows Forms

namespace EventPlanner // Espacio de nombres de la aplicación
{
    public partial class EventosForm : Form // Clase del formulario de eventos
    {
        // Almacena el rol del usuario logueado para controlar la UI
        private string rolUsuario; // Variable para rol del usuario

        // Constructor: recibe el rol y aplica tamaño de ventana desde configuración
        public EventosForm(string rol) // Constructor público
        {
            InitializeComponent(); // Inicializa componentes
            this.Size = AppConfig.TamanoVentana; // Aplica tamaño desde config
            rolUsuario = rol; // Asigna rol
        }

        // Evento que se ejecuta al cargar el formulario.
        // Oculta botones según el rol y carga la lista de eventos.
        private void EventosForm_Load(object sender, EventArgs e) // Evento Load
        {
            MessageBox.Show("Rol: [" + rolUsuario + "]");

            // Si es aprendiz: no puede crear, editar ni eliminar eventos
            if (rolUsuario == "Aprendiz") // Verifica rol aprendiz
            {
                btnCrear.Visible = false; // Oculta botón crear
                btnEditar.Visible = false; // Oculta botón editar
                btnEliminar.Visible = false; // Oculta botón eliminar
            }
            // Si es administrador: no puede inscribirse ni cancelar inscripciones
            if (rolUsuario == "Administrador") // Verifica rol admin
            {
                btnInscribirse.Visible = false; // Oculta botón inscribirse
                btnCancelarInscripcion.Visible = false; // Oculta botón cancelar
            }
            // Carga los eventos al abrir la ventana
            CargarEventos(); // Llama a método cargar eventos
        }

        // Evento vacío (probablemente generado automáticamente por el diseñador)
        private void panelBase_Paint(object sender, PaintEventArgs e) // Evento Paint del panel
        {
            
        }

        // Botón "Crear": abre el formulario de creación de eventos y refresca la lista
        private void btnCrear_Click(object sender, EventArgs e) // Evento Click de crear
        {
            CrearEventoForm crearEventoForm = new CrearEventoForm(rolUsuario); // Instancia formulario crear
            crearEventoForm.ShowDialog(); // Muestra modal
            CargarEventos(); // Refresca lista
        }

        // Carga todos los eventos desde la base de datos y los muestra en el DataGridView
        private void CargarEventos() // Método para cargar eventos
        {
            try // Bloque try
            {
                EventoService service = new EventoService(); // Instancia servicio
                List<Evento> eventos = service.ObtenerEventos(); // Obtiene lista de eventos

                // Asigna la lista como origen de datos del DataGridView
                dgvEventos.DataSource = null; // Limpia datasource
                dgvEventos.DataSource = eventos; // Asigna lista

                // Personaliza los encabezados de las columnas para que sean más amigables
                dgvEventos.Columns["idEvento"].HeaderText = "ID"; // Cambia header ID
                dgvEventos.Columns["nombreEvento"].HeaderText = "Nombre"; // Cambia header Nombre
                dgvEventos.Columns["tipoEvento"].HeaderText = "Tipo"; // Cambia header Tipo
                dgvEventos.Columns["lugarEvento"].HeaderText = "Lugar"; // Cambia header Lugar
                dgvEventos.Columns["fechaInicioEvento"].HeaderText = "Fecha"; // Cambia header Fecha
                dgvEventos.Columns["cupoMaximo"].HeaderText = "Cupo"; // Cambia header Cupo
                dgvEventos.Columns["activo"].HeaderText = "Activo"; // Cambia header Activo

                // Oculta columnas que no son necesarias para el usuario final
                dgvEventos.Columns["descripcionEvento"].Visible = false; // Oculta descripción
                dgvEventos.Columns["fechaFinEvento"].Visible = false; // Oculta fecha fin
                dgvEventos.Columns["fechaInicioInscripcion"].Visible = false; // Oculta inicio inscripción
                dgvEventos.Columns["fechaFinInscripcion"].Visible = false; // Oculta fin inscripción
                dgvEventos.Columns["idUsuarioCreador"].Visible = false; // Oculta creador
            }
            catch (Exception ex) // Captura excepciones
            {
                MessageBox.Show("Error al cargar eventos: " + ex.Message); // Muestra error
            }
        }

        // Botón "Volver": regresa al menú principal y cierra este formulario
        private void btnVolver_Click(object sender, EventArgs e) // Evento Click de volver
        {
            MenuForm menu = new MenuForm(rolUsuario); // Instancia menú
            menu.Show(); // Muestra menú
            this.Close(); // Cierra formulario
        }

        // Botón "Editar": permite modificar el evento seleccionado (solo admin)
        private void btnEditar_Click(object sender, EventArgs e) // Evento Click de editar
        {
            // Verifica que haya una fila seleccionada
            if (dgvEventos.SelectedRows.Count == 0) // Verifica selección
            {
                MessageBox.Show("Selecciona un evento para editar."); // Muestra mensaje
                return; // Sale
            }

            // Obtiene el evento seleccionado desde el DataBoundItem
            Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem; // Obtiene evento seleccionado
            // Abre el formulario de creación en modo edición
            CrearEventoForm editar = new CrearEventoForm(rolUsuario, seleccionado); // Instancia editar
            editar.ShowDialog(); // Muestra modal
            CargarEventos(); // Refresca
        }

        // Botón "Eliminar" (en realidad desactiva el evento - soft delete)
        private void btnEliminar_Click(object sender, EventArgs e) // Evento Click de eliminar
        {
            if (dgvEventos.SelectedRows.Count == 0) // Verifica selección
            {
                MessageBox.Show("Selecciona un evento para eliminar."); // Muestra mensaje
                return; // Sale
            }

            // Confirmación antes de desactivar
            DialogResult confirmacion = MessageBox.Show( // Muestra confirmación
                "¿Estás seguro de que deseas desactivar este evento?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes) // Si confirma
            {
                Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem; // Obtiene seleccionado
                EventoService service = new EventoService(); // Instancia servicio
                service.DesactivarEvento(seleccionado.idEvento); // Desactiva evento
                MessageBox.Show("Evento desactivado correctamente."); // Muestra éxito
                CargarEventos(); // Refresca
            }
        }

        // Botón "Inscribirse": permite a un aprendiz inscribirse en el evento seleccionado
        private void btnInscribirse_Click(object sender, EventArgs e) // Evento Click de inscribirse
        {
            if (dgvEventos.SelectedRows.Count == 0) // Verifica selección
            {
                MessageBox.Show("Por favor, selecciona un evento para inscribirte."); // Muestra mensaje
                return; // Sale
            }

            Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem; // Obtiene seleccionado

            // Valida que el evento esté activo
            if (!seleccionado.activo) // Verifica activo
            {
                MessageBox.Show("Este evento no está activo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Muestra aviso
                return; // Sale
            }

            // Confirmación del usuario
            DialogResult confirmacion = MessageBox.Show( // Muestra confirmación
                $"¿Deseas inscribirte en el evento '{seleccionado.nombreEvento}'?",
                "Confirmar inscripción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes) // Si confirma
            {
                try // Bloque try
                {
                    // Construye el objeto Inscripcion con datos por defecto
                    Inscripcion inscripcion = new Inscripcion() // Instancia inscripción
                    {
                        idAprendiz = Session.IdAprendiz, // Asigna ID aprendiz
                        idEvento = seleccionado.idEvento, // Asigna ID evento
                        tipoInscripcion = "Individual", // Tipo individual
                        modalidad = "Presencial", // Modalidad presencial
                        estadoInscripcion = "Activo" // Estado activo
                    };

                    InscripcionService service = new InscripcionService(); // Instancia servicio
                    service.CrearInscripcion(inscripcion); // Crea inscripción

                    MessageBox.Show("Te has inscrito exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); // Muestra éxito
                }
                catch (Exception ex) // Captura excepciones
                {
                    MessageBox.Show("Error al inscribirse: " + ex.Message); // Muestra error
                }
            }
        }

        // Botón "Cancelar Inscripción": permite al aprendiz cancelar su inscripción en el evento seleccionado
 
        private void btnCancelarInscripcion_Click_1(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0) // Verifica selección
            {
                MessageBox.Show("Selecciona un evento para cancelar tu inscripción."); // Muestra mensaje
                return; // Sale
            }

            Evento seleccionado = (Evento)dgvEventos.SelectedRows[0].DataBoundItem; // Obtiene seleccionado

            DialogResult confirmacion = MessageBox.Show( // Muestra confirmación
                $"¿Deseas cancelar tu inscripción en '{seleccionado.nombreEvento}'?",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes) // Si confirma
            {
                try // Bloque try
                {
                    // Busca la inscripción activa del aprendiz en ese evento
                    InscripcionService service = new InscripcionService(); // Instancia servicio
                    List<InscripcionDetalle> inscripciones = service.ObtenerInscripcionesConDetalle(); // Obtiene inscripciones
                    // Filtra por idEvento y idAprendiz (almacenado en sesión)
                    InscripcionDetalle miInscripcion = inscripciones.Find( // Busca inscripción
                        i => i.idEvento == seleccionado.idEvento &&
                             i.idAprendiz == Session.IdAprendiz);

                    if (miInscripcion == null) // Si no encuentra
                    {
                        MessageBox.Show("No tienes una inscripción activa en este evento."); // Muestra mensaje
                        return; // Sale
                    }

                    service.CancelarInscripcion(miInscripcion.idInscripcion); // Cancela inscripción
                    MessageBox.Show("Inscripción cancelada correctamente."); // Muestra éxito
                    CargarEventos(); // Refresca
                }
                catch (Exception ex) // Captura excepciones
                {
                    MessageBox.Show("Error: " + ex.Message); // Muestra error
                }
            }
        }
    }
}