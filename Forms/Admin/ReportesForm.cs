using System;
using System.Windows.Forms;
using EventPlanner.Utils;


namespace EventPlanner
{
    public partial class ReportesForm : Form
    {
        private string rolUsuario;
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            MenuForm menu = new MenuForm(rolUsuario); // Aquí puedes pasar el rol del usuario actual
            menu.Show();
            this.Close();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (cmbTipoReporte.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un tipo de reporte.");
                return;
            }

            if (dtpDesde.Value > dtpHasta.Value)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.");
                return;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (cmbTipoReporte.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un tipo de reporte para exportar.");
                return;
            }

            if (dtpDesde.Value > dtpHasta.Value)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.");
                return;
            }
        }

        
    }
}
