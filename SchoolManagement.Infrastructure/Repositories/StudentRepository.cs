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
    }
}
