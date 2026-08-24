using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Abstractions.Persistence
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student, CancellationToken cancellationToken = default);
        Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
