using Microsoft.EntityFrameworkCore;
using SGCAE.Domain.Entities;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Persistence.Seeds;

// Datos de ejemplo: una credencial por empleado. Va después de los empleados (Order 2).
public class CredencialSeeder : IDataSeeder
{
    private readonly SGCAEDbContext _context;

    public CredencialSeeder(SGCAEDbContext context)
    {
        _context = context;
    }

    public int Order => 2;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.Credenciales.AnyAsync(cancellationToken))
        {
            return;
        }

        // Cada credencial se busca por la identificación de su empleado.
        Credencial laura = await CrearAsync("1001", "LAURA-001", cancellationToken);
        Credencial carlos = await CrearAsync("1002", "CARLOS-001", cancellationToken);
        Credencial diana = await CrearAsync("1003", "DIANA-001", cancellationToken);
        Credencial andres = await CrearAsync("1004", "ANDRES-001", cancellationToken);
        Credencial marcela = await CrearAsync("1005", "MARCELA-001", cancellationToken);

        // Una credencial bloqueada, para mostrar que no sirve para ingresar.
        andres.Bloquear();

        await _context.Credenciales.AddRangeAsync([laura, carlos, diana, andres, marcela], cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Credencial> CrearAsync(string identificacion, string codigo, CancellationToken cancellationToken)
    {
        Identificacion id = new(identificacion);

        Empleado empleado = await _context.Empleados.FirstAsync(e => e.Identificacion == id, cancellationToken);

        return new Credencial(empleado.Id, new CodigoCredencial(codigo));
    }
}
