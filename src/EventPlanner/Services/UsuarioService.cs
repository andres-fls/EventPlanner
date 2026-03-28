using EventPlanner.Data;
using System;

namespace EventPlanner.Services
{
    public class UsuarioService
    {
        private UsuarioDAO usuarioDAO = new UsuarioDAO();

        // ==========================
        // LOGIN DEL SISTEMA
        // ==========================
        public string IniciarSesion(string usuario, string password)
        {
            // Validaciones de negocio (NO van en DAO)
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Debe ingresar el usuario.");

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Debe ingresar la contraseña.");

            string rol = usuarioDAO.ValidarLogin(usuario, password);

            if (rol == null)
                throw new Exception("Credenciales incorrectas.");

            return rol;
        }
    }
}