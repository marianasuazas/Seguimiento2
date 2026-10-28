using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGCAE.Domain.Entities;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Persistence.Configurations;

// Configuración (Fluent API): cómo se guarda un Empleado en la tabla Empleados.
internal class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {
        builder.ToTable("Empleados");

        builder.HasKey(e => e.Id);

        // El Id lo genera el dominio, no la base de datos.
        builder.Property(e => e.Id).ValueGeneratedNever();

        // Los value objects se guardan como texto en una sola columna.
        builder.Property(e => e.Identificacion)
               .HasConversion(v => v.Valor, v => new Identificacion(v))
               .HasMaxLength(Identificacion.LongitudMaxima)
               .IsRequired();

        // Un índice único impide dos empleados con la misma identificación, incluso si dos registros llegan a la vez.
        builder.HasIndex(e => e.Identificacion).IsUnique();

        builder.Property(e => e.Nombres).HasMaxLength(Empleado.LongitudMaximaTexto).IsRequired();
        builder.Property(e => e.Apellidos).HasMaxLength(Empleado.LongitudMaximaTexto).IsRequired();

        builder.Property(e => e.Correo)
               .HasConversion(v => v.Valor, v => new CorreoElectronico(v))
               .HasMaxLength(CorreoElectronico.LongitudMaxima)
               .IsRequired();

        builder.Property(e => e.Cargo).HasMaxLength(Empleado.LongitudMaximaTexto).IsRequired();
        builder.Property(e => e.Area).HasMaxLength(Empleado.LongitudMaximaTexto).IsRequired();

        // La enumeración se guarda como texto ("Activo", "Inactivo") para que la tabla sea legible.
        builder.Property(e => e.Estado).HasConversion<string>().HasMaxLength(20).IsRequired();
    }
}
