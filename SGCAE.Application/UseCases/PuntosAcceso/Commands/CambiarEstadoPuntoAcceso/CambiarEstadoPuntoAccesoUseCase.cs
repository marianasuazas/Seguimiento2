using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Exceptions;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.PuntosAcceso.Commands.CambiarEstadoPuntoAcceso;

// Caso de uso: resuelve el comando CambiarEstadoPuntoAccesoCommand.
public class CambiarEstadoPuntoAccesoUseCase : IRequestHandler<CambiarEstadoPuntoAccesoCommand, Guid>
{
    private readonly IPuntoAccesoRepository _puntos;
    private readonly IUnitOfWork _unitOfWork;

    public CambiarEstadoPuntoAccesoUseCase(IPuntoAccesoRepository puntos, IUnitOfWork unitOfWork)
    {
        _puntos = puntos;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CambiarEstadoPuntoAccesoCommand request)
    {
        PuntoAcceso punto = await _puntos.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("El punto de acceso no existe.");

        // La entidad cambia su propio estado; si ya estaba en ese estado, lanza una excepción de negocio.
        if (request.Habilitado)
        {
            punto.Habilitar();
        }
        else
        {
            punto.Deshabilitar();
        }

        await _unitOfWork.CommitAsync();

        return punto.Id;
    }
}
