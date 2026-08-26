using SchoolManagement.Application.Abstractions.Persistence;

namespace SchoolManagement.Application.Features.Students.DeleteStudent
{
    public class DeleteStudentHandler
    {
        private readonly IStudentRepository _studentRepository;

        public DeleteStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _studentRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
