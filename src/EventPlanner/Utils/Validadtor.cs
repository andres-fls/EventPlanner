using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EventPlanner.Utils
{
    public static class Validator
    {
        // ===============================
        // VALIDAR CAMPO VACÍO
        // ===============================
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

        // ===============================
        // SOLO LETRAS
        // ===============================
        public static bool SoloLetras(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim();

            if (!Regex.IsMatch(txt.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
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

        // ===============================
        // SOLO NÚMEROS
        // ===============================
        public static bool SoloNumeros(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim();

            if (!Regex.IsMatch(txt.Text, @"^[0-9]+$"))
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
        public static bool EdadValida(TextBox txt)
        {
            if (!int.TryParse(txt.Text, out int edad) || edad < 10 || edad > 100)
            {
                MessageBox.Show("Ingrese una edad válida (10 - 100).",
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
        public static bool EmailValido(TextBox txt)
        {
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
        public static void SoloLetrasKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten letras.");
            }
        }

        // ===============================
        // SOLO NÚMEROS EN TIEMPO REAL
        // ===============================
        public static void SoloNumerosKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten números.");
            }
        }

        public static void UsuarioKeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;

            if (!(char.IsLetterOrDigit(c) ||
                  c == '@' || c == '.' || c == '_' ||
                  char.IsControl(c)))
            {
                e.Handled = true;
            }
        }
    }
}