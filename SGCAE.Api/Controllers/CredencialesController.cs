using Microsoft.AspNetCore.Mvc;
using SGCAE.Application.UseCases.Credenciales;
using SGCAE.Application.UseCases.Credenciales.Commands.AsignarCredencial;
using SGCAE.Application.UseCases.Credenciales.Commands.BloquearCredencial;
using SGCAE.Application.UseCases.Credenciales.Commands.RevocarCredencial;
using SGCAE.Application.UseCases.Credenciales.Queries.ListarCredencialesDeEmpleado;
using SGCAE.Application.UseCases.Credenciales.Queries.ObtenerCredencial;
using SGCAE.Application.Utilities.Mediator;

namespace SGCAE.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CredencialesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CredencialesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // RF-02: asignar una credencial a un empleado.
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Asignar(AsignarCredencialCommand command)
    {
        Guid id = await _mediator.Send(command);

        return CreatedAtAction(nameof(Obtener), new { id }, new { id });
    }

    // RF-02: consultar una credencial y su estado.
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CredencialDTO>> Obtener(Guid id)
    {
        CredencialDTO? credencial = await _mediator.Send(new ObtenerCredencialQuery(id));

        if (credencial is null)
        {
            return NotFound(new { error = "La credencial no existe." });
        }

        return Ok(credencial);
    }

    // RF-02: consultar todas las credenciales de un empleado.
    [HttpGet("/api/empleados/{empleadoId:guid}/credenciales")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CredencialDTO>>> ListarDeEmpleado(Guid empleadoId)
    {
        return Ok(await _mediator.Send(new ListarCredencialesDeEmpleadoQuery(empleadoId)));
    }

    // RF-02: bloquear una credencial (temporal).
    [HttpPatch("{id:guid}/bloquear")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Bloquear(Guid id)
    {
        await _mediator.Send(new BloquearCredencialCommand(id));

        return NoContent();
    }

    // RF-02: revocar una credencial (definitivo).
    [HttpPatch("{id:guid}/revocar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Revocar(Guid id)
    {
        await _mediator.Send(new RevocarCredencialCommand(id));

        return NoContent();
    }
}
