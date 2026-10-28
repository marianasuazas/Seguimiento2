using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Accesos;

// Mapper: convierte un RegistroAcceso del dominio en el DTO que sale por la API.
internal static class RegistroAccesoMapperExtensions
{
    public static RegistroAccesoDTO ToDto(this RegistroAcceso registro)
    {
        return new RegistroAccesoDTO
        {
            Id = registro.Id,
            Codigo = registro.Codigo.Valor,
            EmpleadoId = registro.EmpleadoId,
            PuntoAccesoId = registro.PuntoAccesoId,
            Tipo = registro.Tipo.ToString(),
            Resultado = registro.Resultado.ToString(),
            Motivo = registro.Motivo?.ToString(),
            FechaHora = registro.FechaHora
        };
    }
}
