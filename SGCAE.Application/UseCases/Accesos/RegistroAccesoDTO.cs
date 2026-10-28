namespace SGCAE.Application.UseCases.Accesos;

// DTO: define qué datos de un registro de acceso salen por la API.
public class RegistroAccesoDTO
{
    public Guid Id { get; init; }
    public string Codigo { get; init; } = null!;
    public Guid? EmpleadoId { get; init; }
    public Guid PuntoAccesoId { get; init; }
    public string Tipo { get; init; } = null!;
    public string Resultado { get; init; } = null!;
    public string? Motivo { get; init; }
    public DateTime FechaHora { get; init; }
}
