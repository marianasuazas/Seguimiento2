namespace SGCAE.Application.Contracts;

// Unit of Work: confirma de una sola vez todos los cambios pendientes de un caso de uso.
public interface IUnitOfWork
{
    Task CommitAsync();
}