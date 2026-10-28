using SGCAE.Application.Utilities.Mediator;
using SGCAE.Application.Utilities.Pagination;

namespace SGCAE.Application.UseCases.PuntosAcceso.Queries.ListarPuntosAcceso;

// Consulta: "quiero la lista de puntos de acceso", paginada. Cada uno indica si está habilitado.
public class ListarPuntosAccesoQuery : IRequest<PaginationResponse<PuntoAccesoDTO>>
{
    public PaginationRequest Pagination { get; set; } = PaginationRequest.Default();
}
