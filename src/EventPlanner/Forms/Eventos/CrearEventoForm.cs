using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.Utils;
using System;
using System.Windows.Forms;

namespace EventPlanner
{
    public partial class CrearEventoForm : Form
    {
        private string rolUsuario;
        private Evento _eventoEditar;

        public CrearEventoForm(string rol, Evento eventoEditar = null)
        {
            InitializeComponent();
            rolUsuario = rol;
            _eventoEditar = eventoEditar;
        }

        private void CrearEventoForm_Load(object sender, EventArgs e)
        {
            txtNombre.KeyPress += Validator.SoloLetrasKeyPress;
            cmbTipo.SelectedIndex = -1;

            if (_eventoEditar != null)
            {
                this.Text = "Editar Evento";
                txtNombre.Text = _eventoEditar.nombreEvento;
                cmbTipo.Text = _eventoEditar.tipoEvento;
                txtLugar.Text = _eventoEditar.lugarEvento;
                txtDescripcion.Text = _eventoEditar.descripcionEvento;
                dtpFechaEve.Value = _eventoEditar.fechaInicioEvento.Date;
                dtpHoraEve.Value = _eventoEditar.fechaInicioEvento;
                dtpFechaIni.Value = _eventoEditar.fechaInicioInscripcion.Date;
                dtpHoraIni.Value = _eventoEditar.fechaInicioInscripcion;
                dtpFechaFin.Value = _eventoEditar.fechaFinInscripcion.Date;
                dtpHoraFin.Value = _eventoEditar.fechaFinInscripcion;
                numCupo.Value = _eventoEditar.cupoMaximo;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (Validator.CampoVacio(txtNombre, "Nombre del evento")) return;
            if (Validator.CampoVacio(txtLugar, "Lugar")) return;

            if (cmbTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de evento.");
                return;
            }

            DateTime fechaHoraEvento = dtpFechaEve.Value.Date + dtpHoraEve.Value.TimeOfDay;
            DateTime fechaHoraIniInscripcion = dtpFechaIni.Value.Date + dtpHoraIni.Value.TimeOfDay;
            DateTime fechaHoraFinInscripcion = dtpFechaFin.Value.Date + dtpHoraFin.Value.TimeOfDay;

            if (fechaHoraFinInscripcion <= fechaHoraIniInscripcion)
            {
                MessageBox.Show("La fecha de cierre de inscripciones debe ser posterior a la de inicio.");
                return;
            }

            if (fechaHoraFinInscripcion >= fechaHoraEvento)
            {
                MessageBox.Show("Las inscripciones deben cerrarse antes de la fecha del evento.");
                return;
            }

            try
            {
                Evento evento = new Evento()
                {
                    nombreEvento = txtNombre.Text.Trim(),
                    tipoEvento = cmbTipo.SelectedItem.ToString(),
                    lugarEvento = txtLugar.Text.Trim(),
                    descripcionEvento = txtDescripcion.Text.Trim(),

                    fechaInicioEvento = fechaHoraEvento,
                    fechaFinEvento = fechaHoraEvento,

                    fechaInicioInscripcion = fechaHoraIniInscripcion,
                    fechaFinInscripcion = fechaHoraFinInscripcion,

                    cupoMaximo = (int)numCupo.Value,
                    activo = true,
                    idUsuarioCreador = Session.IdUsuario
                };

                EventoService service = new EventoService();

                if (_eventoEditar == null)
                {
                    service.CrearEvento(evento);
                    MessageBox.Show("Evento guardado exitosamente.");
                }
                else
                {
                    evento.idEvento = _eventoEditar.idEvento;
                    evento.idUsuarioCreador = _eventoEditar.idUsuarioCreador;
                    service.ActualizarEvento(evento);
                    MessageBox.Show("Evento actualizado exitosamente.");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
    
}