using EFCore.Contexts;

namespace EFCore.Tasks
{
    public class DataEditingOperations
    {
        public static void UpdateStudentToTeacher(UniversityDbContext context, int studentId)
        {
            // Find the student
            var student = context.Users.FirstOrDefault(u => u.UserId == studentId);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            if (student.Role != "Student")
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} is not a student.");
                return;
            }

            // Update the role
            student.Role = "Teacher";

            context.SaveChanges();

            Console.WriteLine($"{student.FirstName} {student.LastName} is now a Teacher!");
        }

        // Delete a specific comment
        public static void DeleteComment(UniversityDbContext context, int commentId)
        {
            // Find the comment
            var comment = context.Comments.FirstOrDefault(c => c.CommentId == commentId);

            if (comment == null)
            {
                Console.WriteLine("Comment not found.");
                return;
            }

            // Remove the comment
            context.Comments.Remove(comment);

            context.SaveChanges();

            Console.WriteLine("Comment deleted successfully!");
        }

    }
}
