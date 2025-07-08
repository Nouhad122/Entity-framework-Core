using EFCore;
using EFCore.Contexts;
using EFCore.Tasks;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new UniversityDbContext();

            try
            {   
                // Ensure database is created
                Console.WriteLine("Ensuring database exists...");
                context.Database.EnsureCreated();
                
                // Seed all university data
                UniversityDataSeeder.SeedAllData(context);
                
                Console.WriteLine("\n======================================");
                Console.WriteLine("Database seeding completed successfully!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError occurred during seeding: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            TablesListing.ListTables(context);
            GradeCalculator.ShowAverageGradePerCourse(context);
            GradeCalculator.CalculateStudentGPA(context, 7);
            Console.WriteLine("\n======================================");

            DataEditingOperations.UpdateStudentToTeacher(context, 7);
            Console.WriteLine("\n======================================");

            DataEditingOperations.DeleteComment(context, 3);

        }
    }
}
