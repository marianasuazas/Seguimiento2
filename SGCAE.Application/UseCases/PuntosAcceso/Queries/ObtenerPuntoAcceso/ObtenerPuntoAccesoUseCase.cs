using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.PuntosAcceso.Queries.ObtenerPuntoAcceso;

// Caso de uso: resuelve la consulta ObtenerPuntoAccesoQuery. Solo lee.
public class ObtenerPuntoAccesoUseCase : IRequestHandler<ObtenerPuntoAccesoQuery, PuntoAccesoDTO?>
{
    private readonly IPuntoAccesoRepository _puntos;

    public ObtenerPuntoAccesoUseCase(IPuntoAccesoRepository puntos)
    {
        _puntos = puntos;
    }

    public async Task<PuntoAccesoDTO?> Handle(ObtenerPuntoAccesoQuery request)
    {
        PuntoAcceso? punto = await _puntos.GetByIdAsync(request.Id);

        return punto?.ToDto();
    }
}
