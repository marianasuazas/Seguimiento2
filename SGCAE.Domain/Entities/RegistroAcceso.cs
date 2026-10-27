using SGCAE.Domain.Enums;
using SGCAE.Domain.Exceptions;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Domain.Entities;


public sealed class RegistroAcceso
{
    public Guid Id { get; private set; }


    public CodigoCredencial Codigo { get; private set; } = null!;

   
    public Guid? EmpleadoId { get; private set; }

    public Guid PuntoAccesoId { get; private set; }
    public TipoMovimiento Tipo { get; private set; }
    public ResultadoAcceso Resultado { get; private set; }

    // Solo tiene valor cuando el acceso fue rechazado.
    public MotivoRechazo? Motivo { get; private set; }

    public DateTime FechaHora { get; private set; }


    private RegistroAcceso()
    {
    }

 
    public static RegistroAcceso CrearAutorizado(CodigoCredencial codigo, Guid empleadoId,
                                                 Guid puntoAccesoId, TipoMovimiento tipo)
    {
        if (empleadoId == Guid.Empty)
        {
            throw new BusinessRuleException("El empleado es requerido en un acceso autorizado.");
        }

        return Crear(codigo, empleadoId, puntoAccesoId, tipo, ResultadoAcceso.Autorizado, null);
    }

    public static RegistroAcceso CrearRechazado(CodigoCredencial codigo, Guid? empleadoId,
                                                Guid puntoAccesoId, TipoMovimiento tipo, MotivoRechazo motivo)
    {
        if (!Enum.IsDefined(motivo))
        {
            throw new BusinessRuleException("El motivo de rechazo no es válido.");
        }

        return Crear(codigo, empleadoId, puntoAccesoId, tipo, ResultadoAcceso.Rechazado, motivo);
    }

    private static RegistroAcceso Crear(CodigoCredencial codigo, Guid? empleadoId, Guid puntoAccesoId,
                                        TipoMovimiento tipo, ResultadoAcceso resultado, MotivoRechazo? motivo)
    {
        if (puntoAccesoId == Guid.Empty)
        {
            throw new BusinessRuleException("El punto de acceso es requerido.");
        }

        if (!Enum.IsDefined(tipo))
        {
            throw new BusinessRuleException("El tipo de movimiento no es válido.");
        }

        return new RegistroAcceso
        {
            Id = Guid.CreateVersion7(),
            Codigo = codigo,
            EmpleadoId = empleadoId,
            PuntoAccesoId = puntoAccesoId,
            Tipo = tipo,
            Resultado = resultado,
            Motivo = motivo,
            FechaHora = DateTime.UtcNow
        };
    }
}