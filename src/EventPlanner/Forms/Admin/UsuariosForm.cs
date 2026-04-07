// ============================================================
// Archivo: UsuariosForm.cs
// Propósito: Formulario para la gestión de inscripciones de usuarios.
// NOTA: Actualmente es una versión de demostración/prototipo que
//       utiliza datos de ejemplo (hardcoded). Está diseñado para
//       mostrar, filtrar, activar/desactivar inscripciones.
//       En el futuro debe conectarse a la base de datos real.
// ============================================================

using System; // Importa base de .NET
using System.Windows.Forms; // Importa Windows Forms
using EventPlanner.Utils; // Importa utilidades
using EventPlanner.Services;
using EventPlanner.Models;
using System.Collections.Generic;

namespace EventPlanner
{
    public partial class UsuariosForm : Form
    {
        // Almacena el rol del usuario logueado (aunque no se usa en este prototipo)
        private string rolUsuario;

        private InscripcionService inscripcionService = new InscripcionService(); // Servicio para manejar inscripciones

        private string ObtenerValorCelda(DataGridViewRow fila, int indice)
        {
            var valor = fila.Cells[indice].Value;

            if (valor == null || string.IsNullOrWhiteSpace(valor.ToString()))
                return "Campo vacío";

            return valor.ToString();
        }

        // Constructor: recibe el rol y aplica tamaño de ventana desde configuración
        public UsuariosForm(string rol)
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
            rolUsuario = rol;
        }

        // Evento vacío (generado por diseñador)
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Sin implementación
        }

        // Al cargar el formulario: configura el DataGridView, los filtros y carga datos de ejemplo
        private void UsuariosForm_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();    // Define las columnas del grid
            ConfigurarFiltros(); // Llena el ComboBox con opciones de filtro
            CargarDatos();       // Carga datos de ejemplo (mock)
        }

        // Configura la estructura del DataGridView (columnas y comportamiento)
        private void ConfigurarGrid()
        {
            dgvInscripciones.Columns.Clear(); // Limpia columnas existentes

            dgvInscripciones.AllowUserToAddRows = false; // No permite agregar filas manualmente

            // Agrega columnas manualmente (no se usa DataBinding)
            dgvInscripciones.Columns.Add("Nombre", "Nombre");
            dgvInscripciones.Columns.Add("Evento", "Evento");
            dgvInscripciones.Columns.Add("Modalidad", "Modalidad");
            dgvInscripciones.Columns.Add("Estado", "Estado");

            dgvInscripciones.ReadOnly = true;                     // Solo lectura
            dgvInscripciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selecciona fila completa
            dgvInscripciones.MultiSelect = false;                 // Una sola fila a la vez
        }

        // Configura el ComboBox de filtros con opciones de ejemplo
        private void ConfigurarFiltros()
        {
            cmbEvento.Items.Clear();

            cmbEvento.Items.Add("Todos");
            cmbEvento.Items.Add("Hackathon");
            cmbEvento.Items.Add("Feria Tech");

            cmbEvento.SelectedIndex = 0; // Selecciona "Todos" por defecto
        }

        // Carga datos de ejemplo (mock) en el DataGridView.
        // EN PRODUCCIÓN: Debería obtener datos reales desde la base de datos usando DAOs/Services.
        private void CargarDatos()
        {
            dgvInscripciones.Rows.Clear();

            var inscripciones = inscripcionService.ObtenerInscripcionesConDetalle();

            foreach (var ins in inscripciones)
            {
                dgvInscripciones.Rows.Add(
                    ins.nombreAprendiz,
                    ins.nombreEvento,
                    ins.modalidad,
                    ins.estadoInscripcion
                );
            }
        }

        // Botón "Cargar": refresca los datos (vuelve a cargar los mismos datos de ejemplo)
        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarDatos();
            MessageBox.Show("Datos actualizados.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Botón "Detalle": muestra la información de la inscripción seleccionada
        private void btnDetalle_Click(object sender, EventArgs e)
        {
            // Verifica que haya una fila seleccionada
            if (dgvInscripciones.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro.");
                return;
            }

            var fila = dgvInscripciones.CurrentRow;

            string nombre = ObtenerValorCelda(fila, 0);
            string evento = ObtenerValorCelda(fila, 1);
            string modalidad = ObtenerValorCelda(fila, 2);
            string estado = ObtenerValorCelda(fila, 3);

            MessageBox.Show(
                $"Nombre: {nombre}\n" +
                $"Evento: {evento}\n" +
                $"Modalidad: {modalidad}\n" +
                $"Estado: {estado}",
                "Detalle de Inscripción"
            );
        }
        // Botón "Activar": cambia el estado de la inscripción a "Activo" (solo visual)
        // FUTURO: Debería llamar a un método del DAO para persistir el cambio.
        private void btnActivar_Click(object sender, EventArgs e)
        {
            if (dgvInscripciones.CurrentRow == null ||
                dgvInscripciones.CurrentRow.IsNewRow ||
                dgvInscripciones.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("Seleccione un registro válido.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Desea ACTIVAR esta inscripción?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                // Actualiza la celda "Estado" en el grid
                dgvInscripciones.CurrentRow.Cells[3].Value = "Activo";

                // FUTURO: Llamar a la capa de datos para actualizar el estado en la BD
                // Ejemplo: inscripcionDAO.ActualizarEstado(idInscripcion, "Activo");

                MessageBox.Show("Estado actualizado.");
            }
        }

        // Botón "Desactivar": cambia el estado de la inscripción a "Cancelado" (solo visual)
        // FUTURO: Debería persistir el cambio en la base de datos.
        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (dgvInscripciones.CurrentRow == null ||
                dgvInscripciones.CurrentRow.IsNewRow ||
                dgvInscripciones.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("Seleccione un registro válido.");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Desea DESACTIVAR esta inscripción?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                dgvInscripciones.CurrentRow.Cells[3].Value = "Cancelado";

                // FUTURO: inscripcionDAO.ActualizarEstado(idInscripcion, "Cancelado");

                MessageBox.Show("Estado actualizado.");
            }
        }

        // Botón "Filtrar": oculta/muestra filas según el evento seleccionado en el ComboBox
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string evento = cmbEvento.SelectedItem.ToString();

            // Recorre todas las filas del grid
            foreach (DataGridViewRow fila in dgvInscripciones.Rows)
            {
                if (fila.IsNewRow) continue; // Ignora la fila de nuevo registro (vacía)

                // Si seleccionó "Todos", muestra todas; si no, solo las que coinciden con el evento
                if (evento == "Todos")
                    fila.Visible = true;
                else
                    fila.Visible = fila.Cells[1].Value.ToString() == evento;
            }
        }

        // Botón "Volver": cierra el formulario actual
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}