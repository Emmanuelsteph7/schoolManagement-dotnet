using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Domain.Entities;
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
            CancellationToken cancellationToken = default
        )
        {
            var query = _dbContext
                .Teachers.AsNoTracking()
                .OrderByDescending(teacher => teacher.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}
