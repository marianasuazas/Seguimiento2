using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Entities;
using SGCAE.Domain.Enums;

namespace SGCAE.Application.Contracts.Repositories;

// Lo que Application necesita para trabajar con el historial de accesos. Persistence lo implementará.
public interface IRegistroAccesoRepository : IRepository<RegistroAcceso>
{
    // Último movimiento autorizado de un empleado (entrada o salida). Nulo si nunca ha tenido uno.
    // Sirve para saber si el empleado está dentro o fuera de las instalaciones.
    Task<TipoMovimiento?> GetUltimoMovimientoAutorizadoAsync(Guid empleadoId);

    // Historial filtrado, de más reciente a más antiguo. Devuelve solo una página y el total.
    Task<(IReadOnlyList<RegistroAcceso> Items, int TotalCount)> GetHistorialAsync(
        HistorialAccesosFiltro filtro, PaginationRequest pagination);
}
