// ============================================================
// Archivo: UsuarioService.cs
// Propósito: Capa de servicio para la lógica de negocio
//            relacionada con los usuarios.
// Contiene métodos para iniciar sesión (validando reglas de negocio)
//            y registrar nuevos usuarios.
// Actúa como intermediario entre los formularios y el DAO,
//            aplicando validaciones antes de llamar a la capa de datos.
// ============================================================

using EventPlanner.DAO;
using EventPlanner.Models;
using System;

namespace EventPlanner.Services
{
    public class UsuarioService
    {
        // Instancia del DAO para acceder a los datos de usuario
        private UsuarioDAO usuarioDAO = new UsuarioDAO();

        // ==========================
        // LOGIN DEL SISTEMA
        // ==========================
        // Valida las credenciales de un usuario y retorna su rol.
        // Parámetros:
        //   usuario - Nombre de usuario ingresado
        //   password - Contraseña ingresada
        // Retorna: El rol del usuario ("Aprendiz", "Admin", etc.)
        // Lanza excepción si los campos están vacíos o las credenciales son incorrectas.
        public string IniciarSesion(string usuario, string password)
        {
            // Validaciones de negocio (NO van en DAO para mantener separación de responsabilidades)
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Debe ingresar el usuario.");

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Debe ingresar la contraseña.");

            // Llama al DAO para validar contra la base de datos
            string rol = usuarioDAO.ValidarLogin(usuario, password);

            // Si el DAO retorna null, significa que las credenciales no coinciden
            if (rol == null)
                throw new Exception("Credenciales incorrectas.");

            return rol;
        }

        // Registra un nuevo usuario en el sistema.
        // Parámetro: usuario - Objeto Usuario con los datos a registrar
        // Retorna: El ID autogenerado del nuevo usuario
        // Lanza excepción si el nombre de usuario o contraseña están vacíos.
        public int RegistrarUsuario(Usuario usuario)
        {
            // Validación de negocio: nombre de usuario no vacío
            if (string.IsNullOrWhiteSpace(usuario.nombreUsuario))
                throw new Exception("El nombre de usuario es obligatorio.");

            // Validación de negocio: contraseña no vacía
            if (string.IsNullOrWhiteSpace(usuario.passwordUsuario))
                throw new Exception("La contraseña es obligatoria.");

            // Llama al DAO para insertar el usuario en la base de datos
            return usuarioDAO.CrearUsuario(usuario);
        }
    }
}