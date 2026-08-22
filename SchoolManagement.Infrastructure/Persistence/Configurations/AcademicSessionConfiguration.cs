using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Persistence.Configurations
{
    public class AcademicSessionConfiguration : IEntityTypeConfiguration<AcademicSession>
    {
        public void Configure(EntityTypeBuilder<AcademicSession> builder)
        {
            builder.HasKey(session => session.Id);

            builder.Property(session => session.Name).HasMaxLength(50).IsRequired();

            builder.Property(session => session.StartDate).IsRequired();

            builder.Property(session => session.EndDate).IsRequired();

            /*
                Two sessions cannot have the same name.
            */
            builder.HasIndex(session => session.Name).IsUnique();
        }
    }
}
