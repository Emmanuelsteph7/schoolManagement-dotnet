namespace SchoolManagement.Application.Features.Students.UpdateStudent
{
    public record UpdateStudentRequest(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        DateOnly DateOfBirth
    );
}
