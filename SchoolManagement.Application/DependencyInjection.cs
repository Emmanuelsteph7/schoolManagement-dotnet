using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Features.Students.CreateStudent;
using SchoolManagement.Application.Features.Students.GetStudent;
using SchoolManagement.Application.Features.Students.GetStudents;
using SchoolManagement.Application.Features.Students.UpdateStudent;

namespace SchoolManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateStudentHandler>();
            services.AddScoped<GetStudentHandler>();
            services.AddScoped<GetStudentsHandler>();
            services.AddScoped<UpdateStudentHandler>();

            services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();

            return services;
        }
    }
}
