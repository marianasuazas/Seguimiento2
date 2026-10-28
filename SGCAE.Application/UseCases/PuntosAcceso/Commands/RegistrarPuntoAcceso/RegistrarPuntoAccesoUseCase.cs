using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;
using SGCAE.Domain.Exceptions;

namespace SGCAE.Application.UseCases.PuntosAcceso.Commands.RegistrarPuntoAcceso;

// Caso de uso: resuelve el comando RegistrarPuntoAccesoCommand.
public class RegistrarPuntoAccesoUseCase : IRequestHandler<RegistrarPuntoAccesoCommand, Guid>
{
    private readonly IPuntoAccesoRepository _puntos;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrarPuntoAccesoUseCase(IPuntoAccesoRepository puntos, IUnitOfWork unitOfWork)
    {
        _puntos = puntos;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RegistrarPuntoAccesoCommand request)
    {
        // 1. La entidad valida el nombre y la descripción (y limpia los espacios sobrantes).
        PuntoAcceso punto = new(request.Nombre, request.Descripcion);

        // 2. Regla que necesita la base de datos: el nombre no puede repetirse.
        if (await _puntos.ExisteNombreAsync(punto.Nombre))
        {
            throw new BusinessRuleException("Ya existe un punto de acceso con ese nombre.");
        }

        await _puntos.AddAsync(punto);
        await _unitOfWork.CommitAsync();

        return punto.Id;
    }
}
