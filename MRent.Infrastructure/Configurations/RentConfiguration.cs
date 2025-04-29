using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRent.Domain.Entities;

namespace MRent.Infrastructure.Configurations
{
    public class RentConfiguration : IEntityTypeConfiguration<RentEntity>
    {
        public void Configure(EntityTypeBuilder<RentEntity> builder)
        {
            builder.ToTable("locacao");

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.CourierId)
                .HasColumnName("entregador_id")
                .IsRequired();

            builder.Property(x => x.MotorcycleId)
                .HasColumnName("moto_id")
                .IsRequired();

            builder.Property(x => x.PlanId)
                .HasColumnName("plano_id")
                .IsRequired();

            builder.Property(x => x.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_inicio")
                .IsRequired();

            builder.Property(x => x.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_termino")
                .IsRequired();

            builder.Property(x => x.ExpectedEndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_previsao_termino")
                .IsRequired();

            builder.Property(x => x.ReturnDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_devolucao")
                .IsRequired(false);

            builder.Property(x => x.Tax)
                .HasColumnName("taxas")
                .IsRequired();

            builder.Property(x => x.Subtotal)
                .HasColumnName("subtotal")
                .IsRequired();

            builder.Property(x => x.Total)
                .HasColumnName("total")
                .IsRequired();

            builder.HasOne(h => h.Plan)
                .WithMany(w => w.Rents)
                .HasForeignKey(f => f.PlanId)
                .IsRequired();
        }
    }
}
