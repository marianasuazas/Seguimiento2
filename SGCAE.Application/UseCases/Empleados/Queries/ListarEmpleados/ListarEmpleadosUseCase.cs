using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Empleados.Queries.ListarEmpleados;


public class ListarEmpleadosUseCase : IRequestHandler<ListarEmpleadosQuery, PaginationResponse<EmpleadoDTO>>
{
    private readonly IEmpleadoRepository _empleados;

    public ListarEmpleadosUseCase(IEmpleadoRepository empleados)
    {
        _empleados = empleados;
    }

    public async Task<PaginationResponse<EmpleadoDTO>> Handle(ListarEmpleadosQuery request)
    {
        
        (IReadOnlyList<Empleado> items, int totalCount) = await _empleados.GetPageAsync(request.Pagination);

        
        List<EmpleadoDTO> dtos = items.Select(e => e.ToDto()).ToList();

        
        return PaginationResponse<EmpleadoDTO>.Create(dtos, totalCount, request.Pagination);
    }
}
