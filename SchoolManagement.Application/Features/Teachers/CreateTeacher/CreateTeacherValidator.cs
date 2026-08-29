using FluentValidation;

namespace SchoolManagement.Application.Features.Teachers.CreateTeacher
{
    public class CreateTeacherValidator : AbstractValidator<CreateTeacherRequest>
    {
        public CreateTeacherValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email must be a valid email address.")
                .MaximumLength(255);

            RuleFor(x => x.DateOfBirth)
                .Must(BeNotInFuture)
                .WithMessage("Date of birth cannot be in the future.")
                .Must(BeAtLeast18)
                .WithMessage("Teacher must be at least 18 years old.");

            RuleFor(x => x.DateOfEmployment)
                .Must(BeNotInFuture)
                .WithMessage("Date of employment cannot be in the future.")
                .Must(
                    (request, dateOfEmployment) =>
                        dateOfEmployment >= request.DateOfBirth.AddYears(18)
                )
                .WithMessage("Date of employment cannot precede legal working age (18).");
        }

        private static bool BeNotInFuture(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return date <= today;
        }

        private static bool BeAtLeast18(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return date.AddYears(18) <= today;
        }
    }
}
