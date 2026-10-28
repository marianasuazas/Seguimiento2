using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.Empleados.Commands.ActualizarEmpleado;


public sealed record ActualizarEmpleadoCommand(
    Guid Id,
    string Nombres,
    string Apellidos,
    string Correo,
    string Cargo,
    string Area) : IRequest<Guid>;
