using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Exceptions;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Application.UseCases.Empleados.Commands.ActualizarEmpleado;


public class ActualizarEmpleadoUseCase : IRequestHandler<ActualizarEmpleadoCommand, Guid>
{
    private readonly IEmpleadoRepository _empleados;
    private readonly IUnitOfWork _unitOfWork;

    public ActualizarEmpleadoUseCase(IEmpleadoRepository empleados, IUnitOfWork unitOfWork)
    {
        _empleados = empleados;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(ActualizarEmpleadoCommand request)
    {
        
        Empleado empleado = await _empleados.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("El empleado no existe.");

        
        CorreoElectronico correo = new(request.Correo);

        
        empleado.Actualizar(request.Nombres, request.Apellidos, correo, request.Cargo, request.Area);

       
        await _unitOfWork.CommitAsync();

        return empleado.Id;
    }
}
