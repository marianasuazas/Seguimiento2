using SGCAE.Application.Contracts.Repositories;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Empleados.Queries.ObtenerEmpleado;


public class ObtenerEmpleadoUseCase : IRequestHandler<ObtenerEmpleadoQuery, EmpleadoDTO?>
{
    private readonly IEmpleadoRepository _empleados;

    public ObtenerEmpleadoUseCase(IEmpleadoRepository empleados)
    {
        _empleados = empleados;
    }

    public async Task<EmpleadoDTO?> Handle(ObtenerEmpleadoQuery request)
    {
        
        Empleado? empleado = await _empleados.GetByIdAsync(request.Id);

       
        return empleado?.ToDto();
    }
}
