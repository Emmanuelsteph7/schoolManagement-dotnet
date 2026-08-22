namespace SchoolManagement.Domain.Entities
{
    public class Subject
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private Subject() { }

        public Subject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Subject name is required.");
            }

            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
