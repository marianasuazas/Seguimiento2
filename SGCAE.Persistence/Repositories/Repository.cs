using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Contracts.Repositories;

namespace SGCAE.Persistence.Repositories;

// Implementación genérica: lo que comparten todos los repositorios.
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly SGCAEDbContext Context;

    public Repository(SGCAEDbContext context)
    {
        Context = context;
    }

    // Solo prepara la entidad; la base de datos se modifica cuando el Unit of Work confirma.
    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    // Busca por Id y deja la entidad "vigilada", para que los cambios se guarden con el Unit of Work.
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }
}
