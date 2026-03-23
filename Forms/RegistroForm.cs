using System;
using System.Windows.Forms;
using EventPlanner.Utils;


namespace EventPlanner
{
    public partial class RegistroForm : Form
    {
        public RegistroForm()
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
        }

        private void RegistroForm_Load(object sender, EventArgs e)
        {
            txtNombre.KeyPress += Validator.SoloLetrasKeyPress;
            txtID.KeyPress += Validator.SoloNumerosKeyPress;
            txtEdad.KeyPress += Validator.SoloNumerosKeyPress;
            txtTelefono.KeyPress += Validator.SoloNumerosKeyPress;
            txtFicha.KeyPress += Validator.SoloNumerosKeyPress;
            txtUsuario.KeyPress += Validator.UsuarioKeyPress;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //campos vacios
            if (Validator.CampoVacio(txtNombre, "Nombre")) return;
            if (Validator.CampoVacio(txtID, "Cedula" )) return;
            if (Validator.CampoVacio(txtUsuario, "Usuario")) return;
            if (Validator.CampoVacio(txtEdad, "Edad")) return;
            if (Validator.CampoVacio(txtCorreo, "Correo")) return;
            if (Validator.CampoVacio(txtTelefono, "Telefono")) return;
            if (Validator.CampoVacio(txtPrograma, "Programa")) return;
            if (Validator.CampoVacio(txtFicha, "Ficha")) return;
            if (Validator.CampoVacio(txtPassword, "Contraseña")) return;
            if (Validator.CampoVacio(txtConfirmarPassword, "Confirmar Contraseña")) return;

            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarPassword.Focus();
                return;
            }

            //tipos de datos
            if (!Validator.SoloNumeros(txtID, "Cedula")) return;
            if (!Validator.SoloLetras(txtNombre, "Nombre")) return;
            if (!Validator.SoloNumeros(txtEdad, "Edad")) return;
            if (!Validator.SoloNumeros(txtTelefono, "Telefono")) return;
            if (!Validator.SoloNumeros(txtFicha, "Ficha")) return;

            //formatos
            if (!Validator.EmailValido(txtCorreo)) return;

            //modelo de negocio
            if (!Validator.EdadValida(txtEdad)) return;
            if (!Validator.PasswordValida(txtPassword)) return;
            if (!Validator.LongitudExacta(txtTelefono, 10, "Telefono")) return;

            //registro exitoso

            MessageBox.Show("Registro exitoso.");

            this.Close();
        }

        
    }
}
