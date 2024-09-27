using HH.Domain.Entitties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace HH.Infrastructure.Database
{
    public class ApplicationDbContext(IConfiguration configuration) : DbContext
    {
        private readonly IConfiguration _configuration = configuration;

        public DbSet<Vacancy> Vacancy => Set<Vacancy>();
        public DbSet<VacancyApplication> VacancyApplication => Set<VacancyApplication>();

        public DbSet<User> Users => Set<User>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
        }
    } 
    
}
