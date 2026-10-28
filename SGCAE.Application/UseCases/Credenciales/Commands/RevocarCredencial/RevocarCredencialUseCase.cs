using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Exceptions;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Credenciales.Commands.RevocarCredencial;

// Caso de uso: resuelve el comando RevocarCredencialCommand.
public class RevocarCredencialUseCase : IRequestHandler<RevocarCredencialCommand, Guid>
{
    private readonly ICredencialRepository _credenciales;
    private readonly IUnitOfWork _unitOfWork;

    public RevocarCredencialUseCase(ICredencialRepository credenciales, IUnitOfWork unitOfWork)
    {
        _credenciales = credenciales;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RevocarCredencialCommand request)
    {
        Credencial credencial = await _credenciales.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("La credencial no existe.");

        // La entidad decide si se puede revocar (no una ya revocada).
        credencial.Revocar();

        await _unitOfWork.CommitAsync();

        return credencial.Id;
    }
}
