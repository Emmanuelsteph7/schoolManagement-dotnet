using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(enrollment => enrollment.Id);

            builder
                .HasOne(enrollment => enrollment.Student)
                .WithMany(student => student.Enrollments)
                .HasForeignKey(enrollment => enrollment.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(enrollment => enrollment.SchoolClass)
                .WithMany(schoolClass => schoolClass.Enrollments)
                .HasForeignKey(enrollment => enrollment.SchoolClassId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(enrollment => enrollment.AcademicSession)
                .WithMany(session => session.Enrollments)
                .HasForeignKey(enrollment => enrollment.AcademicSessionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(enrollment => new { enrollment.StudentId, enrollment.AcademicSessionId })
                .IsUnique();
        }
    }
}
