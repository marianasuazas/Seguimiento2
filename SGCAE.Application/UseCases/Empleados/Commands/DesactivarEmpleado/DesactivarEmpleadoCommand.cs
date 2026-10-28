using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.Empleados.Commands.DesactivarEmpleado;


public sealed record DesactivarEmpleadoCommand(Guid Id) : IRequest<Guid>;