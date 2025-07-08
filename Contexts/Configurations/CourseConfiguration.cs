using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore.Entities;

namespace EFCore.Contexts.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasOne(x => x.User)
            .WithMany(x => x.Courses)
            .HasForeignKey(x => x.TeacherId);

            builder.HasOne(x => x.Syllabus)
            .WithOne(x => x.Course)
            .HasForeignKey<Course>(x => x.SyllabusId);
        }

    }
}
