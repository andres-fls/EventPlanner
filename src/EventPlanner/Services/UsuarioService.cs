// ============================================================
// Archivo: UsuarioService.cs
// ============================================================

using EventPlanner.DAO;
using EventPlanner.Models;
using EventPlanner.Utils;
using System;

namespace EventPlanner.Services
{
    public class UsuarioService
    {
        private UsuarioDAO usuarioDAO = new UsuarioDAO();
        private AprendizService aprendizService = new AprendizService(); // ← era AprendizDAO

        public string IniciarSesion(string usuario, string password)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Debe ingresar el usuario.");

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Debe ingresar la contraseña.");

            Session.LimpiarSesion();

            Usuario user = usuarioDAO.ObtenerUsuarioLogin(usuario, password);

            if (user == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            Session.IdUsuario = user.idUsuario;
            Session.Rol = user.rolUsuario;

            if (user.rolUsuario == "Aprendiz")
            {
                
                Aprendiz aprendiz = aprendizService.BuscarPorIdUsuario(user.idUsuario);

                if (aprendiz == null)
                    throw new Exception("El usuario no tiene perfil de aprendiz asociado.");

                Session.IdAprendiz = aprendiz.idAprendiz;
            }
            else
            {
                Session.IdAprendiz = 0;
            }

            return user.rolUsuario;
        }

        public int RegistrarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("El usuario no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(usuario.nombreUsuario))
                throw new Exception("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.passwordUsuario))
                throw new Exception("La contraseña es obligatoria.");

            if (string.IsNullOrWhiteSpace(usuario.rolUsuario))
                throw new Exception("Debe especificar un rol.");

            Usuario existente = usuarioDAO.ObtenerPorNombre(usuario.nombreUsuario);

            if (existente != null)
                throw new Exception("El usuario ya existe.");

            return usuarioDAO.CrearUsuario(usuario);
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new Exception("ID de usuario inválido.");

            return usuarioDAO.ObtenerUsuarioPorId(idUsuario);
        }
    }
}