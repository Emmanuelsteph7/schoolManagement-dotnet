using SchoolManagement.Application.Abstractions.Persistence;

namespace SchoolManagement.Application.Features.Teachers.UpdateTeacher
{
    public class UpdateTeacherHandler
    {
        private readonly ITeacherRepository _teacherRepository;

        public UpdateTeacherHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<bool> HandleAsync(
            Guid id,
            UpdateTeacherRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var teacher = await _teacherRepository.GetByIdAsync(id, cancellationToken);

            if (teacher is null)
            {
                return false;
            }

            teacher.UpdateDetails(request.FirstName, request.LastName, request.Email);

            await _teacherRepository.UpdateAsync(teacher, cancellationToken);

            return true;
        }
    }
}
