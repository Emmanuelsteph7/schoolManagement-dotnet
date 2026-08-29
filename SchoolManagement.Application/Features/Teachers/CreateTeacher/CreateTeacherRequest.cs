using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Teachers.CreateTeacher
{
    public record CreateTeacherRequest(
        string FirstName,
        string LastName,
        string Email,
        DateOnly DateOfBirth,
        DateOnly DateOfEmployment
    );
}
