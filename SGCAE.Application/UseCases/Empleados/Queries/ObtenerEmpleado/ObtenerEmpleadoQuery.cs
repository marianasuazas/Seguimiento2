using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.Empleados.Queries.ObtenerEmpleado;


public sealed record ObtenerEmpleadoQuery(Guid Id) : IRequest<EmpleadoDTO?>;
