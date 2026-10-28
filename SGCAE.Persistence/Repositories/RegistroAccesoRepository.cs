using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Entities;
using SGCAE.Domain.Enums;

namespace SGCAE.Persistence.Repositories;

public class RegistroAccesoRepository : Repository<RegistroAcceso>, IRegistroAccesoRepository
{
    public RegistroAccesoRepository(SGCAEDbContext context) : base(context)
    {
    }

    public async Task<TipoMovimiento?> GetUltimoMovimientoAutorizadoAsync(Guid empleadoId)
    {
        // Toma solo el último movimiento autorizado del empleado (los rechazados no cuentan).
        return await Context.RegistrosAcceso
            .AsNoTracking()
            .Where(r => r.EmpleadoId == empleadoId && r.Resultado == ResultadoAcceso.Autorizado)
            .OrderByDescending(r => r.FechaHora)
            .Select(r => (TipoMovimiento?)r.Tipo)
            .FirstOrDefaultAsync();
    }

    public async Task<(IReadOnlyList<RegistroAcceso> Items, int TotalCount)> GetHistorialAsync(
        HistorialAccesosFiltro filtro, PaginationRequest pagination)
    {
        IQueryable<RegistroAcceso> query = Context.RegistrosAcceso.AsNoTracking();

        // Cada filtro se agrega solo si el usuario lo pidió.
        if (filtro.EmpleadoId is not null)
        {
            query = query.Where(r => r.EmpleadoId == filtro.EmpleadoId);
        }

        if (filtro.PuntoAccesoId is not null)
        {
            query = query.Where(r => r.PuntoAccesoId == filtro.PuntoAccesoId);
        }

        if (filtro.Resultado is not null)
        {
            query = query.Where(r => r.Resultado == filtro.Resultado);
        }

        if (filtro.Desde is not null)
        {
            query = query.Where(r => r.FechaHora >= filtro.Desde);
        }

        if (filtro.Hasta is not null)
        {
            query = query.Where(r => r.FechaHora <= filtro.Hasta);
        }

        // El total se cuenta con los mismos filtros, y después se trae solo la página, de la más reciente a la más antigua.
        int total = await query.CountAsync();

        List<RegistroAcceso> items = await query
            .OrderByDescending(r => r.FechaHora)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize)
            .ToListAsync();

        return (items, total);
    }
}
