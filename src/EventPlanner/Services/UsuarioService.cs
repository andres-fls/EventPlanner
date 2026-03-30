using EventPlanner.DAO;
using EventPlanner.Models;
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

        public int RegistrarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.nombreUsuario))
                throw new Exception("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.passwordUsuario))
                throw new Exception("La contraseña es obligatoria.");

            return usuarioDAO.CrearUsuario(usuario);
        }

    }
}