using SchoolManagement.Application.Abstractions.Persistence;

namespace SchoolManagement.Application.Features.Teachers.GetTeacher
{
    public class GetTeacherHandler
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeacherHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<TeacherResponse?> HandleAsync(
            GetTeacherRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.Id, cancellationToken);

            if (teacher is null)
            {
                return null;
            }

            return new TeacherResponse(
                teacher.Id,
                teacher.FirstName,
                teacher.LastName,
                teacher.Email,
                teacher.DateOfBirth,
                teacher.DateOfEmployment,
                teacher.EmploymentStatus,
                teacher.EmailAccountStatus,
                teacher.EmailAccountVerifiedDate,
                teacher.CreatedAt,
                teacher.UpdatedAt
            );
        }
    }
}
