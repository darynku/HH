using HH.Domain.Entitties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HH.Infrastructure.Configurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.ToTable("Vacancies");

            builder.HasKey(v => v.Id);

            // Обязательные поля
            builder.Property(v => v.Title).IsRequired().HasMaxLength(255);
            builder.Property(v => v.Description).IsRequired();

            // Конфигурация для SalaryRange
            builder.ComplexProperty(v => v.Salary, salary =>
            {
                salary.Property(s => s.MinSalary)
                      .HasColumnName("min_salary")
                      .IsRequired();
                salary.Property(s => s.MaxSalary)
                      .HasColumnName("max_salary")
                      .IsRequired();
            });

            // Дополнительные поля
            builder.Property(v => v.PostedDate).IsRequired();
            builder.Property(v => v.ExpirationDate).IsRequired(); // Срок истекания

            builder.Property(v => v.Region)
                   .HasMaxLength(255) // Указываем максимальную длину строки для региона
                   .IsRequired(); // Регион

            builder.Property(v => v.Position)
                   .HasMaxLength(255) // Должность
                   .IsRequired();

            builder.Property(v => v.WorkExperience) // Опыт работы (диапазон)
                   .HasMaxLength(10) // Ограничение на строку диапазона
                   .IsRequired();

            builder.Property(v => v.Views) // Просмотры
                   .HasDefaultValue(0) // По умолчанию 0
                   .IsRequired();

            builder.Property(v => v.ApplicationsCount) // Количество откликов
                   .HasDefaultValue(0) // По умолчанию 0
                   .IsRequired();

            builder.Property(v => v.ItemFile)
                   .IsRequired(false); // Файл не обязателен

            // Связь с пользователем (владельцем вакансии)
            builder.HasOne(v => v.User)
                   .WithMany(u => u.Vacancies)
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
