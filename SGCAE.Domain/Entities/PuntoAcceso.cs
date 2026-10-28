using SGCAE.Domain.Exceptions;

namespace SGCAE.Domain.Entities;


public sealed class PuntoAcceso
{
    public const int LongitudMaximaNombre = 100;
    public const int LongitudMaximaDescripcion = 200;

    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string? Descripcion { get; private set; }
    public bool Habilitado { get; private set; }


    private PuntoAcceso()
    {
    }

    public PuntoAcceso(string nombre, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new BusinessRuleException("El nombre del punto de acceso es requerido.");
        }

        nombre = nombre.Trim();

        if (nombre.Length > LongitudMaximaNombre)
        {
            throw new BusinessRuleException($"El nombre del punto de acceso no puede superar los {LongitudMaximaNombre} caracteres.");
        }

        Id = Guid.CreateVersion7();
        Nombre = nombre;
        Descripcion = ValidarDescripcion(descripcion);
        Habilitado = true;
    }

    public void Habilitar()
    {
        if (Habilitado)
        {
            throw new BusinessRuleException("El punto de acceso ya está habilitado.");
        }

        Habilitado = true;
    }

    public void Deshabilitar()
    {
        if (!Habilitado)
        {
            throw new BusinessRuleException("El punto de acceso ya está deshabilitado.");
        }

        Habilitado = false;
    }

    private static string? ValidarDescripcion(string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(descripcion))
        {
            return null;
        }

        descripcion = descripcion.Trim();

        if (descripcion.Length > LongitudMaximaDescripcion)
        {
            throw new BusinessRuleException($"La descripción no puede superar los {LongitudMaximaDescripcion} caracteres.");
        }

        return descripcion;
    }
}