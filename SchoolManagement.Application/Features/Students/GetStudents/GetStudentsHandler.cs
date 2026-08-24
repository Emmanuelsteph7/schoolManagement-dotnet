using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Application.Common.Pagination;
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

        public async Task<PagedResult<StudentResponse>> HandleAsync(
            GetStudentsRequest request,
            CancellationToken cancellationToken = default
        )
        {
            var (students, totalCount) = await _studentRepository.GetPagedAsync(
                request.Page,
                request.PageSize,
                cancellationToken
            );

            var items = students
                .Select(student => new StudentResponse(
                    student.Id,
                    student.FirstName,
                    student.LastName,
                    student.Email,
                    student.DateOfBirth
                ))
                .ToList();

            return new PagedResult<StudentResponse>(
                items,
                request.Page,
                request.PageSize,
                totalCount
            );
        }
    }
}
