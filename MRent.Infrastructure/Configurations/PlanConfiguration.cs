using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRent.Domain.Entities;

namespace MRent.Infrastructure.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<PlanEntity>
    {
        public void Configure(EntityTypeBuilder<PlanEntity> builder)
        {
            builder.ToTable("plano");

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.Days)
                .HasColumnName("dias")
                .IsRequired();

            builder.Property(x => x.DailyValue)
                .HasColumnName("valor_diaria")
                .IsRequired();

            builder.Property(x => x.ReturnFeePercent)
                .HasColumnName("porcentagem_taxa_retorno")
                .IsRequired();

            builder.Property(x => x.DailyExceededEndDateFee)
                .HasColumnName("taxa_dia_excedido")
                .IsRequired();
        }
    }
}
