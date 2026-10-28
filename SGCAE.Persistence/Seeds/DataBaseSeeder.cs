using Microsoft.Extensions.DependencyInjection;

namespace SGCAE.Persistence.Seeds;

// Orquestador: ejecuta todos los sembradores registrados, en el orden indicado por su propiedad Order.
public static class DataBaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using IServiceScope scope = services.CreateScope();

        IEnumerable<IDataSeeder> seeders = scope.ServiceProvider
            .GetServices<IDataSeeder>()
            .OrderBy(s => s.Order);

        foreach (IDataSeeder seeder in seeders)
        {
            await seeder.SeedAsync(cancellationToken);
        }
    }
}
