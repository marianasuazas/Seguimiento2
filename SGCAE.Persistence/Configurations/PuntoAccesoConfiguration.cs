using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGCAE.Domain.Entities;

namespace SGCAE.Persistence.Configurations;

// Configuración (Fluent API): cómo se guarda un PuntoAcceso en la tabla PuntosAcceso.
internal class PuntoAccesoConfiguration : IEntityTypeConfiguration<PuntoAcceso>
{
    public void Configure(EntityTypeBuilder<PuntoAcceso> builder)
    {
        builder.ToTable("PuntosAcceso");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.Property(p => p.Nombre).HasMaxLength(PuntoAcceso.LongitudMaximaNombre).IsRequired();
        builder.HasIndex(p => p.Nombre).IsUnique();

        builder.Property(p => p.Descripcion).HasMaxLength(PuntoAcceso.LongitudMaximaDescripcion);
        builder.Property(p => p.Habilitado).IsRequired();
    }
}
