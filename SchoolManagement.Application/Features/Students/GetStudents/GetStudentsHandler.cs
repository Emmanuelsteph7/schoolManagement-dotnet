using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Application.Features.Students.GetStudent;

namespace SchoolManagement.Application.Features.Students.GetStudents
{
    public class GetStudentsHandler
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentsHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IReadOnlyList<StudentResponse>> HandleAsync(
            CancellationToken cancellationToken = default
        )
        {
            var students = await _studentRepository.GetAllAsync(cancellationToken);

            return students
                .Select(student => new StudentResponse(
                    student.Id,
                    student.FirstName,
                    student.LastName,
                    student.Email,
                    student.DateOfBirth
                ))
                .ToList();
        }
    }
}
