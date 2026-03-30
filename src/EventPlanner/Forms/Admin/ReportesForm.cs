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

        public ReportesForm(string rol)
        {
            InitializeComponent();
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

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (cmbTipoReporte.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de reporte.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tipoActual = cmbTipoReporte.SelectedItem.ToString();
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddHours(23).AddMinutes(59);

            try
            {
                dgvReporte.DataSource = null;
                dgvReporte.Columns.Clear();

                if (tipoActual == "Evento")
                {
                    CargarReporteEventos(desde, hasta);
                }
                else if (tipoActual == "Aprendiz")
                {
                    CargarReporteAprendices(desde, hasta);
                }
                else if (tipoActual == "Ambos")
                {
                    CargarReporteEventos(desde, hasta);
                }

                if (dgvReporte.Rows.Count == 0)
                    MessageBox.Show("No se encontraron registros en el rango de fechas seleccionado.", "Sin resultados",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message);
            }
        }

        private void CargarReporteEventos(DateTime desde, DateTime hasta)
        {
            EventoService service = new EventoService();
            List<Evento> eventos = service.ObtenerEventosPorFecha(desde, hasta);

            dgvReporte.DataSource = eventos;

            dgvReporte.Columns["idEvento"].HeaderText = "ID";
            dgvReporte.Columns["nombreEvento"].HeaderText = "Nombre";
            dgvReporte.Columns["tipoEvento"].HeaderText = "Tipo";
            dgvReporte.Columns["lugarEvento"].HeaderText = "Lugar";
            dgvReporte.Columns["fechaInicioEvento"].HeaderText = "Fecha";
            dgvReporte.Columns["cupoMaximo"].HeaderText = "Cupo";
            dgvReporte.Columns["activo"].HeaderText = "Activo";

            dgvReporte.Columns["descripcionEvento"].Visible = false;
            dgvReporte.Columns["fechaFinEvento"].Visible = false;
            dgvReporte.Columns["fechaInicioInscripcion"].Visible = false;
            dgvReporte.Columns["fechaFinInscripcion"].Visible = false;
            dgvReporte.Columns["idUsuarioCreador"].Visible = false;
        }

        private void CargarReporteAprendices(DateTime desde, DateTime hasta)
        {
            AprendizService service = new AprendizService();
            List<Aprendiz> aprendices = service.ObtenerAprendicesPorFecha(desde, hasta);

            dgvReporte.DataSource = aprendices;

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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (cmbTipoReporte.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de reporte para exportar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvReporte.Rows.Count == 0)
            {
                MessageBox.Show("Primero genera el reporte antes de exportar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("PDF exportado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar: " + ex.Message);
                }
            }
        }

        private void ExportarPDF(string rutaArchivo)
        {
            Document doc = new Document(PageSize.A4, 30, 30, 40, 40);
            PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();

            // Título
            Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Font fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            Font fuenteHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.WHITE);
            Font fuenteCelda = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            doc.Add(new Paragraph($"Reporte de {tipoActual}s", fuenteTitulo));
            doc.Add(new Paragraph(
                $"Período: {dtpDesde.Value:dd/MM/yyyy} — {dtpHasta.Value:dd/MM/yyyy}",
                fuenteSubtitulo));
            doc.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteSubtitulo));
            doc.Add(new Paragraph(" "));

            // Tabla
            int columnas = dgvReporte.Columns.Count;
            PdfPTable tabla = new PdfPTable(columnas);
            tabla.WidthPercentage = 100;

            // Encabezados
            foreach (DataGridViewColumn col in dgvReporte.Columns)
            {
                PdfPCell celda = new PdfPCell(new Phrase(col.HeaderText, fuenteHeader));
                celda.BackgroundColor = new BaseColor(46, 117, 182);
                celda.Padding = 5;
                tabla.AddCell(celda);
            }

            // Filas
            foreach (DataGridViewRow fila in dgvReporte.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                {
                    string valor = celda.Value?.ToString() ?? "";
                    // Formatear fechas
                    if (DateTime.TryParse(valor, out DateTime fecha))
                        valor = fecha.ToString("dd/MM/yyyy HH:mm");

                    tabla.AddCell(new PdfPCell(new Phrase(valor, fuenteCelda)) { Padding = 4 });
                }
            }

            doc.Add(tabla);
            doc.Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            MenuForm menu = new MenuForm(rolUsuario);
            menu.Show();
            this.Close();
        }
    }
}