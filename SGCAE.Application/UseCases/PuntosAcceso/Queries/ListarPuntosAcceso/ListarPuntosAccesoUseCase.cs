using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.PuntosAcceso.Queries.ListarPuntosAcceso;

// Caso de uso: resuelve la consulta ListarPuntosAccesoQuery. Solo lee.
public class ListarPuntosAccesoUseCase : IRequestHandler<ListarPuntosAccesoQuery, PaginationResponse<PuntoAccesoDTO>>
{
    private readonly IPuntoAccesoRepository _puntos;

    public ListarPuntosAccesoUseCase(IPuntoAccesoRepository puntos)
    {
        _puntos = puntos;
    }

    public async Task<PaginationResponse<PuntoAccesoDTO>> Handle(ListarPuntosAccesoQuery request)
    {
        (IReadOnlyList<PuntoAcceso> items, int totalCount) = await _puntos.GetPageAsync(request.Pagination);

        List<PuntoAccesoDTO> dtos = items.Select(p => p.ToDto()).ToList();

        return PaginationResponse<PuntoAccesoDTO>.Create(dtos, totalCount, request.Pagination);
    }
}
