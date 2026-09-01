namespace SchoolManagement.Application.Features.Students.UpdateStudent
{
    public record UpdateStudentRequest(
        string FirstName,
        string LastName,
        string Email,
        DateOnly DateOfBirth
    );
}
