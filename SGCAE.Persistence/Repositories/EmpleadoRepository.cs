using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Entities;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Persistence.Repositories;

public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
{
    public EmpleadoRepository(SGCAEDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteIdentificacionAsync(Identificacion identificacion)
    {
        return await Context.Empleados.AnyAsync(e => e.Identificacion == identificacion);
    }

    public async Task<(IReadOnlyList<Empleado> Items, int TotalCount)> GetPageAsync(PaginationRequest pagination)
    {
        // Cuenta el total y trae solo la página pedida: la paginación ocurre en la base de datos.
        int total = await Context.Empleados.CountAsync();

        List<Empleado> items = await Context.Empleados
            .AsNoTracking()
            .OrderBy(e => e.Apellidos)
            .ThenBy(e => e.Nombres)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize)
            .ToListAsync();

        return (items, total);
    }
}
