using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore.Entities;

namespace EFCore.Contexts.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(x => x.Assignment)
             .WithMany(x => x.Comments)
               .HasForeignKey(x => x.AssignmentId);

            builder.HasOne(x => x.User)
            .WithMany(x => x.Comments)
              .HasForeignKey(x => x.CreatedByUserId);
        }

    }
}
