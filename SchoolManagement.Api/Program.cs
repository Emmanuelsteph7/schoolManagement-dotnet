using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Application.Features.Students.CreateStudent;
using SchoolManagement.Infrastructure.Persistence;
using SchoolManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

/*
    DB connection setup
*/
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SchoolManagementDbContext>(options =>
    options.UseNpgsql(connectionString)
);

/*
    Whenever something requests IStudentRepository, provide StudentRepository.
*/
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<CreateStudentHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost(
    "/api/students",
    async (
        CreateStudentRequest request,
        CreateStudentHandler handler,
        CancellationToken cancellationToken
    ) =>
    {
        var studentId = await handler.HandleAsync(request, cancellationToken);

        return Results.Created($"/api/students/{studentId}", new { id = studentId });
    }
);

app.Run();
