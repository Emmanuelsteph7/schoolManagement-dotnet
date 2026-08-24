namespace SchoolManagement.Application.Features.Students.GetStudent
{
    public record StudentResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        DateOnly DateOfBirth
    );
}
