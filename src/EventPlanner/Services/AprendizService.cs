using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class AprendizService
    {
        private AprendizDAO aprendizDAO = new AprendizDAO();

        // ==========================
        // CREAR APRENDIZ
        // ==========================
        public bool CrearAprendiz(Aprendiz aprendiz)
        {
            ValidarAprendiz(aprendiz);

            // ❗ Regla de negocio: no permitir duplicados por cédula
            var existente = aprendizDAO.BuscarPorCedula(aprendiz.cedulaAprendiz);
            if (existente != null)
                throw new Exception("Ya existe un aprendiz con esta cédula.");

            // ❗ Regla: un usuario solo puede tener un aprendiz
            var usuarioAsociado = aprendizDAO.BuscarPorIdUsuario(aprendiz.idUsuario);
            if (usuarioAsociado != null)
                throw new Exception("Este usuario ya tiene un aprendiz asociado.");

            return aprendizDAO.CrearAprendiz(aprendiz);
        }

        // ==========================
        public List<Aprendiz> ObtenerAprendices()
        {
            return aprendizDAO.ObtenerAprendices();
        }

        // ==========================
        public Aprendiz BuscarPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                throw new Exception("La cédula no puede estar vacía.");

            return aprendizDAO.BuscarPorCedula(cedula);
        }

        // ==========================
        public Aprendiz BuscarPorIdUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new Exception("ID de usuario inválido.");

            return aprendizDAO.BuscarPorIdUsuario(idUsuario);
        }

        // ==========================
        public List<Aprendiz> ObtenerAprendicesPorFecha(DateTime desde, DateTime hasta)
        {
            if (hasta < desde)
                throw new Exception("El rango de fechas es inválido.");

            return aprendizDAO.ObtenerAprendicesPorFecha(desde, hasta);
        }

        // =====================================================
        // VALIDACIONES DE NEGOCIO
        // =====================================================
        private void ValidarAprendiz(Aprendiz aprendiz)
        {
            if (aprendiz == null)
                throw new Exception("El aprendiz no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(aprendiz.cedulaAprendiz))
                throw new Exception("La cédula es obligatoria.");

            if (aprendiz.cedulaAprendiz.Length < 6)
                throw new Exception("La cédula no es válida.");

            if (string.IsNullOrWhiteSpace(aprendiz.nombreAprendiz))
                throw new Exception("El nombre es obligatorio.");

            if (aprendiz.edadAprendiz < 14 || aprendiz.edadAprendiz > 100)
                throw new Exception("Edad fuera de rango válido.");

            if (string.IsNullOrWhiteSpace(aprendiz.correoAprendiz))
                throw new Exception("El correo es obligatorio.");

            // Validación simple email
            if (!Regex.IsMatch(aprendiz.correoAprendiz,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new Exception("Formato de correo inválido.");

            if (string.IsNullOrWhiteSpace(aprendiz.telefonoAprendiz))
                throw new Exception("El teléfono es obligatorio.");

            if (aprendiz.codigoFicha <= 0)
                throw new Exception("Debe especificarse una ficha válida.");

            if (aprendiz.idUsuario <= 0)
                throw new Exception("Usuario inválido.");
        }
    }
}