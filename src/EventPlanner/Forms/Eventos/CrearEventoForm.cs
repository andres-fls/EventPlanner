// ============================================================
// Archivo: CrearEventoForm.cs
// Propósito: Formulario para crear o editar un evento.
// Permite ingresar nombre, tipo, lugar, descripción, fechas
// (evento y período de inscripción) y cupo máximo.
// Aplica validaciones antes de guardar y utiliza los servicios
// correspondientes para persistir los datos.
// ============================================================

using EventPlanner.Models; // Importa los modelos de datos de la aplicación
using EventPlanner.Services; // Importa los servicios para lógica de negocio
using EventPlanner.Utils; // Importa utilidades como validadores y configuración
using System; // Importa el espacio de nombres base de .NET
using System.Windows.Forms; // Importa Windows Forms para la interfaz gráfica

namespace EventPlanner // Define el espacio de nombres de la aplicación
{
    public partial class CrearEventoForm : Form // Clase del formulario para crear/editar eventos, hereda de Form
    {
        // Rol del usuario actual (para control de permisos, aunque no se usa directamente aquí)
        private string rolUsuario; // Variable privada para almacenar el rol del usuario
        // Si no es null, indica que estamos en modo edición (contiene el evento a modificar)
        private Evento _eventoEditar; // Variable privada para el evento en edición

        // Constructor: recibe el rol y opcionalmente un evento para editar
        public CrearEventoForm(string rol, Evento eventoEditar = null) // Constructor público con parámetros
        {
            InitializeComponent(); // Inicializa los componentes del formulario
            rolUsuario = rol; // Asigna el rol recibido
            _eventoEditar = eventoEditar; // Asigna el evento a editar si existe
        }

        // Al cargar el formulario, configura validaciones y, si es edición, carga los datos del evento
        private void CrearEventoForm_Load(object sender, EventArgs e) // Evento Load del formulario
        {
            // ===============================
            // VALIDACIONES INPUT
            // ===============================
            txtNombre.KeyPress += Validator.SoloLetrasKeyPress;

            // ===============================
            // CONFIGURAR COMBOS (LIMPIAR PRIMERO)
            // ===============================

            cmbTipoEvento.Items.Clear();
            cmbTipoEvento.Items.Add("Individual");
            cmbTipoEvento.Items.Add("Grupal");
            cmbTipoEvento.SelectedIndex = -1;

            cmbCategEvento.Items.Clear();
            cmbCategEvento.Items.Add("Academico");
            cmbCategEvento.Items.Add("Deportivo");
            cmbCategEvento.Items.Add("Cultural");
            cmbCategEvento.SelectedIndex = -1;

            // ===============================
            // MODO EDICION
            // ===============================
            if (_eventoEditar != null)
            {
                this.Text = "Editar Evento";

                txtNombre.Text = _eventoEditar.nombreEvento;
                cmbTipoEvento.SelectedItem = _eventoEditar.tipoEvento;
                cmbCategEvento.SelectedItem = _eventoEditar.categoriaEvento;
                txtLugar.Text = _eventoEditar.lugarEvento;
                txtDescripcion.Text = _eventoEditar.descripcionEvento;

                dtpFechaEve.Value = _eventoEditar.fechaInicioEvento;
                dtpHoraEve.Value = _eventoEditar.fechaInicioEvento;

                dtpFechaIni.Value = _eventoEditar.fechaInicioInscripcion;
                dtpHoraIni.Value = _eventoEditar.fechaInicioInscripcion;

                dtpFechaFin.Value = _eventoEditar.fechaFinInscripcion;
                dtpHoraFin.Value = _eventoEditar.fechaFinInscripcion;

                numCupo.Value = _eventoEditar.cupoMaximo;
            }
        }

        // Botón Cancelar: cierra el formulario sin guardar
        private void btnCancelar_Click(object sender, EventArgs e) // Evento Click del botón Cancelar
        {
            this.Close(); // Cierra el formulario
        }

        // Botón Guardar: valida los datos, crea/actualiza el evento y lo guarda en la BD
        private void btnGuardar_Click(object sender, EventArgs e) // Evento Click del botón Guardar
        {
            // =============================
            // VALIDACIONES BASICAS
            // =============================

            if (Validator.CampoVacio(txtNombre, "Nombre del evento")) return;
            if (Validator.CampoVacio(txtLugar, "Lugar")) return;

            if (cmbTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de evento.");
                return;
            }

            // =============================
            // COMBINAR FECHA Y HORA
            // =============================

            DateTime fechaHoraEvento =
                dtpFechaEve.Value.Date + dtpHoraEve.Value.TimeOfDay;

            DateTime fechaHoraIniInscripcion =
                dtpFechaIni.Value.Date + dtpHoraIni.Value.TimeOfDay;

            DateTime fechaHoraFinInscripcion =
                dtpFechaFin.Value.Date + dtpHoraFin.Value.TimeOfDay;

            // =============================
            // VALIDACIONES DE FECHA
            // =============================

            // ❌ No permitir eventos pasados
            if (fechaHoraEvento < DateTime.Now)
            {
                MessageBox.Show("No puede crear eventos en fechas pasadas.");
                return;
            }

            if (fechaHoraFinInscripcion <= fechaHoraIniInscripcion)
            {
                MessageBox.Show("La fecha de cierre debe ser posterior al inicio.");
                return;
            }

            if (fechaHoraFinInscripcion >= fechaHoraEvento)
            {
                MessageBox.Show("Las inscripciones deben cerrarse antes del evento.");
                return;
            }

            // =============================
            // VALIDACION HORARIO (7AM - 7PM)
            // =============================

            int horaEvento = fechaHoraEvento.Hour;

            if (horaEvento < 7 || horaEvento >= 19)
            {
                MessageBox.Show("Los eventos solo pueden realizarse entre 7:00 AM y 7:00 PM.");
                return;
            }

            // =============================
            // CREAR EVENTO
            // =============================

            try
            {
                Evento evento = new Evento()
                {
                    nombreEvento = txtNombre.Text.Trim(),
                    tipoEvento = cmbTipo.SelectedItem?.ToString(),
                    lugarEvento = txtLugar.Text.Trim(),
                    descripcionEvento = txtDescripcion.Text.Trim(),

                    fechaInicioEvento = fechaHoraEvento,
                    fechaFinEvento = fechaHoraEvento,

                    fechaInicioInscripcion = fechaHoraIniInscripcion,
                    fechaFinInscripcion = fechaHoraFinInscripcion,

                    cupoMaximo = (int)numCupo.Value,
                    activo = true
                };

                EventoService service = new EventoService();

                service.CrearEvento(evento);

                MessageBox.Show("Evento creado correctamente.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}