using Microsoft.EntityFrameworkCore;
using SGCAE.Domain.Entities;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Persistence.Seeds;

// Datos de ejemplo: empleados. Solo se cargan si la tabla está vacía.
public class EmpleadoSeeder : IDataSeeder
{
    private readonly SGCAEDbContext _context;

    public EmpleadoSeeder(SGCAEDbContext context)
    {
        _context = context;
    }

    public int Order => 1;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.Empleados.AnyAsync(cancellationToken))
        {
            return;
        }

        Empleado marcela = new(new Identificacion("1005"), "Marcela", "Ríos Soto",
                               new CorreoElectronico("marcela.rios@sgcae.com"),
                               "Coordinadora administrativa", "Administración");

        // Un empleado inactivo, para mostrar que no puede ingresar.
        marcela.Desactivar();

        List<Empleado> empleados =
        [
            new(new Identificacion("1001"), "Laura", "Gómez Rojas",
                new CorreoElectronico("laura.gomez@sgcae.com"), "Analista de logística", "Operaciones"),
            new(new Identificacion("1002"), "Carlos", "Pérez Ruiz",
                new CorreoElectronico("carlos.perez@sgcae.com"), "Supervisor de bodega", "Almacenamiento"),
            new(new Identificacion("1003"), "Diana", "Martínez León",
                new CorreoElectronico("diana.martinez@sgcae.com"), "Auxiliar de seguridad", "Seguridad"),
            new(new Identificacion("1004"), "Andrés", "Torres Vega",
                new CorreoElectronico("andres.torres@sgcae.com"), "Operario de montacargas", "Almacenamiento"),
            marcela
        ];

        await _context.Empleados.AddRangeAsync(empleados, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
