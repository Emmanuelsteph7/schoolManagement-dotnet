using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Students.CreateStudent
{
    public class CreateStudentHandler
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Guid> HandleAsync(
            CreateStudentRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var student = new Student(
                request.FirstName,
                request.LastName,
                request.Email,
                request.DateOfBirth
            );

            await _studentRepository.AddAsync(student, cancellationToken);

            return student.Id;
        }
    }
}
