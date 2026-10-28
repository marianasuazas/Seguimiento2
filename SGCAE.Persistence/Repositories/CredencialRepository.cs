using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Domain.Entities;
using SGCAE.Domain.Enums;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Persistence.Repositories;

public class CredencialRepository : Repository<Credencial>, ICredencialRepository
{
    public CredencialRepository(SGCAEDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteCodigoAsync(CodigoCredencial codigo)
    {
        return await Context.Credenciales.AnyAsync(c => c.Codigo == codigo);
    }

    public async Task<bool> EmpleadoTieneCredencialVigenteAsync(Guid empleadoId)
    {
        return await Context.Credenciales
            .AnyAsync(c => c.EmpleadoId == empleadoId && c.Estado == EstadoCredencial.Vigente);
    }

    public async Task<Credencial?> GetByCodigoAsync(CodigoCredencial codigo)
    {
        return await Context.Credenciales
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Codigo == codigo);
    }

    public async Task<IReadOnlyList<Credencial>> GetByEmpleadoAsync(Guid empleadoId)
    {
        return await Context.Credenciales
            .AsNoTracking()
            .Where(c => c.EmpleadoId == empleadoId)
            .OrderByDescending(c => c.FechaAsignacion)
            .ToListAsync();
    }
}
