using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Credenciales.Queries.ObtenerCredencial;

// Caso de uso: resuelve la consulta ObtenerCredencialQuery. Solo lee.
public class ObtenerCredencialUseCase : IRequestHandler<ObtenerCredencialQuery, CredencialDTO?>
{
    private readonly ICredencialRepository _credenciales;

    public ObtenerCredencialUseCase(ICredencialRepository credenciales)
    {
        _credenciales = credenciales;
    }

    public async Task<CredencialDTO?> Handle(ObtenerCredencialQuery request)
    {
        Credencial? credencial = await _credenciales.GetByIdAsync(request.Id);

        return credencial?.ToDto();
    }
}
