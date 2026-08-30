using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Teachers.GetTeacher
{
    public record TeacherResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        DateOnly DateOfBirth,
        DateOnly DateOfEmployment,
        EmploymentStatus EmploymentStatus,
        EmailAccountStatus EmailAccountStatus,
        DateTimeOffset? EmailAccountVerifiedDate,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt
    );
}
