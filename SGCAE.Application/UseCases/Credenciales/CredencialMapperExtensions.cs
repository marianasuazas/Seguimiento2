using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Credenciales;

// Mapper: convierte una Credencial del dominio en el DTO que sale por la API.
internal static class CredencialMapperExtensions
{
    public static CredencialDTO ToDto(this Credencial credencial)
    {
        return new CredencialDTO
        {
            Id = credencial.Id,
            Codigo = credencial.Codigo.Valor,
            EmpleadoId = credencial.EmpleadoId,
            Estado = credencial.Estado.ToString(),
            FechaAsignacion = credencial.FechaAsignacion
        };
    }
}
