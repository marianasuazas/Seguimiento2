using Microsoft.AspNetCore.Mvc;
using SGCAE.Api.Requests;
using SGCAE.Application.UseCases.PuntosAcceso;
using SGCAE.Application.UseCases.PuntosAcceso.Commands.CambiarEstadoPuntoAcceso;
using SGCAE.Application.UseCases.PuntosAcceso.Commands.RegistrarPuntoAcceso;
using SGCAE.Application.UseCases.PuntosAcceso.Queries.ListarPuntosAcceso;
using SGCAE.Application.UseCases.PuntosAcceso.Queries.ObtenerPuntoAcceso;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Application.Utilities.Pagination;

namespace SGCAE.Api.Controllers;

[ApiController]
[Route("api/puntos-acceso")]
public class PuntosAccesoController : ControllerBase
{
    private readonly IMediator _mediator;

    public PuntosAccesoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // RF-06: registrar un punto de acceso.
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registrar(RegistrarPuntoAccesoCommand command)
    {
        Guid id = await _mediator.Send(command);

        return CreatedAtAction(nameof(Obtener), new { id }, new { id });
    }

    // RF-06: consultar los puntos de acceso, paginados; cada uno indica si está habilitado.
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginationResponse<PuntoAccesoDTO>>> Listar(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = PaginationRequest.DefaultPageSize)
    {
        ListarPuntosAccesoQuery query = new() { Pagination = new PaginationRequest(pageNumber, pageSize) };

        return Ok(await _mediator.Send(query));
    }

    // RF-06: consultar un punto de acceso por su id.
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PuntoAccesoDTO>> Obtener(Guid id)
    {
        PuntoAccesoDTO? punto = await _mediator.Send(new ObtenerPuntoAccesoQuery(id));

        if (punto is null)
        {
            return NotFound(new { error = "El punto de acceso no existe." });
        }

        return Ok(punto);
    }

    // RF-06: habilitar o deshabilitar un punto de acceso.
    [HttpPatch("{id:guid}/estado")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CambiarEstado(Guid id, CambiarEstadoPuntoAccesoRequest request)
    {
        await _mediator.Send(new CambiarEstadoPuntoAccesoCommand(id, request.Habilitado));

        return NoContent();
    }
}
