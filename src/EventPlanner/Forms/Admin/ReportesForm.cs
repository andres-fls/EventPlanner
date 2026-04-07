// ============================================================
// Archivo: ReportesForm.cs
// Propósito: Formulario para generar y exportar reportes en PDF.
// Permite seleccionar el tipo de reporte (Eventos, Aprendices o Ambos)
// y un rango de fechas. Muestra los resultados en un DataGridView
// y ofrece la opción de exportarlos a un archivo PDF utilizando
// la biblioteca iTextSharp.
// ============================================================

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
        // Rol del usuario actual (para control de acceso, aunque no se usa directamente aquí)
        private string rolUsuario;
        // Almacena el tipo de reporte seleccionado ("Evento", "Aprendiz" o "Ambos")
        private string tipoActual = "";

        // Constructor: recibe el rol del usuario
        public ReportesForm(string rol)
        {
            InitializeComponent();
            rolUsuario = rol;
        }

        // Al cargar el formulario, llena el ComboBox con las opciones de reporte
        private void ReportesForm_Load(object sender, EventArgs e)
        {
            cmbTipoReporte.Items.Clear();
            cmbTipoReporte.Items.Add("Evento");
            cmbTipoReporte.Items.Add("Aprendiz");
            cmbTipoReporte.Items.Add("Ambos");
            cmbTipoReporte.SelectedIndex = -1; // Sin selección inicial
        }

        // Botón "Generar Reporte": valida fechas y tipo, luego carga los datos en el DataGridView
        private void btnReporte_Click(object sender, EventArgs e)
        {
            // Validación: tipo de reporte seleccionado
            if (cmbTipoReporte.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de reporte.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación: fecha desde no mayor que fecha hasta
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guarda el tipo seleccionado y ajusta la fecha hasta para incluir todo el día
            tipoActual = cmbTipoReporte.SelectedItem.ToString();
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddHours(23).AddMinutes(59);

            try
            {
                // Limpia el DataGridView antes de cargar nuevos datos
                dgvReporte.DataSource = null;
                dgvReporte.Columns.Clear();

                // Carga según el tipo
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
                    // Para "Ambos" se carga el mismo reporte de eventos
                    // (podría modificarse para mostrar ambos, pero por diseño actual solo eventos)
                    CargarReporteEventos(desde, hasta);
                }

                // Si no hay filas, informa al usuario
                if (dgvReporte.Rows.Count == 0)
                    MessageBox.Show("No se encontraron registros en el rango de fechas seleccionado.", "Sin resultados",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message);
            }
        }

        // Carga el reporte de eventos en el DataGridView
        private void CargarReporteEventos(DateTime desde, DateTime hasta)
        {
            EventoService service = new EventoService();
            List<Evento> eventos = service.ObtenerEventosPorFecha(desde, hasta);

            dgvReporte.DataSource = eventos; // Enlaza la lista al grid

            // Configuración de columnas visibles y sus encabezados
            dgvReporte.Columns["idEvento"].HeaderText = "ID";
            dgvReporte.Columns["nombreEvento"].HeaderText = "Nombre";
            dgvReporte.Columns["tipoEvento"].HeaderText = "Tipo";
            dgvReporte.Columns["lugarEvento"].HeaderText = "Lugar";
            dgvReporte.Columns["fechaInicioEvento"].HeaderText = "Fecha";
            dgvReporte.Columns["cupoMaximo"].HeaderText = "Cupo";
            dgvReporte.Columns["activo"].HeaderText = "Activo";

            // Oculta columnas que no son relevantes para el reporte
            dgvReporte.Columns["descripcionEvento"].Visible = false;
            dgvReporte.Columns["fechaFinEvento"].Visible = false;
            dgvReporte.Columns["fechaInicioInscripcion"].Visible = false;
            dgvReporte.Columns["fechaFinInscripcion"].Visible = false;
            dgvReporte.Columns["idUsuarioCreador"].Visible = false;
        }

        // Carga el reporte de aprendices en el DataGridView
        private void CargarReporteAprendices(DateTime desde, DateTime hasta)
        {
            AprendizService service = new AprendizService();
            List<Aprendiz> aprendices = service.ObtenerAprendicesPorFecha(desde, hasta);

            dgvReporte.DataSource = aprendices;

            // Muestra solo las columnas más relevantes
            dgvReporte.Columns["idAprendiz"].HeaderText = "ID";
            dgvReporte.Columns["cedulaAprendiz"].HeaderText = "Cédula";
            dgvReporte.Columns["nombreAprendiz"].HeaderText = "Nombre";
            dgvReporte.Columns["correoAprendiz"].HeaderText = "Correo";
            dgvReporte.Columns["telefonoAprendiz"].HeaderText = "Teléfono";

            // Oculta columnas internas o menos útiles
            dgvReporte.Columns["edadAprendiz"].Visible = false;
            dgvReporte.Columns["generoAprendiz"].Visible = false;
            dgvReporte.Columns["codigoFicha"].Visible = false;
            dgvReporte.Columns["idUsuario"].Visible = false;
        }

        // Botón "Exportar": guarda el contenido del DataGridView en un archivo PDF
        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Validaciones similares a las del reporte
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

            // Diálogo para elegir la ubicación y nombre del archivo PDF
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

        // Genera el archivo PDF a partir del DataGridView actual
        private void ExportarPDF(string rutaArchivo)
        {
            // Crea un documento PDF tamaño A4 con márgenes
            Document doc = new Document(PageSize.A4, 30, 30, 40, 40);
            PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();

            // Definición de fuentes
            Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Font fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            Font fuenteHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.WHITE);
            Font fuenteCelda = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            // Título y encabezado del reporte
            doc.Add(new Paragraph($"Reporte de {tipoActual}s", fuenteTitulo));
            doc.Add(new Paragraph(
                $"Período: {dtpDesde.Value:dd/MM/yyyy} — {dtpHasta.Value:dd/MM/yyyy}",
                fuenteSubtitulo));
            doc.Add(new Paragraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}", fuenteSubtitulo));
            doc.Add(new Paragraph(" "));

            // Crear tabla PDF con el número de columnas del DataGridView
            int columnas = dgvReporte.Columns.Count;
            PdfPTable tabla = new PdfPTable(columnas);
            tabla.WidthPercentage = 100;

            // Encabezados de la tabla (se toman los HeaderText de las columnas)
            foreach (DataGridViewColumn col in dgvReporte.Columns)
            {
                PdfPCell celda = new PdfPCell(new Phrase(col.HeaderText, fuenteHeader));
                celda.BackgroundColor = new BaseColor(46, 117, 182); // Color de fondo azul
                celda.Padding = 5;
                tabla.AddCell(celda);
            }

            // Recorrer todas las filas del DataGridView
            foreach (DataGridViewRow fila in dgvReporte.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                {
                    string valor = celda.Value?.ToString() ?? "";
                    // Si el valor es una fecha, la formatea como dd/MM/yyyy HH:mm
                    if (DateTime.TryParse(valor, out DateTime fecha))
                        valor = fecha.ToString("dd/MM/yyyy HH:mm");

                    tabla.AddCell(new PdfPCell(new Phrase(valor, fuenteCelda)) { Padding = 4 });
                }
            }

            doc.Add(tabla);
            doc.Close();
        }

        // Botón "Volver": regresa al menú principal
        private void btnVolver_Click(object sender, EventArgs e)
        {
            MenuForm menu = new MenuForm(rolUsuario);
            menu.Show();
            this.Close();
        }
    }
}