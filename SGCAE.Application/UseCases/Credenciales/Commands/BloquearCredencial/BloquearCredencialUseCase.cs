using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Exceptions;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Credenciales.Commands.BloquearCredencial;

// Caso de uso: resuelve el comando BloquearCredencialCommand.
public class BloquearCredencialUseCase : IRequestHandler<BloquearCredencialCommand, Guid>
{
    private readonly ICredencialRepository _credenciales;
    private readonly IUnitOfWork _unitOfWork;

    public BloquearCredencialUseCase(ICredencialRepository credenciales, IUnitOfWork unitOfWork)
    {
        _credenciales = credenciales;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(BloquearCredencialCommand request)
    {
        Credencial credencial = await _credenciales.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("La credencial no existe.");

        // La entidad decide si se puede bloquear (no una revocada ni una ya bloqueada).
        credencial.Bloquear();

        await _unitOfWork.CommitAsync();

        return credencial.Id;
    }
}
