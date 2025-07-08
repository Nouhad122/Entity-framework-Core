using EFCore.Contexts;
using EFCore.Entities;

namespace EFCore.Tasks
{
    public class TablesListing
    {
        public static void ListTables(UniversityDbContext context) {
            var courses = context.Courses.ToList();

            //List Courses
            if(courses.Count == 0)
            {
                Console.WriteLine("No courses found.");
                return;
            }

            Console.WriteLine("Courses:");

            foreach (var course in courses)
            {
                Console.WriteLine(course.CourseName);
            }
            Console.WriteLine("\n======================================");

            //List Assignments
            var assignments = context.Assignments.ToList();
            if(assignments.Count == 0)
            {
                Console.WriteLine("No assignments found.");
                return;
            }

            var chosenAssignment = context.Assignments.FirstOrDefault(a => a.AssignmentId == 1);

            if (chosenAssignment != null)
            {
                Console.WriteLine("Assignment: " + chosenAssignment.AssignmentTitle);
                Console.WriteLine("Description: " + chosenAssignment.Description);
                Console.WriteLine("Due Date: " + chosenAssignment.DueDate);
            }
            else
            {
                Console.WriteLine("Assignment not found.");
            }

            Console.WriteLine("\n======================================");

            //List all students
            var students = context.Users.Where(u => u.Role == "Student").ToList();
            foreach(var student in students) 
            { 
                Console.WriteLine($"Student: {student.FirstName} {student.LastName}" +
                    $", Email: {student.EmailAddress}, Phone: {student.PhoneNumber}");  
            }

            Console.WriteLine("\n======================================");

            //List Comments
            var comments = context.Comments.Where(c => c.AssignmentId == 1).ToList();
            var assignment = context.Assignments.First(a => a.AssignmentId == 1);
            foreach (var comment in comments)
            {
                Console.WriteLine($"Comment for Assignment {assignment.AssignmentTitle}: {comment.CommentContent}");
            }

            Console.WriteLine("\n======================================");

            //grades for a student
            var grade = context.Grades.Where(g => g.StudentId == 5).FirstOrDefault();
            var studentForGrade = context.Users.FirstOrDefault(u => u.UserId == grade.StudentId);

            Console.WriteLine($"Student: {studentForGrade.FirstName + ' ' + studentForGrade.LastName}, Grade: {grade.NumericGrade}");

            Console.WriteLine("\n======================================");

            //List each assignment with its course and the teacher’s full name

            var targetedAssignments = context.Assignments.ToList();
            foreach (var ass in targetedAssignments)
            {
                var course = context.Courses.FirstOrDefault(c => c.CourseId == ass.CourseId);
                var teacher = context.Users.FirstOrDefault(u => u.UserId == course.TeacherId);
                if (course != null && teacher != null)
                {
                    Console.WriteLine($"Assignment: {ass.AssignmentTitle}, Course: {course.CourseName}, Teacher: {teacher.FirstName} {teacher.LastName}");
                }
                else
                {
                    Console.WriteLine($"Assignment: {ass.AssignmentTitle} has no associated course or teacher.");
                }
            }
        }
    }
}
