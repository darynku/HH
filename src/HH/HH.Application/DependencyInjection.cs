using HH.Application.Features.Users;
using HH.Application.Features.Vacancies;
using HH.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HH.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVacancyService, VacancyService>();

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
