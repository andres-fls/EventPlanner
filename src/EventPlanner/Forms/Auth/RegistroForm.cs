using System;
using System.Windows.Forms;
using EventPlanner.Utils;
using EventPlanner.Services;
using EventPlanner.Models;

namespace EventPlanner
{
    public partial class RegistroForm : Form
    {
        private UsuarioService usuarioService = new UsuarioService();
        private AprendizService aprendizService = new AprendizService();

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
            // ==========================
            // VALIDACIONES BÁSICAS (UI)
            // ==========================
            if (Validator.CampoVacio(txtNombre, "Nombre")) return;
            if (Validator.CampoVacio(txtID, "Cedula")) return;
            if (Validator.CampoVacio(txtUsuario, "Usuario")) return;
            if (Validator.CampoVacio(txtEdad, "Edad")) return;
            if (Validator.CampoVacio(txtCorreo, "Correo")) return;
            if (Validator.CampoVacio(txtTelefono, "Telefono")) return;
            if (Validator.CampoVacio(txtFicha, "Ficha")) return;
            if (Validator.CampoVacio(txtPassword, "Contraseña")) return;
            if (Validator.CampoVacio(txtConfirmarPassword, "Confirmar Contraseña")) return;

            if (cmbGenero.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un género.");
                return;
            }

            if (!Validator.EmailValido(txtCorreo)) return;

            // ❗ SOLO esta validación se puede quedar en UI
            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                // ==========================
                // CREAR USUARIO
                // ==========================
                Usuario usuario = new Usuario()
                {
                    nombreUsuario = txtUsuario.Text.Trim(),
                    passwordUsuario = txtPassword.Text,
                    rolUsuario = "Aprendiz"
                };

                int idUsuario = usuarioService.RegistrarUsuario(usuario);

                // ==========================
                // CREAR APRENDIZ
                // ==========================
                Aprendiz aprendiz = new Aprendiz()
                {
                    cedulaAprendiz = txtID.Text.Trim(),
                    nombreAprendiz = txtNombre.Text.Trim(),
                    edadAprendiz = int.Parse(txtEdad.Text),
                    generoAprendiz = cmbGenero.SelectedItem.ToString(),
                    correoAprendiz = txtCorreo.Text.Trim(),
                    telefonoAprendiz = txtTelefono.Text.Trim(),
                    codigoFicha = int.Parse(txtFicha.Text),
                    idUsuario = idUsuario
                };

                aprendizService.CrearAprendiz(aprendiz);

                MessageBox.Show("Registro exitoso.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}