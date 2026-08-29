using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Teachers.CreateTeacher
{
    public class CreateTeacherHandler
    {
        private readonly ITeacherRepository _teacherRepository;

        public CreateTeacherHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Guid> HandleAsync(
            CreateTeacherRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var teacher = new Teacher(
                request.FirstName,
                request.LastName,
                request.Email,
                request.DateOfBirth,
                request.DateOfEmployment
            );

            await _teacherRepository.AddAsync(teacher, cancellationToken);

            return teacher.Id;
        }
    }
}
