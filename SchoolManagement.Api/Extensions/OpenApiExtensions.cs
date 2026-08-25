using Scalar.AspNetCore;

namespace SchoolManagement.Api.Extensions
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            services.AddOpenApi();

            return services;
        }

        public static WebApplication UseApiDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            return app;
        }
    }
}
