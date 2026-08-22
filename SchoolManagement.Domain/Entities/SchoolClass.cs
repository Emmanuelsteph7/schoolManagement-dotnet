namespace SchoolManagement.Domain.Entities
{
    public class SchoolClass
    {
        private readonly List<Enrollment> _enrollments = [];

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

        private SchoolClass() { }

        public SchoolClass(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Class name is required.");
            }

            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
