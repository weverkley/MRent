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

            builder.HasData(
                new PlanEntity
                {
                    Id = Guid.Parse("96234321-504d-47a3-ad27-f20ec91c9036"),
                    Days = 7,
                    DailyValue = 30,
                    ReturnFeePercent = 20,
                    DailyExceededEndDateFee = 50
                },
                new PlanEntity
                {
                    Id = Guid.Parse("6307d574-0979-4f1c-8761-a3425b4c955c"),
                    Days = 15,
                    DailyValue = 28,
                    ReturnFeePercent = 40,
                    DailyExceededEndDateFee = 50
                },
                new PlanEntity
                {
                    Id = Guid.Parse("466d0330-70b4-47b5-ae99-d3f62d40bd20"),
                    Days = 30,
                    DailyValue = 22,
                    ReturnFeePercent = 0,
                    DailyExceededEndDateFee = 50
                },
                new PlanEntity
                {
                    Id = Guid.Parse("88924edf-91f6-4130-b54a-b51dc796da93"),
                    Days = 45,
                    DailyValue = 20,
                    ReturnFeePercent = 0,
                    DailyExceededEndDateFee = 50
                },
                new PlanEntity
                {
                    Id = Guid.Parse("d0840305-d467-44ac-ac24-e4e791a58ed3"),
                    Days = 50,
                    DailyValue = 18,
                    ReturnFeePercent = 0,
                    DailyExceededEndDateFee = 50
                }
            );
        }
    }
}
