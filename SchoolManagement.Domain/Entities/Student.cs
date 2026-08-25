namespace SchoolManagement.Domain.Entities
{
    public class Student
    {
        private readonly List<Enrollment> _enrollments = [];

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

        /*
        The parameterless private Student() constructor exists primarily
        for Object-Relational Mappers (ORMs) like Entity Framework Core (EF Core).
        */
        private Student() { }

        public Student(string firstName, string lastName, string email, DateOnly dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name is required.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name is required.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.");
            }

            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public void UpdateDetails(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name is required.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name is required.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.");
            }

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void Enroll(Guid schoolClassId, Guid academicSessionId)
        {
            if (schoolClassId == Guid.Empty)
            {
                throw new ArgumentException("School class ID is required.");
            }

            if (academicSessionId == Guid.Empty)
            {
                throw new ArgumentException("Academic session ID is required.");
            }

            /*
                This checks if the student is already enrolled in that session
            */
            var alreadyEnrolled = _enrollments.Any(enrollment =>
                enrollment.AcademicSessionId == academicSessionId
            );

            if (alreadyEnrolled)
            {
                throw new InvalidOperationException(
                    "Student is already enrolled in this academic session."
                );
            }

            var enrollment = new Enrollment(Id, schoolClassId, academicSessionId);

            _enrollments.Add(enrollment);
        }

        public void Update(string firstName, string lastName, string email, DateOnly dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name is required.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name is required.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.");
            }

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }
    }
}
