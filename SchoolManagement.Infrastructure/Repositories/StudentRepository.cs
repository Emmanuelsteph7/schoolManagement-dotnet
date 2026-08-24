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

        public async Task<IReadOnlyList<Student>> GetAllAsync(
            CancellationToken cancellationToken = default
        )
        {
            /*
                This is an important EF Core concept.
                For a GET request, we're only reading the students. We aren't going to modify them.
                Normally EF Core tracks entities it retrieves
                
                AsNoTracking means get all students from the database without tracking them.
            */
            return await _dbContext.Students.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
