using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public DateOnly DateOfEmployment { get; private set; }
        public EmploymentStatus Status { get; private set; }

        private Teacher() { }

        public Teacher(
            string firstName,
            string lastName,
            string email,
            DateOnly dateOfBirth,
            DateOnly dateOfEmployment,
            EmploymentStatus status
        )
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name is required.", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name is required.", nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.", nameof(email));
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (dateOfBirth > today)
                throw new ArgumentException(
                    "Date of birth cannot be in the future.",
                    nameof(dateOfBirth)
                );

            if (dateOfBirth.AddYears(18) > today)
                throw new ArgumentException(
                    "Teacher must be at least 18 years old.",
                    nameof(dateOfBirth)
                );

            if (dateOfEmployment < dateOfBirth.AddYears(18))
                throw new ArgumentException(
                    "Date of employment cannot precede legal working age (18).",
                    nameof(dateOfEmployment)
                );

            if (!Enum.IsDefined(typeof(EmploymentStatus), status))
                throw new ArgumentOutOfRangeException(
                    nameof(status),
                    "Invalid employment status specified."
                );

            Id = Guid.NewGuid();
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim().ToLowerInvariant();
            DateOfBirth = dateOfBirth;
            DateOfEmployment = dateOfEmployment;
            Status = status;
        }

        public void UpdateDetails(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name is required.", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name is required.", nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.", nameof(email));
            }

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim().ToLowerInvariant();
        }
    }
}
