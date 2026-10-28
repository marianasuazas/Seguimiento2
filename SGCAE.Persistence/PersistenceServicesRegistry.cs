using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGCAE.Application.Contracts;
using SGCAE.Application.Contracts.Repositories;
using SGCAE.Persistence.Repositories;
using SGCAE.Persistence.Seeds;

namespace SGCAE.Persistence;

// Registra en el contenedor de dependencias todo lo que necesita la capa Persistence.
public static class PersistenceServicesRegistry
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SGCAEDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SGCAEDb"));
        });

        // Cada interfaz de Application se conecta con su implementación de Entity Framework.
        services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>();
        services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
        services.AddScoped<ICredencialRepository, CredencialRepository>();
        services.AddScoped<IPuntoAccesoRepository, PuntoAccesoRepository>();
        services.AddScoped<IRegistroAccesoRepository, RegistroAccesoRepository>();

        // Datos iniciales de ejemplo.
        services.AddScoped<IDataSeeder, EmpleadoSeeder>();
        services.AddScoped<IDataSeeder, PuntoAccesoSeeder>();
        services.AddScoped<IDataSeeder, CredencialSeeder>();

        return services;
    }
}
