using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Application.Features.Students.CreateStudent;
using SchoolManagement.Application.Features.Students.GetStudent;
using SchoolManagement.Application.Features.Students.GetStudents;
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
builder.Services.AddScoped<GetStudentHandler>();
builder.Services.AddScoped<GetStudentsHandler>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();

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
        IValidator<CreateStudentRequest> validator,
        CancellationToken cancellationToken
    ) =>
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var studentId = await handler.HandleAsync(request, cancellationToken);

        return Results.Created($"/api/students/{studentId}", new { id = studentId });
    }
);

app.MapGet(
    "/api/students/{id:guid}",
    async (Guid id, GetStudentHandler handler, CancellationToken cancellationToken) =>
    {
        var request = new GetStudentRequest(id);

        var student = await handler.HandleAsync(request, cancellationToken);

        if (student is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(student);
    }
);

app.MapGet(
    "/api/students",
    async (GetStudentsHandler handler, CancellationToken cancellationToken) =>
    {
        var students = await handler.HandleAsync(cancellationToken);

        return Results.Ok(students);
    }
);

app.Run();
