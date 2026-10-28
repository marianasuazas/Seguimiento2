using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Entities;
using SGCAE.Domain.Exceptions;

namespace SGCAE.Application.UseCases.Accesos.Queries.ConsultarHistorialAccesos;

// Caso de uso: resuelve la consulta del historial. Solo lee.
public class ConsultarHistorialAccesosUseCase
    : IRequestHandler<ConsultarHistorialAccesosQuery, PaginationResponse<RegistroAccesoDTO>>
{
    private readonly IRegistroAccesoRepository _registros;

    public ConsultarHistorialAccesosUseCase(IRegistroAccesoRepository registros)
    {
        _registros = registros;
    }

    public async Task<PaginationResponse<RegistroAccesoDTO>> Handle(ConsultarHistorialAccesosQuery request)
    {
        if (request.Desde > request.Hasta)
        {
            throw new BusinessRuleException("La fecha inicial no puede ser posterior a la fecha final.");
        }

        HistorialAccesosFiltro filtro = new(request.EmpleadoId, request.Desde, request.Hasta,
                                            request.PuntoAccesoId, request.Resultado);

        // El filtro y la paginación los hace la base de datos: nunca se trae todo el historial.
        (IReadOnlyList<RegistroAcceso> items, int totalCount) =
            await _registros.GetHistorialAsync(filtro, request.Pagination);

        List<RegistroAccesoDTO> dtos = items.Select(r => r.ToDto()).ToList();

        return PaginationResponse<RegistroAccesoDTO>.Create(dtos, totalCount, request.Pagination);
    }
}
