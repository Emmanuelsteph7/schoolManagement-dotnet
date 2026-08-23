namespace SchoolManagement.Application.Features.Students.CreateStudent
{
    public record CreateStudentRequest(
        string FirstName,
        string LastName,
        string Email,
        DateOnly DateOfBirth
    );
}
