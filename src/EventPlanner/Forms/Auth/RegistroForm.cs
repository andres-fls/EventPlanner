// ============================================================
// Archivo: RegistroForm.cs
// Propósito: Formulario de registro de nuevos aprendices.
// Permite ingresar datos personales (nombre, cédula, edad, género,
// correo, teléfono), información académica (programa, ficha) y
// credenciales de acceso (usuario, contraseña).
// Valida todos los campos (vacíos, tipos de datos, formatos, reglas de negocio)
// y guarda tanto el usuario como el aprendiz en la base de datos.
// ============================================================

using System; // Importa base de .NET
using System.Windows.Forms; // Importa Windows Forms
using EventPlanner.Utils; // Importa utilidades
using EventPlanner.Services; // Importa servicios
using EventPlanner.Models; // Importa modelos

namespace EventPlanner // Espacio de nombres de la aplicación
{
    public partial class RegistroForm : Form // Clase del formulario de registro
    {
        // Constructor: inicializa componentes y aplica tamaño desde configuración
        public RegistroForm() // Constructor público
        {
            InitializeComponent(); // Inicializa componentes
            this.Size = AppConfig.TamanoVentana; // Aplica tamaño desde config
        }

        // Al cargar el formulario, asigna los manejadores de validación de teclado
        private void RegistroForm_Load(object sender, EventArgs e) // Evento Load
        {
            // Solo permite letras en el campo Nombre
            txtNombre.KeyPress += Validator.SoloLetrasKeyPress; // Agrega validación letras
            // Solo números en cédula, edad, teléfono y ficha
            txtID.KeyPress += Validator.SoloNumerosKeyPress; // Agrega validación números ID
            txtEdad.KeyPress += Validator.SoloNumerosKeyPress; // Agrega validación números edad
            txtTelefono.KeyPress += Validator.SoloNumerosKeyPress; // Agrega validación números teléfono
            txtFicha.KeyPress += Validator.SoloNumerosKeyPress; // Agrega validación números ficha
            // Restringe caracteres especiales para el nombre de usuario
            txtUsuario.KeyPress += Validator.UsuarioKeyPress; // Agrega validación usuario
        }

        // Botón Cancelar: cierra el formulario sin guardar
        private void btnCancelar_Click(object sender, EventArgs e) // Evento Click cancelar
        {
            this.Close(); // Cierra formulario
        }

        // Botón Registrar: valida todos los campos y, si es correcto, crea usuario y aprendiz
        private void btnRegistrar_Click(object sender, EventArgs e) // Evento Click registrar
        {
            // ==========================================
            // VALIDACIONES DE CAMPOS VACÍOS
            // ==========================================
            if (Validator.CampoVacio(txtNombre, "Nombre")) return; // Valida nombre no vacío
            if (Validator.CampoVacio(txtID, "Cedula")) return; // Valida cédula no vacía
            if (Validator.CampoVacio(txtUsuario, "Usuario")) return; // Valida usuario no vacío
            if (Validator.CampoVacio(txtEdad, "Edad")) return; // Valida edad no vacía
            if (Validator.CampoVacio(txtCorreo, "Correo")) return; // Valida correo no vacío
            if (Validator.CampoVacio(txtTelefono, "Telefono")) return; // Valida teléfono no vacío
            if (Validator.CampoVacio(txtPrograma, "Programa")) return; // Valida programa no vacío
            if (Validator.CampoVacio(txtFicha, "Ficha")) return; // Valida ficha no vacía
            if (Validator.CampoVacio(txtPassword, "Contraseña")) return; // Valida contraseña no vacía
            if (Validator.CampoVacio(txtConfirmarPassword, "Confirmar Contraseña")) return; // Valida confirmación no vacía

            // Validación de género seleccionado en ComboBox
            if (cmbGenero.SelectedIndex == -1) // Verifica selección género
            {
                MessageBox.Show("Seleccione un género.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Muestra error
                return; // Sale
            }

            // Validación de coincidencia de contraseñas
            if (txtPassword.Text != txtConfirmarPassword.Text) // Verifica coincidencia contraseñas
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Muestra error
                txtConfirmarPassword.Focus(); // Enfoca confirmación
                return; // Sale
            }

            // ==========================================
            // VALIDACIONES DE TIPO DE DATO (solo números, solo letras)
            // ==========================================
            if (!Validator.SoloNumeros(txtID, "Cedula")) return; // Valida cédula números
            if (!Validator.SoloLetras(txtNombre, "Nombre")) return; // Valida nombre letras
            if (!Validator.SoloNumeros(txtEdad, "Edad")) return; // Valida edad números
            if (!Validator.SoloNumeros(txtTelefono, "Telefono")) return; // Valida teléfono números
            if (!Validator.SoloNumeros(txtFicha, "Ficha")) return; // Valida ficha números

            // ==========================================
            // VALIDACIONES DE FORMATO
            // ==========================================
            if (!Validator.EmailValido(txtCorreo)) return;  // Valida formato email

            // ==========================================
            // REGLAS DE NEGOCIO
            // ==========================================
            if (!Validator.EdadValida(txtEdad)) return;     // Valida edad rango
            if (!Validator.PasswordValida(txtPassword)) return; // Valida contraseña segura
            if (!Validator.LongitudExacta(txtTelefono, 10, "Telefono")) return; // Valida longitud teléfono

            try // Bloque try
            {
                // ==========================================
                // 1. CREAR USUARIO (credenciales de acceso)
                // ==========================================
                Usuario usuario = new Usuario() // Instancia usuario
                {
                    nombreUsuario = txtUsuario.Text.Trim(), // Asigna nombre usuario
                    passwordUsuario = txtPassword.Text,   // Asigna contraseña
                    rolUsuario = "Aprendiz"                // Asigna rol aprendiz
                };

                UsuarioService usuarioService = new UsuarioService(); // Instancia servicio usuario
                int idUsuarioCreado = usuarioService.RegistrarUsuario(usuario); // Registra usuario

                // ==========================================
                // 2. CREAR APRENDIZ VINCULADO AL USUARIO
                // ==========================================
                Aprendiz aprendiz = new Aprendiz() // Instancia aprendiz
                {
                    cedulaAprendiz = txtID.Text.Trim(), // Asigna cédula
                    nombreAprendiz = txtNombre.Text.Trim(), // Asigna nombre
                    edadAprendiz = int.Parse(txtEdad.Text), // Asigna edad
                    generoAprendiz = cmbGenero.SelectedItem.ToString(), // Asigna género
                    correoAprendiz = txtCorreo.Text.Trim(), // Asigna correo
                    telefonoAprendiz = txtTelefono.Text.Trim(), // Asigna teléfono
                    codigoFicha = int.Parse(txtFicha.Text), // Asigna ficha
                    idUsuario = idUsuarioCreado            // Asigna ID usuario
                };

                AprendizService aprendizService = new AprendizService(); // Instancia servicio aprendiz
                aprendizService.CrearAprendiz(aprendiz); // Crea aprendiz

                // Mensaje de éxito y cierre del formulario
                MessageBox.Show("Registro exitoso. Ya puedes iniciar sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); // Muestra éxito
                this.Close(); // Cierra formulario
            }
            catch (Exception ex) // Captura excepciones
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Muestra error
            }
        }
    }
}