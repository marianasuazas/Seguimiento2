using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Exceptions;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;
using SGCAE.Domain.Exceptions;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Application.UseCases.Credenciales.Commands.AsignarCredencial;

// Caso de uso: resuelve el comando AsignarCredencialCommand.
public class AsignarCredencialUseCase : IRequestHandler<AsignarCredencialCommand, Guid>
{
    private readonly IEmpleadoRepository _empleados;
    private readonly ICredencialRepository _credenciales;
    private readonly IUnitOfWork _unitOfWork;

    public AsignarCredencialUseCase(IEmpleadoRepository empleados, ICredencialRepository credenciales,
                                    IUnitOfWork unitOfWork)
    {
        _empleados = empleados;
        _credenciales = credenciales;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AsignarCredencialCommand request)
    {
        // 1. El value object valida el formato del código.
        CodigoCredencial codigo = new(request.Codigo);

        // 2. El empleado debe existir y estar activo.
        Empleado empleado = await _empleados.GetByIdAsync(request.EmpleadoId)
            ?? throw new NotFoundException("El empleado no existe.");

        if (!empleado.EstaActivo)
        {
            throw new BusinessRuleException("No se puede asignar una credencial a un empleado inactivo.");
        }

        // 3. Reglas que necesitan la base de datos: código único y una sola credencial vigente por empleado.
        if (await _credenciales.ExisteCodigoAsync(codigo))
        {
            throw new BusinessRuleException("Ya existe una credencial con ese código.");
        }

        if (await _credenciales.EmpleadoTieneCredencialVigenteAsync(empleado.Id))
        {
            throw new BusinessRuleException("El empleado ya tiene una credencial vigente.");
        }

        // 4. Crea la credencial (nace vigente) y la guarda.
        Credencial credencial = new(empleado.Id, codigo);

        await _credenciales.AddAsync(credencial);
        await _unitOfWork.CommitAsync();

        return credencial.Id;
    }
}
