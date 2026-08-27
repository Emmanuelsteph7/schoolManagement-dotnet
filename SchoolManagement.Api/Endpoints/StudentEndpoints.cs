using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Api.Extensions;
using SchoolManagement.Application.Common.Pagination;
using SchoolManagement.Application.Features.Students.CreateStudent;
using SchoolManagement.Application.Features.Students.DeleteStudent;
using SchoolManagement.Application.Features.Students.GetStudent;
using SchoolManagement.Application.Features.Students.GetStudents;
using SchoolManagement.Application.Features.Students.UpdateStudent;

namespace SchoolManagement.Api.Endpoints;

public static class StudentEndpoints
{
    public static IEndpointRouteBuilder MapStudentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var students = endpoints.MapGroup("/api/students").WithTags("Students");

        students
            .MapPost("/", CreateStudent)
            .WithName("CreateStudent")
            .WithSummary("Create a student")
            .WithDescription("Creates a new student.")
            .Produces<CreateStudentResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        students
            .MapGet("/", GetStudents)
            .WithName("GetStudents")
            .WithSummary("Get students")
            .WithDescription("Returns a paginated list of students.")
            .Produces<PagedResult<StudentResponse>>(StatusCodes.Status200OK)
            .ProducesValidationProblem();

        students
            .MapGet("/{id:guid}", GetStudent)
            .WithName("GetStudent")
            .WithSummary("Get a student")
            .WithDescription("Retrieves a student by their unique identifier.")
            .Produces<StudentResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        students
            .MapPut("/{id:guid}", UpdateStudent)
            .WithName("UpdateStudent")
            .WithSummary("Update a student")
            .WithDescription("Updates an existing student's information.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();

        students
            .MapDelete("/{id:guid}", DeleteStudent)
            .WithName("DeleteStudent")
            .WithSummary("Delete a student")
            .WithDescription(
                "Deletes an existing student record from the system using their unique identifier."
            )
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(
                StatusCodes.Status404NotFound,
                contentType: "application/json"
            )
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        ;

        return endpoints;
    }

    private static async Task<IResult> CreateStudent(
        CreateStudentRequest request,
        CreateStudentHandler handler,
        IValidator<CreateStudentRequest> validator,
        CancellationToken cancellationToken
    )
    {
        var validationError = await request.ValidateAsync(validator, cancellationToken);

        if (validationError is not null)
        {
            return validationError;
        }

        var studentId = await handler.HandleAsync(request, cancellationToken);

        return Results.Created($"/api/students/{studentId}", new CreateStudentResponse(studentId));
    }

    private static async Task<IResult> GetStudents(
        [AsParameters] GetStudentsRequest request,
        GetStudentsHandler handler,
        IValidator<GetStudentsRequest> validator,
        CancellationToken cancellationToken
    )
    {
        var validationError = await request.ValidateAsync(validator, cancellationToken);

        if (validationError is not null)
        {
            return validationError;
        }

        var students = await handler.HandleAsync(request, cancellationToken);

        return Results.Ok(students);
    }

    private static async Task<IResult> GetStudent(
        Guid id,
        GetStudentHandler handler,
        CancellationToken cancellationToken
    )
    {
        var request = new GetStudentRequest(id);

        var student = await handler.HandleAsync(request, cancellationToken);

        if (student is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(student);
    }

    private static async Task<IResult> UpdateStudent(
        Guid id,
        UpdateStudentRequest request,
        UpdateStudentHandler handler,
        IValidator<UpdateStudentRequest> validator,
        CancellationToken cancellationToken
    )
    {
        var requestWithId = request with { Id = id };

        var validationError = await requestWithId.ValidateAsync(validator, cancellationToken);

        if (validationError is not null)
        {
            return validationError;
        }

        var updated = await handler.HandleAsync(requestWithId, cancellationToken);

        if (!updated)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }

    private static async Task<IResult> DeleteStudent(
        Guid id,
        DeleteStudentHandler handler,
        CancellationToken cancellationToken
    )
    {
        var deleted = await handler.HandleAsync(id, cancellationToken);

        if (!deleted)
        {
            return Results.NotFound(
                new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Student Not Found",
                    Detail = $"Student with ID '{id}' does not exist.",
                }
            );
        }

        return Results.NoContent();
    }
}
