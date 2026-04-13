// ============================================================
// Archivo: Validator.cs
// Propósito: Proporciona métodos de validación estáticos para
//            ser utilizados en toda la aplicación.
// Incluye validaciones de campos vacíos, formatos (solo letras,
// solo números, email), reglas de negocio (edad, contraseña,
// longitud exacta), y validaciones en tiempo real mediante eventos
// KeyPress para restringir la entrada del usuario.
// ============================================================

using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EventPlanner.Utils
{
    public static class Validator
    {
        // ===============================
        // VALIDAR CAMPO VACÍO
        // ===============================
        // Verifica si un TextBox está vacío o contiene solo espacios en blanco.
        // Si está vacío, muestra un mensaje de advertencia y enfoca el control.
        // Retorna: true si el campo está vacío, false si contiene texto.
        public static bool CampoVacio(TextBox txt, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                MessageBox.Show($"El campo {nombreCampo} es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txt.Focus();
                return true;
            }

            return false;
        }

        public static bool SoloLetras(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim(); // ← ahora sí se usa abajo

            if (!Regex.IsMatch(valor, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txt.Focus();
                return false;
            }

            return true;
        }

        public static bool SoloNumeros(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim(); // ← ahora sí se usa abajo

            if (!Regex.IsMatch(valor, @"^[0-9]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener números.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txt.Focus();
                return false;
            }

            return true;
        }

        // ===============================
        // EDAD VÁLIDA
        // ===============================
        // Verifica que la edad esté en el rango de 10 a 100 años.
        // Retorna: true si la edad es válida, false en caso contrario.
        public static bool EdadValida(TextBox txt)
        {
            if (!int.TryParse(txt.Text, out int edad) || edad < 14|| edad > 100)
            {
                MessageBox.Show("Ingrese una edad válida (14 - 100).",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txt.Focus();
                return false;
            }

            return true;
        }

        // ===============================
        // EMAIL VÁLIDO
        // ===============================
        // Valida el formato de correo electrónico usando una expresión regular básica.
        // Retorna: true si el formato es válido, false en caso contrario.
        public static bool EmailValido(TextBox txt)
        {
            // Expresión regular simple: texto@texto.texto
            if (!Regex.IsMatch(txt.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingrese un correo válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txt.Focus();
                return false;
            }

            return true;
        }

        // ===============================
        // CONTRASEÑA SEGURA
        // ===============================
        // Verifica que la contraseña tenga al menos 6 caracteres.
        // Retorna: true si la longitud es suficiente, false en caso contrario.
        public static bool PasswordValida(TextBox txt)
        {
            if (txt.Text.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener mínimo 6 caracteres.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txt.Focus();
                return false;
            }

            return true;
        }

        // ===============================
        // LONGITUD EXACTA NUMÉRICA
        // ===============================
        // Verifica que el texto tenga exactamente la longitud especificada (útil para teléfonos, cédulas, etc.).
        // Retorna: true si la longitud coincide, false en caso contrario.
        public static bool LongitudExacta(TextBox txt, int longitud, string nombreCampo)
        {
            if (txt.Text.Length != longitud)
            {
                MessageBox.Show($"{nombreCampo} debe tener {longitud} dígitos.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txt.Focus();
                return false;
            }

            return true;
        }

        // ===============================
        // SOLO LETRAS EN TIEMPO REAL
        // ===============================
        // Evento KeyPress para restringir la entrada a solo letras, espacios y teclas de control.
        // Se asigna a un TextBox para filtrar caracteres mientras el usuario escribe.
        public static void SoloLetrasKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Bloquea el carácter
                MessageBox.Show("Solo se permiten letras.");
            }
        }

        // ===============================
        // SOLO NÚMEROS EN TIEMPO REAL
        // ===============================
        // Evento KeyPress para restringir la entrada a solo dígitos numéricos y teclas de control.
        public static void SoloNumerosKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten números.");
            }
        }

        // ===============================
        // USUARIO EN TIEMPO REAL
        // ===============================
        // Evento KeyPress para permitir caracteres típicos en nombres de usuario:
        // letras, números, @, ., _, y teclas de control.
        public static void UsuarioKeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;

            if (!(char.IsLetterOrDigit(c) ||
                  c == '@' || c == '.' || c == '_' ||
                  char.IsControl(c)))
            {
                e.Handled = true; // Bloquea caracteres no permitidos
            }
        }
    }
}