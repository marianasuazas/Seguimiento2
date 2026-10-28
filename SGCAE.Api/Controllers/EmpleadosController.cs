using Microsoft.AspNetCore.Mvc;
using SGCAE.Api.Requests;
using SGCAE.Application.UseCases.Empleados;
using SGCAE.Application.UseCases.Empleados.Commands.ActualizarEmpleado;
using SGCAE.Application.UseCases.Empleados.Commands.DesactivarEmpleado;
using SGCAE.Application.UseCases.Empleados.Commands.RegistrarEmpleado;
using SGCAE.Application.UseCases.Empleados.Queries.ListarEmpleados;
using SGCAE.Application.UseCases.Empleados.Queries.ObtenerEmpleado;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Application.Utilities.Pagination;

namespace SGCAE.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class EmpleadosController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmpleadosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // RF-01: registrar un empleado.
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registrar(RegistrarEmpleadoCommand command)
    {
        Guid id = await _mediator.Send(command);

        return CreatedAtAction(nameof(Obtener), new { id }, new { id });
    }

    // RF-01: consultar los empleados, paginados.
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginationResponse<EmpleadoDTO>>> Listar(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = PaginationRequest.DefaultPageSize)
    {
        ListarEmpleadosQuery query = new() { Pagination = new PaginationRequest(pageNumber, pageSize) };

        return Ok(await _mediator.Send(query));
    }

    // RF-01: consultar un empleado por su id.
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpleadoDTO>> Obtener(Guid id)
    {
        EmpleadoDTO? empleado = await _mediator.Send(new ObtenerEmpleadoQuery(id));

        if (empleado is null)
        {
            return NotFound(new { error = "El empleado no existe." });
        }

        return Ok(empleado);
    }

    // RF-01: actualizar los datos de un empleado.
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Actualizar(Guid id, ActualizarEmpleadoRequest request)
    {
        await _mediator.Send(new ActualizarEmpleadoCommand(id, request.Nombres, request.Apellidos,
                                                           request.Correo, request.Cargo, request.Area));

        return NoContent();
    }

    // RF-01: desactivar un empleado.
    [HttpPatch("{id:guid}/desactivar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Desactivar(Guid id)
    {
        await _mediator.Send(new DesactivarEmpleadoCommand(id));

        return NoContent();
    }
}
