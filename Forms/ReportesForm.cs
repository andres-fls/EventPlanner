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
     

        private void btnVolver_Click(object sender, EventArgs e)
        {
            MenuForm menu = new MenuForm(rolUsuario); // Aquí puedes pasar el rol del usuario actual
            menu.Show();
            this.Close();
        }
    }
}
