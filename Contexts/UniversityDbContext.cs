using EFCore.Contexts.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Contexts
{
    public class UniversityDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\ProjectModels;Initial Catalog=EFCore;Integrated Security=True");
        }
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Course> Courses { get; set; }
        public DbSet<Entities.Assignment> Assignments { get; set; }
        public DbSet<Entities.Comment> Comments { get; set; }
        public DbSet<Entities.Grade> Grades { get; set; }
        public DbSet<Entities.Syllabus> Syllabus { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AssignmentConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new GradeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(builder);
        }


    }
}

