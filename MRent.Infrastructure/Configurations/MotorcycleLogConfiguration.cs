using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRent.Domain.Entities;

namespace MRent.Infrastructure.Configurations
{
    public class MotorcycleLogConfiguration : IEntityTypeConfiguration<MotorcycleLogEntity>
    {
        public void Configure(EntityTypeBuilder<MotorcycleLogEntity> builder)
        {
            builder.ToTable("motolog");

            builder.HasKey(c => c.Id);

            builder.HasIndex(i => i.Identifier);

            builder.HasIndex(i => i.Plate)
                .IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.MotorcycleId)
                .HasColumnName("moto_id")
                .IsRequired();

            builder.Property(x => x.Identifier)
                .HasColumnName("identificador")
                .IsRequired();

            builder.Property(x => x.Year)
                .HasColumnName("ano")
                .IsRequired();

            builder.Property(x => x.Model)
                .HasColumnName("modelo")
                .IsRequired();

            builder.Property(x => x.Plate)
                .HasColumnName("placa")
                .IsRequired();
        }
    }
}
