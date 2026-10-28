using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.PuntosAcceso.Commands.RegistrarPuntoAcceso;

// Comando: "registra este punto de acceso". Devuelve el Id del punto creado.
public sealed record RegistrarPuntoAccesoCommand(string Nombre, string? Descripcion) : IRequest<Guid>;
