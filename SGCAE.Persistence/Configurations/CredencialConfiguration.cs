using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGCAE.Domain.Entities;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Persistence.Configurations;

// Configuración (Fluent API): cómo se guarda una Credencial en la tabla Credenciales.
internal class CredencialConfiguration : IEntityTypeConfiguration<Credencial>
{
    public void Configure(EntityTypeBuilder<Credencial> builder)
    {
        builder.ToTable("Credenciales");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(c => c.Codigo)
               .HasConversion(v => v.Valor, v => new CodigoCredencial(v))
               .HasMaxLength(CodigoCredencial.LongitudMaxima)
               .IsRequired();

        // El código de una credencial es único en todo el sistema.
        builder.HasIndex(c => c.Codigo).IsUnique();

        builder.Property(c => c.Estado).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(c => c.FechaAsignacion).IsRequired();

        // Cada credencial pertenece a un empleado. Restrict: no se puede borrar un empleado que tenga credenciales.
        builder.HasOne<Empleado>()
               .WithMany()
               .HasForeignKey(c => c.EmpleadoId)
               .OnDelete(DeleteBehavior.Restrict);

        // Acelera la búsqueda de credenciales de un empleado según su estado (por ejemplo, la vigente).
        builder.HasIndex(c => new { c.EmpleadoId, c.Estado });
    }
}
