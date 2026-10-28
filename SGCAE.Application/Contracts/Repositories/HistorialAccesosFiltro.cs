using SGCAE.Domain.Enums;

namespace SGCAE.Application.Contracts.Repositories;

// Filtros opcionales para consultar el historial de accesos. Los que vienen nulos no se aplican.
public sealed record HistorialAccesosFiltro(
    Guid? EmpleadoId,
    DateTime? Desde,
    DateTime? Hasta,
    Guid? PuntoAccesoId,
    ResultadoAcceso? Resultado);
