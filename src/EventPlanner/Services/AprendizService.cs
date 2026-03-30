using System;
using System.Collections.Generic;
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
            return aprendizDAO.CrearAprendiz(aprendiz);
        }

        // ==========================
        // LISTAR APRENDICES
        // ==========================
        public List<Aprendiz> ObtenerAprendices()
        {
            return aprendizDAO.ObtenerAprendices();
        }

        // ==========================
        // BUSCAR POR CÉDULA
        // ==========================
        public Aprendiz BuscarPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                throw new Exception("La cédula no puede estar vacía.");

            return aprendizDAO.BuscarPorCedula(cedula);
        }

        // ==========================
        // VALIDACIONES (REGLAS NEGOCIO)
        // ==========================
        private void ValidarAprendiz(Aprendiz aprendiz)
        {
            if (string.IsNullOrWhiteSpace(aprendiz.cedulaAprendiz))
                throw new Exception("La cédula del aprendiz es obligatoria.");

            if (string.IsNullOrWhiteSpace(aprendiz.nombreAprendiz))
                throw new Exception("El nombre del aprendiz es obligatorio.");

            if (string.IsNullOrWhiteSpace(aprendiz.correoAprendiz))
                throw new Exception("El correo del aprendiz es obligatorio.");

            if (aprendiz.edadAprendiz <= 0)
                throw new Exception("La edad debe ser mayor a cero.");

            if (aprendiz.codigoFicha <= 0)
                throw new Exception("Debe especificarse una ficha válida.");
        }
    }
}