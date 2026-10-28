using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.Credenciales.Queries.ObtenerCredencial;

// Consulta: "quiero la credencial con este id" (incluye su estado). Nulo si no existe.
public sealed record ObtenerCredencialQuery(Guid Id) : IRequest<CredencialDTO?>;
