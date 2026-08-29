using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    /**
        This define database schema constraints for the Student entity.
        Instead of cluttering your domain model class (Student) with
        database annotations like [Required] or [MaxLength],
        implementing IEntityTypeConfiguration<T> separates database configuration
        from your core domain logic (following Clean Architecture principles).
    */
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            /*
                Configures the Id property as the Primary Key for the Students table.
            */
            builder.HasKey(student => student.Id);

            /*
                Mapped as non-nullable columns (NOT NULL) with a maximum length of
                100 characters (e.g., VARCHAR(100) or TEXT depending on your database provider).
            */
            builder.Property(student => student.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(student => student.LastName).HasMaxLength(100).IsRequired();

            /*
                Mapped as non-nullable columns (NOT NULL) with a maximum length of
                255 characters (e.g., VARCHAR(255) or TEXT depending on your database provider).
            */
            builder.Property(student => student.Email).HasMaxLength(255).IsRequired();

            builder.Property(student => student.CreatedAt).IsRequired();

            builder.Property(student => student.UpdatedAt);

            /*
                Marked as a required column (NOT NULL).
            */
            builder.Property(student => student.DateOfBirth).IsRequired();
        }
    }
}
