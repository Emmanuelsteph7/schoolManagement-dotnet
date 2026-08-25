using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolManagementDbContext _dbContext;

        public StudentRepository(SchoolManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
        {
            await _dbContext.Students.AddAsync(student, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Student?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            return await _dbContext.Students.FirstOrDefaultAsync(
                student => student.Id == id,
                cancellationToken
            );
        }

        public async Task<(IReadOnlyList<Student> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default
        )
        {
            var query = _dbContext
                .Students.AsNoTracking()
                .OrderBy(student => student.LastName)
                .ThenBy(student => student.FirstName);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task UpdateAsync(
            Student student,
            CancellationToken cancellationToken = default
        )
        {
            _dbContext.Students.Update(student);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
