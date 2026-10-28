using SGCAE.Domain.Entities;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Application.Contracts.Repositories;

// Lo que Application necesita para trabajar con credenciales. Persistence lo implementará con Entity Framework.
public interface ICredencialRepository : IRepository<Credencial>
{
    // Para validar que no se asigne dos veces el mismo código.
    Task<bool> ExisteCodigoAsync(CodigoCredencial codigo);

    // Para validar la regla: un empleado tiene una sola credencial vigente a la vez.
    Task<bool> EmpleadoTieneCredencialVigenteAsync(Guid empleadoId);

    // La usará la validación de accesos: busca la credencial por el código que se leyó.
    Task<Credencial?> GetByCodigoAsync(CodigoCredencial codigo);

    // Todas las credenciales de un empleado (vigentes, bloqueadas y revocadas).
    Task<IReadOnlyList<Credencial>> GetByEmpleadoAsync(Guid empleadoId);
}
