using SchoolManagement.Application.Abstractions.Persistence;

namespace SchoolManagement.Application.Features.Students.UpdateStudent
{
    public class UpdateStudentHandler
    {
        private readonly IStudentRepository _studentRepository;

        public UpdateStudentHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> HandleAsync(
            UpdateStudentRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (student is null)
            {
                return false;
            }

            student.Update(request.FirstName, request.LastName, request.Email, request.DateOfBirth);

            await _studentRepository.UpdateAsync(student, cancellationToken);

            return true;
        }
    }
}
