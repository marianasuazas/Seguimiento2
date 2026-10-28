namespace SGCAE.Application.UseCases.Credenciales;

// DTO: define qué datos de una credencial salen por la API, sin exponer la entidad del dominio.
public class CredencialDTO
{
    public Guid Id { get; init; }
    public string Codigo { get; init; } = null!;
    public Guid EmpleadoId { get; init; }
    public string Estado { get; init; } = null!;
    public DateTime FechaAsignacion { get; init; }
}
