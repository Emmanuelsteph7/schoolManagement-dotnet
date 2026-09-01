using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.Abstractions.Persistence
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default);
        Task<Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<(IReadOnlyList<Teacher> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            TeacherSortField SortBy = TeacherSortField.CreatedAt,
            SortDirection SortDirection = SortDirection.Desc,
            string? search = null,
            EmploymentStatus? employmentStatus = null,
            EmailAccountStatus? emailAccountStatus = null,
            CancellationToken cancellationToken = default
        );
        Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default);
    }
}
