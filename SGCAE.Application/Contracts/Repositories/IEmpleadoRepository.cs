using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Entities;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Application.Contracts.Repositories;


public interface IEmpleadoRepository : IRepository<Empleado>
{
    
    Task<bool> ExisteIdentificacionAsync(Identificacion identificacion);

    
    Task<(IReadOnlyList<Empleado> Items, int TotalCount)> GetPageAsync(PaginationRequest pagination);
}
