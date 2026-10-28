using SGCAE.Application.Utilities.Mediator;
using SGCAE.Application.Utilities.Pagination;

namespace SGCAE.Application.UseCases.Empleados.Queries.ListarEmpleados;


public class ListarEmpleadosQuery : IRequest<PaginationResponse<EmpleadoDTO>>
{
    
    public PaginationRequest Pagination { get; set; } = PaginationRequest.Default();
}
