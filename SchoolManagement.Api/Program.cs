using SchoolManagement.Api.Endpoints;
using SchoolManagement.Api.Extensions;
using SchoolManagement.Application;
using SchoolManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddApiDocumentation();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseApiDocumentation();

app.UseHttpsRedirection();

app.MapStudentEndpoints();

app.Run();
