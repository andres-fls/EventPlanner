// ============================================================
// Archivo: AprendizService.cs
// Propósito: Capa de servicio para la lógica de negocio
//            relacionada con los aprendices.
// Contiene métodos para crear aprendices, listarlos, buscar por
//            cédula, buscar por ID de usuario, y obtener aprendices
//            que se inscribieron en un rango de fechas.
// Aplica validaciones de reglas de negocio (campos obligatorios,
//            edad positiva, ficha válida, etc.) antes de llamar al DAO.
// ============================================================

using System;
using System.Collections.Generic;
using EventPlanner.DAO;
using EventPlanner.Models;

namespace EventPlanner.Services
{
    public class AprendizService
    {
        // Instancia del DAO para acceder a los datos de aprendiz
        private AprendizDAO aprendizDAO = new AprendizDAO();

        // ==========================
        // CREAR APRENDIZ
        // ==========================
        // Valida los datos del aprendiz y lo guarda en la base de datos.
        // Parámetro: aprendiz - Objeto Aprendiz con los datos a registrar.
        // Retorna: true si la inserción fue exitosa, false en caso contrario.
        public bool CrearAprendiz(Aprendiz aprendiz)
        {
            // Aplica reglas de negocio antes de insertar
            ValidarAprendiz(aprendiz);
            // Llama al DAO para realizar la inserción
            return aprendizDAO.CrearAprendiz(aprendiz);
        }

        // ==========================
        // LISTAR APRENDICES
        // ==========================
        // Retorna una lista con todos los aprendices registrados.
        public List<Aprendiz> ObtenerAprendices()
        {
            return aprendizDAO.ObtenerAprendices();
        }

        // ==========================
        // BUSCAR POR CÉDULA
        // ==========================
        // Busca un aprendiz por su número de cédula.
        // Parámetro: cedula - Número de cédula a buscar.
        // Retorna: Objeto Aprendiz si se encuentra, o null si no existe.
        // Lanza excepción si la cédula está vacía.
        public Aprendiz BuscarPorCedula(string cedula)
        {
            // Validación de negocio: la cédula no puede estar vacía
            if (string.IsNullOrWhiteSpace(cedula))
                throw new Exception("La cédula no puede estar vacía.");

            return aprendizDAO.BuscarPorCedula(cedula);
        }

        // ==========================
        // VALIDACIONES (REGLAS DE NEGOCIO)
        // ==========================
        // Método privado que verifica que el aprendiz cumpla con las reglas del negocio.
        private void ValidarAprendiz(Aprendiz aprendiz)
        {
            // Validación: cédula obligatoria
            if (string.IsNullOrWhiteSpace(aprendiz.cedulaAprendiz))
                throw new Exception("La cédula del aprendiz es obligatoria.");

            // Validación: nombre obligatorio
            if (string.IsNullOrWhiteSpace(aprendiz.nombreAprendiz))
                throw new Exception("El nombre del aprendiz es obligatorio.");

            // Validación: correo obligatorio
            if (string.IsNullOrWhiteSpace(aprendiz.correoAprendiz))
                throw new Exception("El correo del aprendiz es obligatorio.");

            // Validación: edad debe ser mayor que cero
            if (aprendiz.edadAprendiz <= 0)
                throw new Exception("La edad debe ser mayor a cero.");

            // Validación: código de ficha válido (positivo)
            if (aprendiz.codigoFicha <= 0)
                throw new Exception("Debe especificarse una ficha válida.");
        }

        // ==========================
        // BUSCAR POR ID DE USUARIO
        // ==========================
        // Busca un aprendiz asociado a un ID de usuario específico.
        // Parámetro: idUsuario - Identificador del usuario en la tabla Usuario.
        // Retorna: Objeto Aprendiz si se encuentra, o null si no existe.
        // Lanza excepción si el ID no es válido.
        public Aprendiz BuscarPorIdUsuario(int idUsuario)
        {
            // Validación: el ID debe ser positivo
            if (idUsuario <= 0)
                throw new Exception("ID de usuario inválido.");

            return aprendizDAO.BuscarPorIdUsuario(idUsuario);
        }

        // ==========================
        // OBTENER APRENDICES POR RANGO DE FECHAS
        // ==========================
        // Retorna aprendices que hayan realizado inscripciones dentro del rango de fechas.
        // Parámetros:
        //   desde - Fecha inicial del rango
        //   hasta - Fecha final del rango
        // Retorna: Lista de aprendices (solo información básica).
        public List<Aprendiz> ObtenerAprendicesPorFecha(DateTime desde, DateTime hasta)
        {
            return aprendizDAO.ObtenerAprendicesPorFecha(desde, hasta);
        }
    }
}