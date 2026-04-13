using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EventPlanner.Services;
using EventPlanner.Models;
using EventPlanner.Utils;

namespace EventPlanner
{
    public partial class UsuariosForm : Form
    {
        private string rolUsuario;
        private InscripcionService inscripcionService = new InscripcionService();

        private List<InscripcionDetalle> listaInscripciones;

        public UsuariosForm(string rol)
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
            rolUsuario = rol;
        }

        private void UsuariosForm_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        // ==========================
        // CARGAR DATOS
        // ==========================
        private void CargarDatos()
        {
            try
            {
                listaInscripciones =
                    inscripcionService.ObtenerInscripcionesConDetalle();

                dgvInscripciones.DataSource = null;
                dgvInscripciones.DataSource = listaInscripciones;

                dgvInscripciones.Columns["idInscripcion"].Visible = false;
                dgvInscripciones.Columns["idAprendiz"].Visible = false;
                dgvInscripciones.Columns["idEvento"].Visible = false;

                dgvInscripciones.Columns["nombreAprendiz"].HeaderText = "Nombre";
                dgvInscripciones.Columns["nombreEvento"].HeaderText = "Evento";
                dgvInscripciones.Columns["modalidad"].HeaderText = "Modalidad";
                dgvInscripciones.Columns["estadoInscripcion"].HeaderText = "Estado";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }

        // ==========================
        // OBTENER SELECCIÓN
        // ==========================
        private InscripcionDetalle ObtenerSeleccion()
        {
            if (dgvInscripciones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro.");
                return null;
            }

            return (InscripcionDetalle)
                dgvInscripciones.SelectedRows[0].DataBoundItem;
        }

        // ==========================
        // ACTIVAR
        // ==========================
        private void btnActivar_Click(object sender, EventArgs e)
        {
            var seleccion = ObtenerSeleccion();
            if (seleccion == null) return;

            try
            {
                inscripcionService.ActualizarEstado(
                    seleccion.idInscripcion,
                    "Activo"
                );

                MessageBox.Show("Estado actualizado.");
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ==========================
        // DESACTIVAR
        // ==========================
        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            var seleccion = ObtenerSeleccion();
            if (seleccion == null) return;

            try
            {
                inscripcionService.ActualizarEstado(
                    seleccion.idInscripcion,
                    "Cancelado"
                );

                MessageBox.Show("Estado actualizado.");
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ==========================
        // DETALLE
        // ==========================
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            var seleccion = ObtenerSeleccion();
            if (seleccion == null) return;

            MessageBox.Show(
                $"Nombre: {seleccion.nombreAprendiz}\n" +
                $"Evento: {seleccion.nombreEvento}\n" +
                $"Modalidad: {seleccion.modalidad}\n" +
                $"Estado: {seleccion.estadoInscripcion}",
                "Detalle"
            );
        }

        // ==========================
        // REFRESCAR
        // ==========================
        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarDatos();
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