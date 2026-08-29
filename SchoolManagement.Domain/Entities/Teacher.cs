using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        private string activateErrorMessage =
            "Teacher needs to activate email address before activation";

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public DateOnly DateOfEmployment { get; private set; }
        public EmploymentStatus EmploymentStatus { get; private set; }
        public EmailAccountStatus EmailAccountStatus { get; private set; }
        public DateTimeOffset? EmailAccountVerifiedDate { get; private set; }

        private Teacher() { }

        public Teacher(
            string firstName,
            string lastName,
            string email,
            DateOnly dateOfBirth,
            DateOnly dateOfEmployment
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

            if (dateOfEmployment > today)
                throw new ArgumentException(
                    "Date of employment cannot be in the future.",
                    nameof(dateOfEmployment)
                );

            if (dateOfEmployment < dateOfBirth.AddYears(18))
                throw new ArgumentException(
                    "Date of employment cannot precede legal working age (18).",
                    nameof(dateOfEmployment)
                );

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim().ToLowerInvariant();
            DateOfBirth = dateOfBirth;
            DateOfEmployment = dateOfEmployment;
            EmploymentStatus = EmploymentStatus.Pending;
            EmailAccountStatus = EmailAccountStatus.Pending;
            EmailAccountVerifiedDate = null;
        }

        private (bool, string) TeacherStatusUpdateGuard(EmploymentStatus employmentStatus)
        {
            if (EmailAccountStatus == EmailAccountStatus.Pending)
            {
                return (false, activateErrorMessage);
            }

            if (EmploymentStatus == employmentStatus)
            {
                return (false, "");
            }

            return (true, "");
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
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void VerifyEmail()
        {
            if (EmailAccountStatus == EmailAccountStatus.Verified)
                return;

            var currentTime = DateTimeOffset.UtcNow;

            EmailAccountStatus = EmailAccountStatus.Verified;
            EmailAccountVerifiedDate = currentTime;
            UpdatedAt = currentTime;
        }

        public void ActivateTeacher()
        {
            var (canProceed, errorMessage) = TeacherStatusUpdateGuard(EmploymentStatus.Active);

            if (!canProceed)
            {
                if (errorMessage == activateErrorMessage)
                {
                    throw new InvalidOperationException(activateErrorMessage);
                }
                return;
            }

            var currentTime = DateTimeOffset.UtcNow;

            EmploymentStatus = EmploymentStatus.Active;
            UpdatedAt = currentTime;
        }

        public void DeactivateTeacher()
        {
            var (canProceed, errorMessage) = TeacherStatusUpdateGuard(EmploymentStatus.InActive);

            if (!canProceed)
            {
                if (errorMessage == activateErrorMessage)
                {
                    throw new InvalidOperationException(activateErrorMessage);
                }
                return;
            }

            if (EmploymentStatus == EmploymentStatus.OnLeave)
            {
                throw new InvalidOperationException("Teacher on leave can not be deactivated");
            }

            var currentTime = DateTimeOffset.UtcNow;

            EmploymentStatus = EmploymentStatus.InActive;
            UpdatedAt = currentTime;
        }

        public void PutTeacherOnLeave()
        {
            var (canProceed, errorMessage) = TeacherStatusUpdateGuard(EmploymentStatus.OnLeave);

            if (!canProceed)
            {
                if (errorMessage == activateErrorMessage)
                {
                    throw new InvalidOperationException(activateErrorMessage);
                }
                return;
            }

            if (EmploymentStatus == EmploymentStatus.InActive)
            {
                throw new InvalidOperationException(
                    "Teacher currently inactive can not be on leave"
                );
            }

            var currentTime = DateTimeOffset.UtcNow;

            EmploymentStatus = EmploymentStatus.OnLeave;
            UpdatedAt = currentTime;
        }
    }
}
