using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.Credenciales.Commands.AsignarCredencial;

// Comando: "asigna esta credencial a este empleado". Devuelve el Id de la credencial creada.
public sealed record AsignarCredencialCommand(Guid EmpleadoId, string Codigo) : IRequest<Guid>;
