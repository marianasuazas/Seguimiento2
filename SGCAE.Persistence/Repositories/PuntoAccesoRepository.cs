using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Entities;

namespace SGCAE.Persistence.Repositories;

public class PuntoAccesoRepository : Repository<PuntoAcceso>, IPuntoAccesoRepository
{
    public PuntoAccesoRepository(SGCAEDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteNombreAsync(string nombre)
    {
        return await Context.PuntosAcceso.AnyAsync(p => p.Nombre == nombre);
    }

    public async Task<(IReadOnlyList<PuntoAcceso> Items, int TotalCount)> GetPageAsync(PaginationRequest pagination)
    {
        int total = await Context.PuntosAcceso.CountAsync();

        List<PuntoAcceso> items = await Context.PuntosAcceso
            .AsNoTracking()
            .OrderBy(p => p.Nombre)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize)
            .ToListAsync();

        return (items, total);
    }
}
