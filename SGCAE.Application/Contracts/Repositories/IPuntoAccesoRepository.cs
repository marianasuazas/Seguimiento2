using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.Contracts.Repositories;

// Lo que Application necesita para trabajar con puntos de acceso. Persistence lo implementará.
public interface IPuntoAccesoRepository : IRepository<PuntoAcceso>
{
    // Para validar que no se registren dos puntos con el mismo nombre.
    Task<bool> ExisteNombreAsync(string nombre);

    // Devuelve solo una página de puntos de acceso y el total.
    Task<(IReadOnlyList<PuntoAcceso> Items, int TotalCount)> GetPageAsync(PaginationRequest pagination);
}
