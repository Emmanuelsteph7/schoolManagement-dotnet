using FluentValidation;

namespace SchoolManagement.Application.Features.Students.CreateStudent
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("aaa")
                .MaximumLength(100)
                .WithMessage("kjsnknksk");

            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("Date of birth must be in the past.");
        }
    }
}
