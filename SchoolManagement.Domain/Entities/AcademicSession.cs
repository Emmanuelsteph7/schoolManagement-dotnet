namespace SchoolManagement.Domain.Entities
{
    public class AcademicSession
    {
        private readonly List<Enrollment> _enrollments = [];

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

        private AcademicSession() { }

        public AcademicSession(string name, DateOnly startDate, DateOnly endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Academic session name is required.");
            }

            if (startDate >= endDate)
            {
                throw new ArgumentException("Start date must be before end date.");
            }

            Id = Guid.NewGuid();
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
