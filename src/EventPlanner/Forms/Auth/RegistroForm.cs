using System;
using System.Windows.Forms;
using EventPlanner.Utils;
using EventPlanner.Services;
using EventPlanner.Models;

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
            // Campos vacíos
            if (Validator.CampoVacio(txtNombre, "Nombre")) return;
            if (Validator.CampoVacio(txtID, "Cedula")) return;
            if (Validator.CampoVacio(txtUsuario, "Usuario")) return;
            if (Validator.CampoVacio(txtEdad, "Edad")) return;
            if (Validator.CampoVacio(txtCorreo, "Correo")) return;
            if (Validator.CampoVacio(txtTelefono, "Telefono")) return;
            if (Validator.CampoVacio(txtPrograma, "Programa")) return;
            if (Validator.CampoVacio(txtFicha, "Ficha")) return;
            if (Validator.CampoVacio(txtPassword, "Contraseña")) return;
            if (Validator.CampoVacio(txtConfirmarPassword, "Confirmar Contraseña")) return;

            if (cmbGenero.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un género.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarPassword.Focus();
                return;
            }

            // Tipos de datos
            if (!Validator.SoloNumeros(txtID, "Cedula")) return;
            if (!Validator.SoloLetras(txtNombre, "Nombre")) return;
            if (!Validator.SoloNumeros(txtEdad, "Edad")) return;
            if (!Validator.SoloNumeros(txtTelefono, "Telefono")) return;
            if (!Validator.SoloNumeros(txtFicha, "Ficha")) return;

            // Formatos
            if (!Validator.EmailValido(txtCorreo)) return;

            // Modelo de negocio
            if (!Validator.EdadValida(txtEdad)) return;
            if (!Validator.PasswordValida(txtPassword)) return;
            if (!Validator.LongitudExacta(txtTelefono, 10, "Telefono")) return;

            try
            {
                // 1. Crear el usuario
                Usuario usuario = new Usuario()
                {
                    nombreUsuario = txtUsuario.Text.Trim(),
                    passwordUsuario = txtPassword.Text,
                    rolUsuario = "Aprendiz"
                };

                UsuarioService usuarioService = new UsuarioService();
                int idUsuarioCreado = usuarioService.RegistrarUsuario(usuario);

                // 2. Crear el aprendiz vinculado al usuario
                Aprendiz aprendiz = new Aprendiz()
                {
                    cedulaAprendiz = txtID.Text.Trim(),
                    nombreAprendiz = txtNombre.Text.Trim(),
                    edadAprendiz = int.Parse(txtEdad.Text),
                    generoAprendiz = cmbGenero.SelectedItem.ToString(),
                    correoAprendiz = txtCorreo.Text.Trim(),
                    telefonoAprendiz = txtTelefono.Text.Trim(),
                    codigoFicha = int.Parse(txtFicha.Text),
                    idUsuario = idUsuarioCreado
                };

                AprendizService aprendizService = new AprendizService();
                aprendizService.CrearAprendiz(aprendiz);

                MessageBox.Show("Registro exitoso. Ya puedes iniciar sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  
    }
}
