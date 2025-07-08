using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore.Entities;

namespace EFCore.Contexts.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            {
                builder.HasOne(x => x.Assignment)
                 .WithOne(x => x.Grade)
                   .HasForeignKey<Grade>(x => x.AssignmentId);

                builder.HasOne(x => x.User)
                .WithMany(x => x.Grades)
                  .HasForeignKey(x => x.StudentId);
            }
        } 
    }
}
