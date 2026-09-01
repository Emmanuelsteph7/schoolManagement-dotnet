using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolManagementDbContext _dbContext;

        public TeacherRepository(SchoolManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            await _dbContext.Teachers.AddAsync(teacher, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Teacher?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            return await _dbContext
                .Teachers.AsNoTracking()
                .FirstOrDefaultAsync(teacher => teacher.Id == id, cancellationToken);
        }

        public async Task<(IReadOnlyList<Teacher> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            TeacherSortField sortBy,
            SortDirection sortDirection,
            string? search,
            EmploymentStatus? employmentStatus,
            CancellationToken cancellationToken = default
        )
        {
            var query = _dbContext.Teachers.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(teacher =>
                    teacher.FirstName.ToLowerInvariant().Contains(search.ToLowerInvariant())
                    || teacher.LastName.ToLowerInvariant().Contains(search.ToLowerInvariant())
                    || teacher.Email.ToLowerInvariant().Contains(search.ToLowerInvariant())
                );
            }

            if (employmentStatus.HasValue)
            {
                query = query.Where(teacher => teacher.EmploymentStatus == employmentStatus.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            query = sortBy switch
            {
                TeacherSortField.FirstName => sortDirection == SortDirection.Asc
                    ? query.OrderBy(t => t.FirstName)
                    : query.OrderByDescending(t => t.FirstName),

                TeacherSortField.LastName => sortDirection == SortDirection.Asc
                    ? query.OrderBy(t => t.LastName)
                    : query.OrderByDescending(t => t.LastName),

                TeacherSortField.DateOfEmployment => sortDirection == SortDirection.Asc
                    ? query.OrderBy(t => t.DateOfEmployment)
                    : query.OrderByDescending(t => t.DateOfEmployment),

                TeacherSortField.CreatedAt => sortDirection == SortDirection.Asc
                    ? query.OrderBy(t => t.CreatedAt)
                    : query.OrderByDescending(t => t.CreatedAt),

                _ => query.OrderByDescending(t => t.CreatedAt),
            };

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}
