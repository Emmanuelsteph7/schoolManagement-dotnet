using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagement.Infrastructure.Repositories;

namespace SchoolManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            /*
                DB connection setup
            */
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SchoolManagementDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            /*
                Whenever something requests IStudentRepository, provide StudentRepository.
            */
            services.AddScoped<IStudentRepository, StudentRepository>();

            return services;
        }
    }
}
