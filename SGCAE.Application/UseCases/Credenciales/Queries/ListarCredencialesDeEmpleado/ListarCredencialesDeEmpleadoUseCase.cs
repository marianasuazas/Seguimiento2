using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Credenciales.Queries.ListarCredencialesDeEmpleado;

// Caso de uso: resuelve la consulta ListarCredencialesDeEmpleadoQuery. Solo lee.
public class ListarCredencialesDeEmpleadoUseCase
    : IRequestHandler<ListarCredencialesDeEmpleadoQuery, List<CredencialDTO>>
{
    private readonly ICredencialRepository _credenciales;

    public ListarCredencialesDeEmpleadoUseCase(ICredencialRepository credenciales)
    {
        _credenciales = credenciales;
    }

    public async Task<List<CredencialDTO>> Handle(ListarCredencialesDeEmpleadoQuery request)
    {
        IReadOnlyList<Credencial> credenciales = await _credenciales.GetByEmpleadoAsync(request.EmpleadoId);

        return credenciales.Select(c => c.ToDto()).ToList();
    }
}
