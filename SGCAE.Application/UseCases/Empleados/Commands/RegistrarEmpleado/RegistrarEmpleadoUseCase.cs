using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;
using SGCAE.Domain.Exceptions;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Application.UseCases.Empleados.Commands.RegistrarEmpleado;


public class RegistrarEmpleadoUseCase : IRequestHandler<RegistrarEmpleadoCommand, Guid>
{
    private readonly IEmpleadoRepository _empleados;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrarEmpleadoUseCase(IEmpleadoRepository empleados, IUnitOfWork unitOfWork)
    {
        _empleados = empleados;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RegistrarEmpleadoCommand request)
    {
        
        Identificacion identificacion = new(request.Identificacion);
        CorreoElectronico correo = new(request.Correo);

        
        if (await _empleados.ExisteIdentificacionAsync(identificacion))
        {
            throw new BusinessRuleException("Ya existe un empleado con esa identificación.");
        }

        
        Empleado empleado = new(identificacion, request.Nombres, request.Apellidos,
                                correo, request.Cargo, request.Area);

        
        await _empleados.AddAsync(empleado);
        await _unitOfWork.CommitAsync();

        return empleado.Id;
    }
}