using EventPlanner.DAO;
using EventPlanner.Models;
using EventPlanner.Services;
using System;
using System.Collections.Generic;

public class InscripcionService
{
    private InscripcionDAO inscripcionDAO = new InscripcionDAO();
    private EventoService eventoService = new EventoService();

    // ==========================
    // LISTADOS
    // ==========================
    public List<Inscripcion> ObtenerInscripciones()
    {
        return inscripcionDAO.ObtenerInscripciones();
    }

    public List<InscripcionDetalle> ObtenerInscripcionesConDetalle()
    {
        return inscripcionDAO.ObtenerInscripcionesConDetalle();
    }

    // ==========================
    // INSCRIBIR
    // ==========================
    public void InscribirAprendiz(int idAprendiz, int idEvento)
    {
        if (idAprendiz <= 0)
            throw new Exception("Aprendiz inválido.");

        if (idEvento <= 0)
            throw new Exception("Evento inválido.");

        Evento evento = eventoService.ObtenerEventoPorId(idEvento);

        if (evento == null)
            throw new Exception("El evento no existe.");

        if (!evento.activo)
            throw new Exception("El evento no está activo.");

        ValidarPeriodoInscripcion(evento);

        if (inscripcionDAO.TieneCruceHorario(idAprendiz, evento.fechaInicioEvento))
            throw new Exception("Ya tienes una inscripción en ese horario.");

        if (inscripcionDAO.ExisteInscripcion(idAprendiz, idEvento))
            throw new Exception("Ya estás inscrito en este evento.");

        Inscripcion nueva = new Inscripcion()
        {
            idAprendiz = idAprendiz,
            idEvento = idEvento,
            tipoInscripcion = "Individual",
            modalidad = "Presencial",
            estadoInscripcion = "Activo"
        };

        inscripcionDAO.CrearInscripcion(nueva);
    }

    // ==========================
    // CANCELAR
    // ==========================
    public void CancelarInscripcionAprendiz(int idAprendiz, int idEvento)
    {
        if (idAprendiz <= 0)
            throw new Exception("Aprendiz inválido.");

        List<InscripcionDetalle> lista =
            inscripcionDAO.ObtenerInscripcionesConDetalle();

        InscripcionDetalle existente = lista.Find(i =>
            i.idAprendiz == idAprendiz &&
            i.idEvento == idEvento &&
            i.estadoInscripcion == "Activo");

        if (existente == null)
            throw new Exception("No existe una inscripción activa.");

        inscripcionDAO.CancelarInscripcion(existente.idInscripcion);
    }

    // ==========================
    // ADMIN
    // ==========================
    public void ActualizarEstado(int idInscripcion, string nuevoEstado)
    {
        if (idInscripcion <= 0)
            throw new Exception("ID inválido.");

        if (string.IsNullOrWhiteSpace(nuevoEstado))
            throw new Exception("Estado inválido.");

        inscripcionDAO.ActualizarEstado(idInscripcion, nuevoEstado);
    }

    // ==========================
    // VALIDACIONES
    // ==========================
    private void ValidarPeriodoInscripcion(Evento evento)
    {
        DateTime ahora = DateTime.Now;

        if (ahora < evento.fechaInicioInscripcion)
            throw new Exception("El período de inscripción aún no inicia.");

        if (ahora > evento.fechaFinInscripcion)
            throw new Exception("El período de inscripción finalizó.");
    }

    // =====================================================
    // VALIDAR SI YA ESTÁ INSCRITO
    // =====================================================
    public bool AprendizYaInscrito(int idAprendiz, int idEvento)
    {
        if (idAprendiz <= 0)
            throw new Exception("Aprendiz inválido.");

        if (idEvento <= 0)
            throw new Exception("Evento inválido.");

        return inscripcionDAO.ExisteInscripcion(idAprendiz, idEvento);
    }

}