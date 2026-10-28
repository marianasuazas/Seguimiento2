using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.Credenciales.Commands.RevocarCredencial;

// Comando: "revoca esta credencial" (definitivo). Devuelve el Id de la credencial.
public sealed record RevocarCredencialCommand(Guid Id) : IRequest<Guid>;
