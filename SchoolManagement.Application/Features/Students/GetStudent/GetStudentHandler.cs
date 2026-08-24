using SchoolManagement.Application.Abstractions.Persistence;

namespace SchoolManagement.Application.Features.Students.GetStudent
{
    public class GetStudentHandler
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentResponse?> HandleAsync(
            GetStudentRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (student is null)
            {
                return null;
            }

            return new StudentResponse(
                student.Id,
                student.FirstName,
                student.LastName,
                student.Email,
                student.DateOfBirth
            );
        }
    }
}
