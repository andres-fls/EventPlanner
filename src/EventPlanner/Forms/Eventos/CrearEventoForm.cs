// ============================================================
// Archivo: CrearEventoForm.cs
// Propósito: Formulario para crear o editar un evento.
// Permite ingresar nombre, tipo, lugar, descripción, fechas
// (evento y período de inscripción) y cupo máximo.
// Aplica validaciones antes de guardar y utiliza los servicios
// correspondientes para persistir los datos.
// ============================================================

using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.Utils;
using System;
using System.Windows.Forms;

namespace EventPlanner
{
    public partial class CrearEventoForm : Form
    {
        // Rol del usuario actual (para control de permisos, aunque no se usa directamente aquí)
        private string rolUsuario;
        // Si no es null, indica que estamos en modo edición (contiene el evento a modificar)
        private Evento _eventoEditar;

        // Constructor: recibe el rol y opcionalmente un evento para editar
        public CrearEventoForm(string rol, Evento eventoEditar = null)
        {
            InitializeComponent();
            rolUsuario = rol;
            _eventoEditar = eventoEditar; // Si se pasa un evento, estamos editando
        }

        // Al cargar el formulario, configura validaciones y, si es edición, carga los datos del evento
        private void CrearEventoForm_Load(object sender, EventArgs e)
        {
            // Restringe la entrada en txtNombre a solo letras (usando el validador)
            txtNombre.KeyPress += Validator.SoloLetrasKeyPress;
            // Inicializa el ComboBox sin selección
            cmbTipo.SelectedIndex = -1;

            // Si estamos en modo edición, cargar los datos del evento en los controles
            if (_eventoEditar != null)
            {
                this.Text = "Editar Evento"; // Cambia el título de la ventana
                txtNombre.Text = _eventoEditar.nombreEvento;
                cmbTipo.Text = _eventoEditar.tipoEvento;
                txtLugar.Text = _eventoEditar.lugarEvento;
                txtDescripcion.Text = _eventoEditar.descripcionEvento;
                // Fecha del evento: separamos fecha y hora
                dtpFechaEve.Value = _eventoEditar.fechaInicioEvento.Date;
                dtpHoraEve.Value = _eventoEditar.fechaInicioEvento;
                // Inicio inscripción
                dtpFechaIni.Value = _eventoEditar.fechaInicioInscripcion.Date;
                dtpHoraIni.Value = _eventoEditar.fechaInicioInscripcion;
                // Fin inscripción
                dtpFechaFin.Value = _eventoEditar.fechaFinInscripcion.Date;
                dtpHoraFin.Value = _eventoEditar.fechaFinInscripcion;
                numCupo.Value = _eventoEditar.cupoMaximo;
            }
        }

        // Botón Cancelar: cierra el formulario sin guardar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Botón Guardar: valida los datos, crea/actualiza el evento y lo guarda en la BD
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación: campos de texto no vacíos
            if (Validator.CampoVacio(txtNombre, "Nombre del evento")) return;
            if (Validator.CampoVacio(txtLugar, "Lugar")) return;

            // Validación: tipo de evento seleccionado
            if (cmbTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de evento.");
                return;
            }

            // Combinar fecha y hora para cada campo DateTime
            DateTime fechaHoraEvento = dtpFechaEve.Value.Date + dtpHoraEve.Value.TimeOfDay;
            DateTime fechaHoraIniInscripcion = dtpFechaIni.Value.Date + dtpHoraIni.Value.TimeOfDay;
            DateTime fechaHoraFinInscripcion = dtpFechaFin.Value.Date + dtpHoraFin.Value.TimeOfDay;

            // Validación: la fecha de fin de inscripción debe ser posterior a la de inicio
            if (fechaHoraFinInscripcion <= fechaHoraIniInscripcion)
            {
                MessageBox.Show("La fecha de cierre de inscripciones debe ser posterior a la de inicio.");
                return;
            }

            // Validación: las inscripciones deben cerrarse antes del evento
            if (fechaHoraFinInscripcion >= fechaHoraEvento)
            {
                MessageBox.Show("Las inscripciones deben cerrarse antes de la fecha del evento.");
                return;
            }

            try
            {
                // Crear objeto Evento con los datos del formulario
                Evento evento = new Evento()
                {
                    nombreEvento = txtNombre.Text.Trim(),
                    tipoEvento = cmbTipo.SelectedItem.ToString(),
                    lugarEvento = txtLugar.Text.Trim(),
                    descripcionEvento = txtDescripcion.Text.Trim(),

                    fechaInicioEvento = fechaHoraEvento,
                    fechaFinEvento = fechaHoraEvento,  // Se usa la misma fecha/hora (el evento dura un día o se toma como inicio)

                    fechaInicioInscripcion = fechaHoraIniInscripcion,
                    fechaFinInscripcion = fechaHoraFinInscripcion,

                    cupoMaximo = (int)numCupo.Value,
                    activo = true,                      // Por defecto, el evento se crea activo
                    idUsuarioCreador = Session.IdUsuario // Usuario actual de la sesión
                };

                EventoService service = new EventoService();

                if (_eventoEditar == null)
                {
                    // Modo creación: llama al servicio para insertar
                    service.CrearEvento(evento);
                    MessageBox.Show("Evento guardado exitosamente.");
                }
                else
                {
                    // Modo edición: asigna el ID y el usuario creador original, luego actualiza
                    evento.idEvento = _eventoEditar.idEvento;
                    evento.idUsuarioCreador = _eventoEditar.idUsuarioCreador;
                    service.ActualizarEvento(evento);
                    MessageBox.Show("Evento actualizado exitosamente.");
                }

                this.Close(); // Cierra el formulario después de guardar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}