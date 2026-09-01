using FluentValidation;
using SchoolManagement.Api.Extensions;
using SchoolManagement.Application.Common.Pagination;
using SchoolManagement.Application.Features.Teachers.CreateTeacher;
using SchoolManagement.Application.Features.Teachers.GetTeacher;
using SchoolManagement.Application.Features.Teachers.GetTeachers;
using SchoolManagement.Application.Features.Teachers.UpdateTeacher;
using SchoolManagement.Application.Features.Teachers.UpdateTeacherStatus;

namespace SchoolManagement.Api.Endpoints
{
    public static class TeacherEndpoints
    {
        public static IEndpointRouteBuilder MapTeacherEndpoints(
            this IEndpointRouteBuilder endpoints
        )
        {
            var teachers = endpoints.MapGroup("/api/teachers").WithTags("Teachers");

            teachers
                .MapPost("/", CreateTeacher)
                .WithName("CreateTeacher")
                .WithSummary("Create a teacher")
                .WithDescription("Creates a new teacher.")
                .Produces<CreateTeacherResponse>(StatusCodes.Status201Created)
                .ProducesValidationProblem();

            teachers
                .MapGet("/{id:guid}", GetTeacher)
                .WithName("GetTeacher")
                .WithSummary("Get a teacher")
                .WithDescription("Retrieves a teacher by their unique identifier.")
                .Produces<TeacherResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            teachers
                .MapGet("/", GetTeachers)
                .WithName("GetTeachers")
                .WithSummary("Get teachers")
                .WithDescription("Returns a paginated list of teachers.")
                .Produces<PagedResult<TeacherResponse>>(StatusCodes.Status200OK)
                .ProducesValidationProblem();

            teachers
                .MapPut("/{id:guid}", UpdateTeacher)
                .WithName("UpdateTeacher")
                .WithSummary("Update a teacher")
                .WithDescription("Updates a teacher's personal details.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .ProducesValidationProblem();

            teachers
                .MapPost("/{id:guid}/verify-email", VerifyTeacherEmail)
                .WithName("VerifyTeacherEmail")
                .WithSummary("Verify a teacher's email")
                .WithDescription("Verifies the teacher's email address.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            teachers
                .MapPost("/{id:guid}/activate", ActivateTeacher)
                .WithName("ActivateTeacher")
                .WithSummary("Activate a teacher")
                .WithDescription("Activates a teacher.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            teachers
                .MapPost("/{id:guid}/deactivate", DeactivateTeacher)
                .WithName("DeactivateTeacher")
                .WithSummary("Deactivate a teacher")
                .WithDescription("Deactivates a teacher.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            teachers
                .MapPost("/{id:guid}/leave", PutTeacherOnLeave)
                .WithName("PutTeacherOnLeave")
                .WithSummary("Put a teacher on leave")
                .WithDescription("Places a teacher on leave.")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            return endpoints;
        }

        private static async Task<IResult> CreateTeacher(
            CreateTeacherRequest request,
            CreateTeacherHandler handler,
            IValidator<CreateTeacherRequest> validator,
            CancellationToken cancellationToken
        )
        {
            var validationError = await request.ValidateAsync(validator, cancellationToken);

            if (validationError is not null)
            {
                return validationError;
            }

            var teacherId = await handler.HandleAsync(request, cancellationToken);

            return Results.Created(
                $"/api/teachers/{teacherId}",
                new CreateTeacherResponse(teacherId)
            );
        }

        private static async Task<IResult> GetTeacher(
            Guid id,
            GetTeacherHandler handler,
            CancellationToken cancellationToken
        )
        {
            var request = new GetTeacherRequest(id);

            var teacher = await handler.HandleAsync(request, cancellationToken);

            if (teacher is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(teacher);
        }

        private static async Task<IResult> GetTeachers(
            [AsParameters] GetTeachersRequest request,
            GetTeachersHandler handler,
            IValidator<GetTeachersRequest> validator,
            CancellationToken cancellationToken
        )
        {
            var validationError = await request.ValidateAsync(validator, cancellationToken);

            if (validationError is not null)
            {
                return validationError;
            }

            var teachers = await handler.HandleAsync(request, cancellationToken);

            return Results.Ok(teachers);
        }

        private static async Task<IResult> UpdateTeacher(
            Guid id,
            UpdateTeacherRequest request,
            UpdateTeacherHandler handler,
            IValidator<UpdateTeacherRequest> validator,
            CancellationToken cancellationToken
        )
        {
            var validationError = await request.ValidateAsync(validator, cancellationToken);

            if (validationError is not null)
            {
                return validationError;
            }

            var updated = await handler.HandleAsync(id, request, cancellationToken);

            if (!updated)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }

        private static async Task<IResult> VerifyTeacherEmail(
            Guid id,
            UpdateTeacherStatusHandler handler,
            CancellationToken cancellationToken
        )
        {
            var updated = await handler.VerifyEmailAsync(id, cancellationToken);

            if (!updated)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }

        private static async Task<IResult> ActivateTeacher(
            Guid id,
            UpdateTeacherStatusHandler handler,
            CancellationToken cancellationToken
        )
        {
            var updated = await handler.ActivateAsync(id, cancellationToken);

            if (!updated)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }

        private static async Task<IResult> DeactivateTeacher(
            Guid id,
            UpdateTeacherStatusHandler handler,
            CancellationToken cancellationToken
        )
        {
            var updated = await handler.DeactivateAsync(id, cancellationToken);

            if (!updated)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }

        private static async Task<IResult> PutTeacherOnLeave(
            Guid id,
            UpdateTeacherStatusHandler handler,
            CancellationToken cancellationToken
        )
        {
            var updated = await handler.PutOnLeaveAsync(id, cancellationToken);

            if (!updated)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }
    };
}
