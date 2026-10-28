using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SGCAE.Persistence;

// Solo lo usan las herramientas de migraciones (dotnet ef) para crear el DbContext sin arrancar la API.
// La API real toma la cadena de conexión de appsettings.json.
public class SGCAEDbContextFactory : IDesignTimeDbContextFactory<SGCAEDbContext>
{
    public SGCAEDbContext CreateDbContext(string[] args)
    {
        string connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__SGCAEDb")
            ?? @"Server=(localdb)\MSSQLLocalDB;Database=SGCAEDb;Trusted_Connection=True;TrustServerCertificate=True";

        DbContextOptions<SGCAEDbContext> options = new DbContextOptionsBuilder<SGCAEDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new SGCAEDbContext(options);
    }
}
