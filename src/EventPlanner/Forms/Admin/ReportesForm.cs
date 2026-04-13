using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EventPlanner
{
    public partial class ReportesForm : Form
    {
        private string rolUsuario;
        private string tipoActual = "";

        // ✅ Services como atributos (consistente con arquitectura)
        private EventoService eventoService = new EventoService();
        private AprendizService aprendizService = new AprendizService();

        public ReportesForm(string rol)
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
            rolUsuario = rol;
        }

        private void ReportesForm_Load(object sender, EventArgs e)
        {
            cmbTipoReporte.Items.Clear();
            cmbTipoReporte.Items.Add("Evento");
            cmbTipoReporte.Items.Add("Aprendiz");
            cmbTipoReporte.Items.Add("Ambos");
            cmbTipoReporte.SelectedIndex = -1;
        }

        // =====================================================
        // VALIDACIÓN CENTRALIZADA
        // =====================================================
        private bool ValidarFormulario()
        {
            if (cmbTipoReporte.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de reporte.");
                return false;
            }

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor.");
                return false;
            }

            return true;
        }

        // =====================================================
        // GENERAR REPORTE
        // =====================================================
        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            tipoActual = cmbTipoReporte.SelectedItem.ToString();

            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddHours(23).AddMinutes(59);

            try
            {
                dgvReporte.DataSource = null;
                dgvReporte.Columns.Clear();

                switch (tipoActual)
                {
                    case "Evento":
                        CargarEventos(desde, hasta);
                        break;

                    case "Aprendiz":
                        CargarAprendices(desde, hasta);
                        break;

                    case "Ambos":
                        CargarAmbos(desde, hasta); // ← antes llamaba solo CargarEventos
                        break;
                }

                if (dgvReporte.Rows.Count == 0)
                    MessageBox.Show("No hay datos en ese rango.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // =====================================================
        // EVENTOS
        // =====================================================
        private void CargarEventos(DateTime desde, DateTime hasta)
        {
            List<Evento> eventos =
                eventoService.ObtenerEventosPorFecha(desde, hasta);

            dgvReporte.DataSource = eventos;

            dgvReporte.Columns["idEvento"].HeaderText = "ID";
            dgvReporte.Columns["nombreEvento"].HeaderText = "Nombre";
            dgvReporte.Columns["categoriaEvento"].HeaderText = "Categoria";
            dgvReporte.Columns["tipoInscripcion"].HeaderText = "Tipo Inscripción";
            dgvReporte.Columns["lugarEvento"].HeaderText = "Lugar";
            dgvReporte.Columns["fechaInicioEvento"].HeaderText = "Fecha";
            dgvReporte.Columns["cupoMaximo"].HeaderText = "Cupo";
            dgvReporte.Columns["activo"].HeaderText = "Activo";

            // ocultar 
            dgvReporte.Columns["descripcionEvento"].Visible = false;
            dgvReporte.Columns["fechaFinEvento"].Visible = false;
            dgvReporte.Columns["fechaInicioInscripcion"].Visible = false;
            dgvReporte.Columns["fechaFinInscripcion"].Visible = false;
            dgvReporte.Columns["idUsuarioCreador"].Visible = false;
        }

        // =====================================================
        // APRENDICES
        // =====================================================
        private void CargarAprendices(DateTime desde, DateTime hasta)
        {
            List<Aprendiz> lista =
                aprendizService.ObtenerAprendicesPorFecha(desde, hasta);

            dgvReporte.DataSource = lista;

            dgvReporte.Columns["idAprendiz"].HeaderText = "ID";
            dgvReporte.Columns["cedulaAprendiz"].HeaderText = "Cédula";
            dgvReporte.Columns["nombreAprendiz"].HeaderText = "Nombre";
            dgvReporte.Columns["correoAprendiz"].HeaderText = "Correo";
            dgvReporte.Columns["telefonoAprendiz"].HeaderText = "Teléfono";

            dgvReporte.Columns["edadAprendiz"].Visible = false;
            dgvReporte.Columns["generoAprendiz"].Visible = false;
            dgvReporte.Columns["codigoFicha"].Visible = false;
            dgvReporte.Columns["idUsuario"].Visible = false;
        }

        // =====================================================
        // AMBOS — muestra eventos y aprendices en filas combinadas
        // =====================================================
        private void CargarAmbos(DateTime desde, DateTime hasta)
        {
            // Usamos InscripcionDetalle que ya tiene JOIN de aprendiz + evento
            InscripcionService inscripcionService = new InscripcionService();
            List<InscripcionDetalle> lista = inscripcionService.ObtenerInscripcionesConDetalle();

            // Filtrar por rango: solo inscripciones en eventos dentro del rango
            List<Evento> eventosRango = eventoService.ObtenerEventosPorFecha(desde, hasta);

            HashSet<int> idsValidos = new HashSet<int>();
            foreach (var ev in eventosRango)
                idsValidos.Add(ev.idEvento);

            List<InscripcionDetalle> filtrada = lista.FindAll(i => idsValidos.Contains(i.idEvento));

            dgvReporte.DataSource = filtrada;

            dgvReporte.Columns["idInscripcion"].Visible = false;
            dgvReporte.Columns["idAprendiz"].Visible = false;
            dgvReporte.Columns["idEvento"].Visible = false;

            dgvReporte.Columns["nombreAprendiz"].HeaderText = "Aprendiz";
            dgvReporte.Columns["nombreEvento"].HeaderText = "Evento";
            dgvReporte.Columns["modalidad"].HeaderText = "Modalidad";
            dgvReporte.Columns["estadoInscripcion"].HeaderText = "Estado";
        }

        // =====================================================
        // EXPORTAR PDF
        // =====================================================
        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            if (dgvReporte.Rows.Count == 0)
            {
                MessageBox.Show("Genera el reporte primero.");
                return;
            }

            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "PDF (*.pdf)|*.pdf";
            dialogo.FileName = $"Reporte_{tipoActual}_{DateTime.Now:yyyyMMdd}";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportarPDF(dialogo.FileName);
                    MessageBox.Show("PDF generado.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void ExportarPDF(string ruta)
        {
            Document doc = new Document(PageSize.A4, 30, 30, 40, 40);
            PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
            doc.Open();

            Font titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
            Font texto = FontFactory.GetFont(FontFactory.HELVETICA, 9);

            doc.Add(new Paragraph($"Reporte de {tipoActual}", titulo));
            doc.Add(new Paragraph($"Generado: {DateTime.Now}", texto));
            doc.Add(new Paragraph(" "));

            PdfPTable tabla = new PdfPTable(dgvReporte.Columns.Count);
            tabla.WidthPercentage = 100;

            foreach (DataGridViewColumn col in dgvReporte.Columns)
            {
                tabla.AddCell(new Phrase(col.HeaderText));
            }

            foreach (DataGridViewRow fila in dgvReporte.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                {
                    tabla.AddCell(celda.Value?.ToString() ?? "");
                }
            }

            doc.Add(tabla);
            doc.Close();
        }

        // =====================================================
        // VOLVER
        // =====================================================
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}