using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.PuntosAcceso.Commands.CambiarEstadoPuntoAcceso;

// Comando: "habilita o deshabilita este punto de acceso". Devuelve el Id del punto.
public sealed record CambiarEstadoPuntoAccesoCommand(Guid Id, bool Habilitado) : IRequest<Guid>;
