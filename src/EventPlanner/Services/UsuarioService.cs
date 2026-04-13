using EventPlanner.DAO;
using EventPlanner.Models;
using EventPlanner.Utils;
using System;

namespace EventPlanner.Services
{
    public class UsuarioService
    {
        private UsuarioDAO usuarioDAO = new UsuarioDAO();
        private AprendizDAO aprendizDAO = new AprendizDAO();

        // =====================================================
        // INICIAR SESIÓN
        // =====================================================
        public string IniciarSesion(string usuario, string password)
        {
            // ==========================
            // VALIDACIONES
            // ==========================
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Debe ingresar el usuario.");

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Debe ingresar la contraseña.");

            // ==========================
            // LIMPIAR SESIÓN PREVIA
            // ==========================
            Session.IdUsuario = 0;
            Session.IdAprendiz = 0;
            Session.Rol = null;

            // ==========================
            // VALIDAR LOGIN
            // ==========================
            Usuario user = usuarioDAO.ObtenerUsuarioLogin(usuario, password);

            if (user == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            // ==========================
            // CONFIGURAR SESIÓN
            // ==========================
            Session.IdUsuario = user.idUsuario;
            Session.Rol = user.rolUsuario;

            // ==========================
            // SI ES APRENDIZ → CARGAR PERFIL
            // ==========================
            if (user.rolUsuario == "Aprendiz")
            {
                Aprendiz aprendiz = aprendizDAO.BuscarPorIdUsuario(user.idUsuario);

                if (aprendiz == null)
                    throw new Exception("El usuario no tiene perfil de aprendiz asociado.");

                Session.IdAprendiz = aprendiz.idAprendiz;
            }
            else
            {
                // Asegurar consistencia
                Session.IdAprendiz = 0;
            }

            return user.rolUsuario;
        }

        // =====================================================
        // REGISTRAR USUARIO
        // =====================================================
        public int RegistrarUsuario(Usuario usuario)
        {
            // ==========================
            // VALIDACIONES
            // ==========================
            if (usuario == null)
                throw new Exception("El usuario no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(usuario.nombreUsuario))
                throw new Exception("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.passwordUsuario))
                throw new Exception("La contraseña es obligatoria.");

            if (string.IsNullOrWhiteSpace(usuario.rolUsuario))
                throw new Exception("Debe especificar un rol.");

            // ==========================
            // VALIDAR EXISTENCIA (CORRECTO)
            // ==========================
            Usuario existente = usuarioDAO.ObtenerPorNombre(usuario.nombreUsuario);

            if (existente != null)
                throw new Exception("El usuario ya existe.");

            // ==========================
            // CREAR USUARIO
            // ==========================
            return usuarioDAO.CrearUsuario(usuario);
        }

        // =====================================================
        // OBTENER USUARIO POR ID
        // =====================================================
        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new Exception("ID de usuario inválido.");

            return usuarioDAO.ObtenerUsuarioPorId(idUsuario);
        }
    }
}