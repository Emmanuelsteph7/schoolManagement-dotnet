using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Abstractions.Persistence
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student, CancellationToken cancellationToken = default);
    }
}
