using FluentValidation;

namespace SchoolManagement.Application.Features.Students.GetStudents
{
    public class GetStudentsValidator : AbstractValidator<GetStudentsRequest>
    {
        public GetStudentsValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
        }
    }
}
