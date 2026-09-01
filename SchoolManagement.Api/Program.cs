using System.Text.Json.Serialization;
using SchoolManagement.Api.Endpoints;
using SchoolManagement.Api.ExceptionHandling;
using SchoolManagement.Api.Extensions;
using SchoolManagement.Application;
using SchoolManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddApiDocumentation();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

/*
    This allows ASP.NET Core to serialize enums as strings.
    So, we can pass the string values, instead of the enum int. which is more readable
*/
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

app.UseApiDocumentation();

app.UseHttpsRedirection();

app.MapStudentEndpoints();
app.MapTeacherEndpoints();

app.Run();
