using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(teacher => teacher.Id);

            builder.Property(teacher => teacher.FirstName).HasMaxLength(100).IsRequired();

            builder.Property(teacher => teacher.LastName).HasMaxLength(100).IsRequired();

            builder.Property(teacher => teacher.Email).HasMaxLength(255).IsRequired();

            /*
                Two teachers cannot have the same email address.
            */
            builder.HasIndex(teacher => teacher.Email).IsUnique();
        }
    }
}
