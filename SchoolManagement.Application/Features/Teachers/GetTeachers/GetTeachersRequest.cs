using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Features.Teachers.GetTeachers
{
    public record GetTeachersRequest(
        int Page = 1,
        int PageSize = 20,
        TeacherSortField SortBy = TeacherSortField.CreatedAt,
        SortDirection SortDirection = SortDirection.Desc,
        string? Search = null,
        EmploymentStatus? EmploymentStatus = null
    );
}
