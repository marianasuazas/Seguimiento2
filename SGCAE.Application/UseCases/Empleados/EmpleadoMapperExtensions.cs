using SGCAE.Domain.Entities;

namespace SGCAE.Application.UseCases.Empleados;

// Mapper: convierte un Empleado del dominio en el DTO que sale por la API.
// Se usa como empleado.ToDto(). "internal" = solo se ve dentro de Application.
internal static class EmpleadoMapperExtensions
{
    public static EmpleadoDTO ToDto(this Empleado empleado)
    {
        return new EmpleadoDTO
        {
            Id = empleado.Id,
            Identificacion = empleado.Identificacion.Valor,   // De los value objects se toma solo el texto
            Nombres = empleado.Nombres,
            Apellidos = empleado.Apellidos,
            Correo = empleado.Correo.Valor,
            Cargo = empleado.Cargo,
            Area = empleado.Area,
            Estado = empleado.Estado.ToString()               // El estado sale como texto: "Activo" o "Inactivo"
        };
    }
}