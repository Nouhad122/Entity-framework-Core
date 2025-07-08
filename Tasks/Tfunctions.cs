using EFCore.Contexts;
using EFCore.Entities;

namespace EFCore.Tasks
{
    public class GradeCalculator
    {
        // 1. Query to show average grade per course
        public static void ShowAverageGradePerCourse(UniversityDbContext context)
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("        AVERAGE GRADE PER COURSE");
            Console.WriteLine("==========================================");

            var courses = context.Courses.ToList();

            if (!courses.Any())
            {
                Console.WriteLine("No courses found.");
                return;
            }

            foreach (var course in courses)
            {
                var assignments = context.Assignments.Where(a => a.CourseId == course.CourseId).ToList();

                if (!assignments.Any())
                {
                    Console.WriteLine($"Course: {course.CourseName} - No assignments found");
                    continue;
                }

                var assignmentIds = assignments.Select(a => a.AssignmentId).ToList();
                var grades = context.Grades.Where(g => assignmentIds.Contains(g.AssignmentId) && g.NumericGrade.HasValue).ToList();

                if (!grades.Any())
                {
                    Console.WriteLine($"Course: {course.CourseName} - No grades found");
                    continue;
                }

                // Calculate average grade
                double averageGrade = grades.Average(g => g.NumericGrade.Value);
                string letterGrade = GetLetterGrade((int)Math.Round(averageGrade));

                // Get teacher name
                var teacher = context.Users.FirstOrDefault(u => u.UserId == course.TeacherId);
                string teacherName = teacher != null ? $"{teacher.FirstName} {teacher.LastName}" : "No Teacher";

                Console.WriteLine($"Course: {course.CourseName}");
                Console.WriteLine($"Teacher: {teacherName}");
                Console.WriteLine($"Average Grade: {averageGrade:F1}");
                Console.WriteLine($"Letter Grade: {letterGrade}");
                Console.WriteLine($"Total Grades: {grades.Count}");
                Console.WriteLine("------------------------------------------");
            }
        }

        // 2. Method to return letter grades (A, B, C, D, F) based on student's performance
        public static string GetLetterGrade(int numericGrade)
        {
            if (numericGrade >= 90) return "A";
            if (numericGrade >= 80) return "B";
            if (numericGrade >= 70) return "C";
            if (numericGrade >= 60) return "D";
            return "F";
        }

        // 3. Method to calculate GPA for a student
        public static void CalculateStudentGPA(UniversityDbContext context, int studentId)
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("         STUDENT GPA CALCULATION");
            Console.WriteLine("==========================================");

            // Get student information
            var student = context.Users.FirstOrDefault(u => u.UserId == studentId && u.Role == "Student");

            if (student == null)
            {
                Console.WriteLine($"Student with ID {studentId} not found.");
                return;
            }

            // Get all grades for this student
            var grades = context.Grades.Where(g => g.StudentId == studentId && g.NumericGrade.HasValue).ToList();

            if (!grades.Any())
            {
                Console.WriteLine($"No grades found for {student.FirstName} {student.LastName}.");
                return;
            }

            Console.WriteLine($"Student: {student.FirstName} {student.LastName}");
            Console.WriteLine($"Email: {student.EmailAddress}");
            Console.WriteLine("------------------------------------------");

            double totalGradePoints = 0;
            int totalGrades = 0;

            Console.WriteLine("Individual Grades:");

            foreach (var grade in grades)
            {
                int numericGrade = grade.NumericGrade.Value;
                string letterGrade = GetLetterGrade(numericGrade);
                double gradePoints = GetGradePoints(letterGrade);

                // Get assignment and course information
                var assignment = context.Assignments.FirstOrDefault(a => a.AssignmentId == grade.AssignmentId);
                var course = assignment != null ? context.Courses.FirstOrDefault(c => c.CourseId == assignment.CourseId) : null;

                string assignmentName = assignment?.AssignmentTitle ?? "Unknown Assignment";
                string courseName = course?.CourseName ?? "Unknown Course";

                Console.WriteLine($"  {courseName} - {assignmentName}");
                Console.WriteLine($"    Grade: {numericGrade} ({letterGrade}) = {gradePoints:F1} points");

                totalGradePoints += gradePoints;
                totalGrades++;
            }

            // Calculate GPA
            double gpa = totalGradePoints / totalGrades;

            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Total Grades: {totalGrades}");
            Console.WriteLine($"Total Grade Points: {totalGradePoints:F1}");
            Console.WriteLine($"GPA: {gpa:F2}");
            Console.WriteLine($"GPA Status: {GetGPAStatus(gpa)}");
        }

        private static double GetGradePoints(string letterGrade)
        {
            if (letterGrade == "A") return 4.0;
            if (letterGrade == "B") return 3.0;
            if (letterGrade == "C") return 2.0;
            if (letterGrade == "D") return 1.0;
            return 0.0;
        }

        private static string GetGPAStatus(double gpa)
        {
            if (gpa >= 3.7)
                return "Excellent (Dean's List)";
            else if (gpa >= 3.0)
                return "Good Standing";
            else if (gpa >= 2.0)
                return "Satisfactory";
            else if (gpa >= 1.0)
                return "Academic Warning";
            else
                return "Academic Probation";
        }
    }
}