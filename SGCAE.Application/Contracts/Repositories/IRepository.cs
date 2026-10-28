namespace SGCAE.Application.Contracts.Repositories;


public interface IRepository<TEntity> where TEntity : class
{
    
    Task AddAsync(TEntity entity);

    
    Task<TEntity?> GetByIdAsync(Guid id);
}