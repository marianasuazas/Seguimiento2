using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.Credenciales.Commands.BloquearCredencial;

// Comando: "bloquea esta credencial". Devuelve el Id de la credencial.
public sealed record BloquearCredencialCommand(Guid Id) : IRequest<Guid>;
