using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRent.Domain.Entities;

namespace MRent.Infrastructure.Configurations
{
    public class CourierConfiguration : IEntityTypeConfiguration<CourierEntity>
    {
        public void Configure(EntityTypeBuilder<CourierEntity> builder)
        {
            builder.ToTable("entregador");

            builder.HasKey(c => c.Id);

            builder.HasIndex(i => i.CNPJ)
                .IsUnique();

            builder.HasIndex(i => i.CNH)
                .IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("nome")
                .IsRequired();

            builder.Property(x => x.CNPJ)
                .HasColumnName("cnpj")
                .IsRequired();

            builder.Property(x => x.BornDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_nascimento")
                .IsRequired();

            builder.Property(x => x.CNH)
                .HasColumnName("numero_cnh")
                .IsRequired();

            builder.Property(x => x.CNHType)
                .HasColumnName("tipo_cnh")
                .IsRequired();

            builder.Property(x => x.CNHImage)
                .HasColumnName("imagem_cnh")
                .IsRequired(false);

            builder.HasMany(h => h.Rents)
                .WithOne(w => w.Courier)
                .HasForeignKey(f => f.CourierId)
                .IsRequired();
        }
    }
}
