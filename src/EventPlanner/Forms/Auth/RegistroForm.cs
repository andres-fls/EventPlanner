// ============================================================
// Archivo: RegistroForm.cs
// Propósito: Formulario de registro de nuevos aprendices.
// Permite ingresar datos personales (nombre, cédula, edad, género,
// correo, teléfono), información académica (programa, ficha) y
// credenciales de acceso (usuario, contraseña).
// Valida todos los campos (vacíos, tipos de datos, formatos, reglas de negocio)
// y guarda tanto el usuario como el aprendiz en la base de datos.
// ============================================================

using System;
using System.Windows.Forms;
using EventPlanner.Utils;
using EventPlanner.Services;
using EventPlanner.Models;

namespace EventPlanner
{
    public partial class RegistroForm : Form
    {
        // Constructor: inicializa componentes y aplica tamaño desde configuración
        public RegistroForm()
        {
            InitializeComponent();
            this.Size = AppConfig.TamanoVentana;
        }

        // Al cargar el formulario, asigna los manejadores de validación de teclado
        private void RegistroForm_Load(object sender, EventArgs e)
        {
            // Solo permite letras en el campo Nombre
            txtNombre.KeyPress += Validator.SoloLetrasKeyPress;
            // Solo números en cédula, edad, teléfono y ficha
            txtID.KeyPress += Validator.SoloNumerosKeyPress;
            txtEdad.KeyPress += Validator.SoloNumerosKeyPress;
            txtTelefono.KeyPress += Validator.SoloNumerosKeyPress;
            txtFicha.KeyPress += Validator.SoloNumerosKeyPress;
            // Restringe caracteres especiales para el nombre de usuario
            txtUsuario.KeyPress += Validator.UsuarioKeyPress;
        }

        // Botón Cancelar: cierra el formulario sin guardar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Botón Registrar: valida todos los campos y, si es correcto, crea usuario y aprendiz
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // ==========================================
            // VALIDACIONES DE CAMPOS VACÍOS
            // ==========================================
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

            // Validación de género seleccionado en ComboBox
            if (cmbGenero.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un género.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación de coincidencia de contraseñas
            if (txtPassword.Text != txtConfirmarPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarPassword.Focus();
                return;
            }

            // ==========================================
            // VALIDACIONES DE TIPO DE DATO (solo números, solo letras)
            // ==========================================
            if (!Validator.SoloNumeros(txtID, "Cedula")) return;
            if (!Validator.SoloLetras(txtNombre, "Nombre")) return;
            if (!Validator.SoloNumeros(txtEdad, "Edad")) return;
            if (!Validator.SoloNumeros(txtTelefono, "Telefono")) return;
            if (!Validator.SoloNumeros(txtFicha, "Ficha")) return;

            // ==========================================
            // VALIDACIONES DE FORMATO
            // ==========================================
            if (!Validator.EmailValido(txtCorreo)) return;  // Formato de correo electrónico

            // ==========================================
            // REGLAS DE NEGOCIO
            // ==========================================
            if (!Validator.EdadValida(txtEdad)) return;     // Edad entre 15 y 99 años (ejemplo)
            if (!Validator.PasswordValida(txtPassword)) return; // Contraseña con requisitos de seguridad
            if (!Validator.LongitudExacta(txtTelefono, 10, "Telefono")) return; // Teléfono de 10 dígitos

            try
            {
                // ==========================================
                // 1. CREAR USUARIO (credenciales de acceso)
                // ==========================================
                Usuario usuario = new Usuario()
                {
                    nombreUsuario = txtUsuario.Text.Trim(),
                    passwordUsuario = txtPassword.Text,   // En producción debería ir encriptada
                    rolUsuario = "Aprendiz"                // Por defecto, todos los registros son aprendices
                };

                UsuarioService usuarioService = new UsuarioService();
                int idUsuarioCreado = usuarioService.RegistrarUsuario(usuario); // Inserta y retorna el ID generado

                // ==========================================
                // 2. CREAR APRENDIZ VINCULADO AL USUARIO
                // ==========================================
                Aprendiz aprendiz = new Aprendiz()
                {
                    cedulaAprendiz = txtID.Text.Trim(),
                    nombreAprendiz = txtNombre.Text.Trim(),
                    edadAprendiz = int.Parse(txtEdad.Text),
                    generoAprendiz = cmbGenero.SelectedItem.ToString(),
                    correoAprendiz = txtCorreo.Text.Trim(),
                    telefonoAprendiz = txtTelefono.Text.Trim(),
                    codigoFicha = int.Parse(txtFicha.Text),
                    idUsuario = idUsuarioCreado            // Relación con el usuario recién creado
                };

                AprendizService aprendizService = new AprendizService();
                aprendizService.CrearAprendiz(aprendiz);

                // Mensaje de éxito y cierre del formulario
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