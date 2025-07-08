using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore.Entities;

namespace EFCore.Contexts.Configurations
{
    public class SyllabusConfiguration : IEntityTypeConfiguration<Syllabus>
    {
        public void Configure(EntityTypeBuilder<Syllabus> builder)
        {
        }
    }
}
