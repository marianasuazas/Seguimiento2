using Microsoft.EntityFrameworkCore;
using SGCAE.Domain.Entities;

namespace SGCAE.Persistence;

// El DbContext es la "puerta" a la base de datos: cada DbSet es una tabla.
public class SGCAEDbContext : DbContext
{
    public SGCAEDbContext(DbContextOptions<SGCAEDbContext> options) : base(options)
    {
    }

    public DbSet<Empleado> Empleados => Set<Empleado>();
    public DbSet<Credencial> Credenciales => Set<Credencial>();
    public DbSet<PuntoAcceso> PuntosAcceso => Set<PuntoAcceso>();
    public DbSet<RegistroAcceso> RegistrosAcceso => Set<RegistroAcceso>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Carga todas las configuraciones de la carpeta Configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SGCAEDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
