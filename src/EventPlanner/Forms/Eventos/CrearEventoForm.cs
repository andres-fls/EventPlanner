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
            // Restringe la entrada en txtNombre a solo letras (usando el validador)
            txtNombre.KeyPress += Validator.SoloLetrasKeyPress; // Agrega evento KeyPress para validación
            // Inicializa el ComboBox sin selección
            cmbTipo.SelectedIndex = -1; // Establece índice -1 para no selección

            // Si estamos en modo edición, cargar los datos del evento en los controles
            if (_eventoEditar != null) // Verifica si hay evento para editar
            {
                this.Text = "Editar Evento"; // Cambia el título de la ventana
                txtNombre.Text = _eventoEditar.nombreEvento; // Carga nombre
                cmbTipo.Text = _eventoEditar.tipoEvento; // Carga tipo
                txtLugar.Text = _eventoEditar.lugarEvento; // Carga lugar
                txtDescripcion.Text = _eventoEditar.descripcionEvento; // Carga descripción
                // Fecha del evento: separamos fecha y hora
                dtpFechaEve.Value = _eventoEditar.fechaInicioEvento.Date; // Carga fecha del evento
                dtpHoraEve.Value = _eventoEditar.fechaInicioEvento; // Carga hora del evento
                // Inicio inscripción
                dtpFechaIni.Value = _eventoEditar.fechaInicioInscripcion.Date; // Carga fecha inicio inscripción
                dtpHoraIni.Value = _eventoEditar.fechaInicioInscripcion; // Carga hora inicio inscripción
                // Fin inscripción
                dtpFechaFin.Value = _eventoEditar.fechaFinInscripcion.Date; // Carga fecha fin inscripción
                dtpHoraFin.Value = _eventoEditar.fechaFinInscripcion; // Carga hora fin inscripción
                numCupo.Value = _eventoEditar.cupoMaximo; // Carga cupo máximo
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
            // Validación: campos de texto no vacíos
            if (Validator.CampoVacio(txtNombre, "Nombre del evento")) return; // Valida nombre no vacío
            if (Validator.CampoVacio(txtLugar, "Lugar")) return; // Valida lugar no vacío

            // Validación: tipo de evento seleccionado
            if (cmbTipo.SelectedIndex == -1) // Verifica selección en combo
            {
                MessageBox.Show("Seleccione un tipo de evento."); // Muestra mensaje de error
                return; // Sale de la función
            }

            // Combinar fecha y hora para cada campo DateTime
            DateTime fechaHoraEvento = dtpFechaEve.Value.Date + dtpHoraEve.Value.TimeOfDay; // Combina fecha y hora evento
            DateTime fechaHoraIniInscripcion = dtpFechaIni.Value.Date + dtpHoraIni.Value.TimeOfDay; // Combina fecha y hora inicio
            DateTime fechaHoraFinInscripcion = dtpFechaFin.Value.Date + dtpHoraFin.Value.TimeOfDay; // Combina fecha y hora fin

            // Validación: la fecha de fin de inscripción debe ser posterior a la de inicio
            if (fechaHoraFinInscripcion <= fechaHoraIniInscripcion) // Verifica lógica de fechas
            {
                MessageBox.Show("La fecha de cierre de inscripciones debe ser posterior a la de inicio."); // Muestra error
                return; // Sale
            }

            // Validación: las inscripciones deben cerrarse antes del evento
            if (fechaHoraFinInscripcion >= fechaHoraEvento) // Verifica que cierre antes del evento
            {
                MessageBox.Show("Las inscripciones deben cerrarse antes de la fecha del evento."); // Muestra error
                return; // Sale
            }

            try // Bloque try para manejo de excepciones
            {
                // Crear objeto Evento con los datos del formulario
                Evento evento = new Evento() // Instancia nuevo objeto Evento
                {
                    nombreEvento = txtNombre.Text.Trim(), // Asigna nombre
                    tipoEvento = cmbTipo.SelectedItem.ToString(), // Asigna tipo
                    lugarEvento = txtLugar.Text.Trim(), // Asigna lugar
                    descripcionEvento = txtDescripcion.Text.Trim(), // Asigna descripción

                    fechaInicioEvento = fechaHoraEvento, // Asigna fecha inicio evento
                    fechaFinEvento = fechaHoraEvento,  // Asigna fecha fin evento (igual a inicio)

                    fechaInicioInscripcion = fechaHoraIniInscripcion, // Asigna fecha inicio inscripción
                    fechaFinInscripcion = fechaHoraFinInscripcion, // Asigna fecha fin inscripción

                    cupoMaximo = (int)numCupo.Value, // Asigna cupo máximo
                    activo = true,                      // Establece activo por defecto
                    idUsuarioCreador = Session.IdUsuario // Asigna ID del creador
                };

                EventoService service = new EventoService(); // Instancia servicio de eventos

                if (_eventoEditar == null) // Verifica si es creación
                {
                    // Modo creación: llama al servicio para insertar
                    service.CrearEvento(evento); // Crea evento
                    MessageBox.Show("Evento guardado exitosamente."); // Muestra éxito
                }
                else // Modo edición
                {
                    // Modo edición: asigna el ID y el usuario creador original, luego actualiza
                    evento.idEvento = _eventoEditar.idEvento; // Asigna ID
                    evento.idUsuarioCreador = _eventoEditar.idUsuarioCreador; // Asigna creador original
                    service.ActualizarEvento(evento); // Actualiza evento
                    MessageBox.Show("Evento actualizado exitosamente."); // Muestra éxito
                }

                this.Close(); // Cierra el formulario después de guardar
            }
            catch (Exception ex) // Captura excepciones
            {
                MessageBox.Show("Error: " + ex.Message); // Muestra error
            }
        }
    }
}