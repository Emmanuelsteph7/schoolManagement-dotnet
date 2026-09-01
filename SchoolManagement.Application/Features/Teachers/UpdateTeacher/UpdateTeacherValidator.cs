using FluentValidation;

namespace SchoolManagement.Application.Features.Teachers.UpdateTeacher
{
    public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherRequest>
    {
        public UpdateTeacherValidator()
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
        }
    }
}
