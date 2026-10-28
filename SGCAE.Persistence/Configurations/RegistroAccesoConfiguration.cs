using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGCAE.Domain.Entities;
using SGCAE.Domain.ValueObjects;

namespace SGCAE.Persistence.Configurations;

// Configuración (Fluent API): cómo se guarda un RegistroAcceso en la tabla RegistrosAcceso (el historial).
internal class RegistroAccesoConfiguration : IEntityTypeConfiguration<RegistroAcceso>
{
    public void Configure(EntityTypeBuilder<RegistroAcceso> builder)
    {
        builder.ToTable("RegistrosAcceso");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();

        // Se guarda el código leído como texto: no es una llave foránea, porque puede no existir.
        builder.Property(r => r.Codigo)
               .HasConversion(v => v.Valor, v => new CodigoCredencial(v))
               .HasMaxLength(CodigoCredencial.LongitudMaxima)
               .IsRequired();

        builder.Property(r => r.Tipo).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(r => r.Resultado).HasConversion<string>().HasMaxLength(20).IsRequired();

        // El motivo solo tiene valor en los accesos rechazados.
        builder.Property(r => r.Motivo).HasConversion<string>().HasMaxLength(50);

        builder.Property(r => r.FechaHora).IsRequired();

        // El empleado es opcional: es nulo cuando la credencial leída no existe.
        builder.HasOne<Empleado>()
               .WithMany()
               .HasForeignKey(r => r.EmpleadoId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<PuntoAcceso>()
               .WithMany()
               .HasForeignKey(r => r.PuntoAccesoId)
               .OnDelete(DeleteBehavior.Restrict);

        // Índices para que el historial y la búsqueda del último movimiento sean rápidos.
        builder.HasIndex(r => new { r.EmpleadoId, r.FechaHora });
        builder.HasIndex(r => r.FechaHora);
        builder.HasIndex(r => r.PuntoAccesoId);
    }
}
