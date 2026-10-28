using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SGCAE.Application.Exceptions;
using SGCAE.Domain.Exceptions;

namespace SGCAE.Api.Middleware;

// Manejo de errores en un solo lugar: convierte cada excepción en la respuesta HTTP que corresponde.
// Así los controladores no necesitan try/catch.
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
                                                CancellationToken cancellationToken)
    {
        (int status, string mensaje) = exception switch
        {
            // Una regla de negocio incumplida: error del cliente.
            BusinessRuleException e => (StatusCodes.Status400BadRequest, e.Message),

            // Algo que se buscó por id y no existe.
            NotFoundException e => (StatusCodes.Status404NotFound, e.Message),

            // La base de datos rechazó el cambio (por ejemplo, un índice único): conflicto.
            DbUpdateException => (StatusCodes.Status409Conflict, "El registro entra en conflicto con otro existente."),

            // Cualquier otra cosa es un error inesperado del servidor.
            _ => (StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado.")
        };

        if (status == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Error no controlado");
        }

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new { error = mensaje }, cancellationToken);

        return true;
    }
}