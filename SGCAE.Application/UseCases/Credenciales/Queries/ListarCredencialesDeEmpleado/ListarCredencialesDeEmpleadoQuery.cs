using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.Credenciales.Queries.ListarCredencialesDeEmpleado;

// Consulta: "quiero todas las credenciales de este empleado". La lista queda vacía si no tiene.
public sealed record ListarCredencialesDeEmpleadoQuery(Guid EmpleadoId) : IRequest<List<CredencialDTO>>;
