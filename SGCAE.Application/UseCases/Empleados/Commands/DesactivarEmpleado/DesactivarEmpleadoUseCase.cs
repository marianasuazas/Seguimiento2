using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Exceptions;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Empleados.Commands.DesactivarEmpleado;


public class DesactivarEmpleadoUseCase : IRequestHandler<DesactivarEmpleadoCommand, Guid>
{
    private readonly IEmpleadoRepository _empleados;
    private readonly IUnitOfWork _unitOfWork;

    public DesactivarEmpleadoUseCase(IEmpleadoRepository empleados, IUnitOfWork unitOfWork)
    {
        _empleados = empleados;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DesactivarEmpleadoCommand request)
    {
        
        Empleado empleado = await _empleados.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("El empleado no existe.");

        
        empleado.Desactivar();

        
        await _unitOfWork.CommitAsync();

        return empleado.Id;
    }
}
