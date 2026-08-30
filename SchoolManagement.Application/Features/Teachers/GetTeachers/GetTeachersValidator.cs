using FluentValidation;

namespace SchoolManagement.Application.Features.Teachers.GetTeachers
{
    public class GetTeachersValidator : AbstractValidator<GetTeachersRequest>
    {
        public GetTeachersValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(100)
                .WithMessage("Page size must be between 1 and 100.");
        }
    }
}
