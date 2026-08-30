using FluentValidation;
using SchoolManagement.Api.Extensions;
using SchoolManagement.Application.Features.Teachers.CreateTeacher;
using SchoolManagement.Application.Features.Teachers.GetTeacher;

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
    };
}
