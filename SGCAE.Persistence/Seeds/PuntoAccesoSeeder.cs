using Microsoft.EntityFrameworkCore;
using SGCAE.Domain.Entities;

namespace SGCAE.Persistence.Seeds;

// Datos de ejemplo: puntos de acceso. Solo se cargan si la tabla está vacía.
public class PuntoAccesoSeeder : IDataSeeder
{
    private readonly SGCAEDbContext _context;

    public PuntoAccesoSeeder(SGCAEDbContext context)
    {
        _context = context;
    }

    public int Order => 1;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.PuntosAcceso.AnyAsync(cancellationToken))
        {
            return;
        }

        PuntoAcceso areaRestringida = new("Área restringida de servidores", "Cuarto técnico de acceso limitado");

        // Un punto deshabilitado, para mostrar que rechaza todos los accesos.
        areaRestringida.Deshabilitar();

        List<PuntoAcceso> puntos =
        [
            new("Recepción principal", "Ingreso general de la sede"),
            new("Parqueadero", "Entrada de vehículos y personal"),
            new("Bodega de carga", "Zona de almacenamiento y despacho"),
            areaRestringida
        ];

        await _context.PuntosAcceso.AddRangeAsync(puntos, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
