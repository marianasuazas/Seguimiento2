namespace SGCAE.Application.UseCases.Empleados;


public class EmpleadoDTO
{
    public Guid Id { get; init; }
    public string Identificacion { get; init; } = null!;
    public string Nombres { get; init; } = null!;
    public string Apellidos { get; init; } = null!;
    public string Correo { get; init; } = null!;
    public string Cargo { get; init; } = null!;
    public string Area { get; init; } = null!;
    public string Estado { get; init; } = null!;
}
