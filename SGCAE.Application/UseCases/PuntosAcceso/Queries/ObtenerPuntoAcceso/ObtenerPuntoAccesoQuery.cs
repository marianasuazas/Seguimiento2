using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Application.UseCases.PuntosAcceso.Queries.ObtenerPuntoAcceso;

// Consulta: "quiero el punto de acceso con este id". Nulo si no existe.
public sealed record ObtenerPuntoAccesoQuery(Guid Id) : IRequest<PuntoAccesoDTO?>;
