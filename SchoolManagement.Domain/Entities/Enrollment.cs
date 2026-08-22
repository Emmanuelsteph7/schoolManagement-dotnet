namespace SchoolManagement.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; private set; }
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; } = null!;
        public Guid SchoolClassId { get; private set; }
        public SchoolClass SchoolClass { get; private set; } = null!;
        public Guid AcademicSessionId { get; private set; }
        public AcademicSession AcademicSession { get; private set; } = null!;

        private Enrollment() { }

        public Enrollment(Guid studentId, Guid schoolClassId, Guid academicSessionId)
        {
            if (studentId == Guid.Empty)
            {
                throw new ArgumentException("Student ID is required.");
            }

            if (schoolClassId == Guid.Empty)
            {
                throw new ArgumentException("School class ID is required.");
            }

            if (academicSessionId == Guid.Empty)
            {
                throw new ArgumentException("Academic session is required.");
            }

            Id = Guid.NewGuid();
            StudentId = studentId;
            SchoolClassId = schoolClassId;
            AcademicSessionId = academicSessionId;
        }
    }
}
