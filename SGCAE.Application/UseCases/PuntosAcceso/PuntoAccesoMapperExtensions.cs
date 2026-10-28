using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.PuntosAcceso;

// Mapper: convierte un PuntoAcceso del dominio en el DTO que sale por la API.
internal static class PuntoAccesoMapperExtensions
{
    public static PuntoAccesoDTO ToDto(this PuntoAcceso punto)
    {
        return new PuntoAccesoDTO
        {
            Id = punto.Id,
            Nombre = punto.Nombre,
            Descripcion = punto.Descripcion,
            Habilitado = punto.Habilitado
        };
    }
}
