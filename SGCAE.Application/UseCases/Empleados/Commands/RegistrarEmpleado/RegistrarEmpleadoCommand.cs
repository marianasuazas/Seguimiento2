using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.Empleados.Commands.RegistrarEmpleado;


public sealed record RegistrarEmpleadoCommand(
    string Identificacion,
    string Nombres,
    string Apellidos,
    string Correo,
    string Cargo,
    string Area) : IRequest<Guid>;