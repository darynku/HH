using HH.Domain.Entitties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HH.Infrastructure.Configurations
{
    public class VacancyApplicationConfiguration : IEntityTypeConfiguration<VacancyApplication>
    {
        public void Configure(EntityTypeBuilder<VacancyApplication> builder)
        {
            builder.ToTable(nameof(VacancyApplication));
            builder.HasKey(x => x.Id);

            builder.Property(v => v.ApplyTime).IsRequired();
            builder.Property(v => v.Recomendation).IsRequired();

            builder.HasOne(v => v.User).WithOne();
            builder.HasOne(v => v.Vacancy).WithOne();
        }
    }
}
