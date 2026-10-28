using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Enums;

namespace SGCAE.Application.UseCases.Accesos.Commands.RegistrarAcceso;

// Comando: "alguien pasó esta credencial por este punto de acceso para entrar o salir".
// Devuelve el registro creado, que dice si el acceso fue autorizado o rechazado (y por qué).
public sealed record RegistrarAccesoCommand(
    string Codigo,
    Guid PuntoAccesoId,
    TipoMovimiento Tipo) : IRequest<RegistroAccesoDTO>;
