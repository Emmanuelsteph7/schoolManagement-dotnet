using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Application.Common.Pagination;
using SchoolManagement.Application.Features.Teachers.GetTeacher;

namespace SchoolManagement.Application.Features.Teachers.GetTeachers
{
    public class GetTeachersHandler
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeachersHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<PagedResult<TeacherResponse>> HandleAsync(
            GetTeachersRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var (teachers, totalCount) = await _teacherRepository.GetPagedAsync(
                request.Page,
                request.PageSize,
                cancellationToken
            );

            var items = teachers
                .Select(teacher => new TeacherResponse(
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
                ))
                .ToList();

            return new PagedResult<TeacherResponse>(
                items,
                request.Page,
                request.PageSize,
                totalCount
            );
        }
    }
}
