using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRent.Domain.Entities;

namespace MRent.Infrastructure.Configurations
{
    public class MotorcycleConfiguration : IEntityTypeConfiguration<MotorcycleEntity>
    {
        public void Configure(EntityTypeBuilder<MotorcycleEntity> builder)
        {
            builder.ToTable("moto");

            builder.HasKey(c => c.Id);

            builder.HasIndex(i => i.Identifier);

            builder.HasIndex(i => i.Plate)
                .IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("id")
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

            builder.HasMany(h => h.Rents)
                .WithOne(w => w.Motorcycle)
                .HasForeignKey(f => f.MotorcycleId)
                .IsRequired();
        }
    }
}
