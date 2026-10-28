using SGCAE.Application.Contracts;

namespace SGCAE.Persistence;

// Unit of Work con Entity Framework: confirma de una sola vez todos los cambios pendientes del contexto.
public class EFCoreUnitOfWork : IUnitOfWork
{
    private readonly SGCAEDbContext _context;

    public EFCoreUnitOfWork(SGCAEDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
