using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Features.Students.CreateStudent;
using SchoolManagement.Application.Features.Students.DeleteStudent;
using SchoolManagement.Application.Features.Students.GetStudent;
using SchoolManagement.Application.Features.Students.GetStudents;
using SchoolManagement.Application.Features.Students.UpdateStudent;
using SchoolManagement.Application.Features.Teachers.CreateTeacher;
using SchoolManagement.Application.Features.Teachers.GetTeacher;
using SchoolManagement.Application.Features.Teachers.GetTeachers;
using SchoolManagement.Application.Features.Teachers.UpdateTeacher;

namespace SchoolManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            /* Students */
            services.AddScoped<CreateStudentHandler>();
            services.AddScoped<GetStudentHandler>();
            services.AddScoped<GetStudentsHandler>();
            services.AddScoped<UpdateStudentHandler>();
            services.AddScoped<DeleteStudentHandler>();

            /* Teachers */
            services.AddScoped<CreateTeacherHandler>();
            services.AddScoped<GetTeacherHandler>();
            services.AddScoped<GetTeachersHandler>();
            services.AddScoped<UpdateTeacherHandler>();

            services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateTeacherValidator>();

            return services;
        }
    }
}
