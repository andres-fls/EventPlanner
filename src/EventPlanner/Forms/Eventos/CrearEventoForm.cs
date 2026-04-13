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
            // ===============================
            // VALIDACIONES UI
            // ===============================
            txtNombre.KeyPress += Validator.SoloLetrasKeyPress;

            // ===============================
            // COMBOS
            // ===============================
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
                cmbCategEvento.SelectedItem = _eventoEditar.categoriaEvento;
                txtLugar.Text = _eventoEditar.lugarEvento;
                txtDescripcion.Text = _eventoEditar.descripcionEvento;

                dtpFechaEve.Value = _eventoEditar.fechaInicioEvento;
                dtpHoraEve.Value = _eventoEditar.fechaInicioEvento;

                dtpFechaEveFin.Value = _eventoEditar.fechaFinEvento;   
                dtpHoraEveFin.Value = _eventoEditar.fechaFinEvento;   

                dtpFechaIni.Value = _eventoEditar.fechaInicioInscripcion;
                dtpHoraIni.Value = _eventoEditar.fechaInicioInscripcion;

                dtpFechaFin.Value = _eventoEditar.fechaFinInscripcion;
                dtpHoraFin.Value = _eventoEditar.fechaFinInscripcion;

                numCupo.Value = _eventoEditar.cupoMaximo;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =====================================================
        // BOTON GUARDAR (ULTRA LIMPIO)
        // =====================================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // -----------------------------
            // VALIDACIONES UI (SOLO UI)
            // -----------------------------
            if (Validator.CampoVacio(txtNombre, "Nombre del evento")) return;
            if (Validator.CampoVacio(txtLugar, "Lugar")) return;

            if (cmbCategEvento.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            try
            {
                // -----------------------------
                // CREAR OBJETO MODELO
                // -----------------------------
                Evento evento = new Evento()
                {
                    nombreEvento = txtNombre.Text.Trim(),
                    categoriaEvento = cmbCategEvento.SelectedItem.ToString(),
                    lugarEvento = txtLugar.Text.Trim(),
                    descripcionEvento = txtDescripcion.Text.Trim(),

                    fechaInicioEvento =
                        dtpFechaEve.Value.Date + dtpHoraEve.Value.TimeOfDay,

                    fechaFinEvento =
                        dtpFechaEveFin.Value.Date + dtpHoraEveFin.Value.TimeOfDay,

                    fechaInicioInscripcion =
                        dtpFechaIni.Value.Date + dtpHoraIni.Value.TimeOfDay,

                    fechaFinInscripcion =
                        dtpFechaFin.Value.Date + dtpHoraFin.Value.TimeOfDay,

                    cupoMaximo = (int)numCupo.Value,
                    activo = true,
                    idUsuarioCreador = Session.IdUsuario
                };

                EventoService service = new EventoService();

                // -----------------------------
                // CREAR O EDITAR
                // -----------------------------
                if (_eventoEditar == null)
                {
                    service.CrearEvento(evento);
                    MessageBox.Show("Evento creado correctamente.");
                }
                else
                {
                    evento.idEvento = _eventoEditar.idEvento;
                    service.ActualizarEvento(evento);
                    MessageBox.Show("Evento actualizado correctamente.");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                // Los errores vienen del SERVICE
                MessageBox.Show(ex.Message);
            }
        }
    }
}