using Microsoft.AspNetCore.Mvc;
using SGCAE.Application.UseCases.Accesos;
using SGCAE.Application.UseCases.Accesos.Commands.RegistrarAcceso;
using SGCAE.Application.UseCases.Accesos.Queries.ConsultarHistorialAccesos;
using SGCAE.Application.Utilities.Mediator;
using SGCAE.Application.Utilities.Pagination;
using SGCAE.Domain.Enums;

namespace SGCAE.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccesosController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccesosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // RF-03: registrar una entrada o salida. Un acceso rechazado también responde 200:
    // el resultado (Autorizado o Rechazado) y el motivo vienen en el cuerpo.
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RegistroAccesoDTO>> Registrar(RegistrarAccesoCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    // RF-04: consultar el historial de accesos con filtros opcionales y paginación.
    // Las fechas con "Z" se leen como UTC; sin "Z", como hora del servidor.
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginationResponse<RegistroAccesoDTO>>> ConsultarHistorial(
        [FromQuery] Guid? empleadoId,
        [FromQuery] DateTime? desde,
        [FromQuery] DateTime? hasta,
        [FromQuery] Guid? puntoAccesoId,
        [FromQuery] ResultadoAcceso? resultado,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = PaginationRequest.DefaultPageSize)
    {
        ConsultarHistorialAccesosQuery query = new()
        {
            EmpleadoId = empleadoId,
            Desde = desde?.ToUniversalTime(),
            Hasta = hasta?.ToUniversalTime(),
            PuntoAccesoId = puntoAccesoId,
            Resultado = resultado,
            Pagination = new PaginationRequest(pageNumber, pageSize)
        };

        return Ok(await _mediator.Send(query));
    }
}
