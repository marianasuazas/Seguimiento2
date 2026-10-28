namespace SGCAE.Application.UseCases.PuntosAcceso;

// DTO: define qué datos de un punto de acceso salen por la API.
public class PuntoAccesoDTO
{
    public Guid Id { get; init; }
    public string Nombre { get; init; } = null!;
    public string? Descripcion { get; init; }
    public bool Habilitado { get; init; }
}
