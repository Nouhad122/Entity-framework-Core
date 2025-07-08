using EFCore.Contexts;
using EFCore.Entities;

namespace EFCore
{
    public static class UniversityDataSeeder
    {
        public static void SeedAllData(UniversityDbContext context)
        {
            // Check if data already exists
            if (context.Users.Any())
            {
                Console.WriteLine("Data already exists. Skipping seed.");
                return;
            }

            try
            {
                SeedUsers(context);
                SeedSyllabus(context);
                SeedCourses(context);
                SeedAssignments(context);
                SeedComments(context);
                SeedGrades(context);

                Console.WriteLine("University data seeding completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during seeding: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        private static void SeedUsers(UniversityDbContext context)
        {
            var teacherNames = new List<(string userName, string firstName, string lastName, string email, string phone)>
            {
                ("sami.teacher", "Sami", "Al-Ahmad", "sami.ahmad@university.edu", "555-1001"),
                ("feryal.teacher", "Feryal", "Al-Hassan", "feryal.hassan@university.edu", "555-1002")
            };

            for (int i = 0; i < teacherNames.Count; i++)
            {
                var teacher = teacherNames[i];
                var newTeacher = new User
                {
                    UserName = teacher.userName,
                    FirstName = teacher.firstName,
                    LastName = teacher.lastName,
                    EmailAddress = teacher.email,
                    PhoneNumber = teacher.phone,
                    Role = "Teacher"
                };
                context.Users.Add(newTeacher);
            }

            var studentNames = new List<(string userName, string firstName, string lastName, string email, string phone)>
            {
                ("ahmed.student", "Ahmed", "Al-Mansouri", "ahmed.mansouri@student.university.edu", "555-2001"),
                ("fatima.student", "Fatima", "Al-Zahra", "fatima.zahra@student.university.edu", "555-2002"),
                ("omar.student", "Omar", "Al-Rashid", "omar.rashid@student.university.edu", "555-2003"),
                ("layla.student", "Layla", "Al-Nouri", "layla.nouri@student.university.edu", "555-2004"),
                ("hassan.student", "Hassan", "Al-Mahmoud", "hassan.mahmoud@student.university.edu", "555-2005"),
                ("aisha.student", "Aisha", "Al-Salem", "aisha.salem@student.university.edu", "555-2006"),
                ("youssef.student", "Youssef", "Al-Badawi", "youssef.badawi@student.university.edu", "555-2007"),
                ("zeinab.student", "Zeinab", "Al-Khoury", "zeinab.khoury@student.university.edu", "555-2008"),
                ("karim.student", "Karim", "Al-Farouk", "karim.farouk@student.university.edu", "555-2009"),
                ("maya.student", "Maya", "Al-Sharif", "maya.sharif@student.university.edu", "555-2010")
            };

            for (int i = 0; i < studentNames.Count; i++)
            {
                var student = studentNames[i];
                var newStudent = new User
                {
                    UserName = student.userName,
                    FirstName = student.firstName,
                    LastName = student.lastName,
                    EmailAddress = student.email,
                    PhoneNumber = student.phone,
                    Role = "Student"
                };
                context.Users.Add(newStudent);
            }

            context.SaveChanges();
        }

        private static void SeedSyllabus(UniversityDbContext context)
        {

            var syllabusDescriptions = new List<string>
            {
                "Introduction to SQL database management, queries, joins, indexes, and database design principles. Students will learn to create, modify, and query relational databases using SQL.",
                "Comprehensive C# programming course covering object-oriented programming, LINQ, exception handling, and .NET framework fundamentals for building robust applications.",
                "Entity Framework Core course focusing on Object-Relational Mapping (ORM), Code First approach, migrations, and database operations in .NET applications.",
                "Web API development using ASP.NET Core, including RESTful services, authentication, authorization, and API documentation with Swagger.",
                "Modern React development including components, hooks, state management, routing, and integration with backend APIs for building dynamic web applications."
            };

            for (int i = 0; i < syllabusDescriptions.Count; i++)
            {
                var newSyllabus = new Syllabus
                {
                    Description = syllabusDescriptions[i]
                };
                context.Syllabus.Add(newSyllabus);
            }

            context.SaveChanges();
        }

        private static void SeedCourses(UniversityDbContext context)
        {

            // Get teachers
            var sami = context.Users.First(u => u.UserName == "sami.teacher");
            var feryal = context.Users.First(u => u.UserName == "feryal.teacher");
            var syllabuses = context.Syllabus.ToList();

            var courseData = new List<(string name, int teacherId, DateTime start, DateTime end, int syllabusIndex)>
            {
                ("SQL Database Management", sami.UserId, new DateTime(2024, 9, 1), new DateTime(2024, 12, 15), 0),
                ("C# Programming Fundamentals", feryal.UserId, new DateTime(2024, 9, 1), new DateTime(2024, 12, 15), 1),
                ("Entity Framework Core", sami.UserId, new DateTime(2025, 1, 15), new DateTime(2025, 5, 30), 2),
                ("Web API Development", feryal.UserId, new DateTime(2025, 1, 15), new DateTime(2025, 5, 30), 3),
                ("React Frontend Development", sami.UserId, new DateTime(2025, 6, 1), new DateTime(2025, 9, 15), 4)
            };

            for (int i = 0; i < courseData.Count; i++)
            {
                var course = courseData[i];
                var newCourse = new Course
                {
                    CourseName = course.name,
                    TeacherId = course.teacherId,
                    StartDate = course.start,
                    EndDate = course.end,
                    SyllabusId = syllabuses[course.syllabusIndex].SyllabusId
                };
                context.Courses.Add(newCourse);
            }

            context.SaveChanges();
        }

        private static void SeedAssignments(UniversityDbContext context)
        {
            var courses = context.Courses.ToList();

            // SQL Course Assignments
            var sqlAssignments = new List<(string title, string description, float weight, int maxGrade, DateTime dueDate)>
            {
                ("Basic SQL Queries", "Write SELECT statements with WHERE, ORDER BY, and GROUP BY clauses", 0.15f, 100, new DateTime(2024, 9, 15)),
                ("Database Design Project", "Design and create a normalized database for a library system", 0.25f, 100, new DateTime(2024, 10, 15)),
                ("Advanced Joins Assignment", "Create complex queries using INNER, LEFT, and RIGHT joins", 0.20f, 100, new DateTime(2024, 11, 1)),
                ("Stored Procedures Lab", "Develop stored procedures for data manipulation and reporting", 0.20f, 100, new DateTime(2024, 11, 15)),
                ("Final Database Project", "Complete database implementation with triggers and views", 0.20f, 100, new DateTime(2024, 12, 10))
            };

            // C# Course Assignments
            var csharpAssignments = new List<(string title, string description, float weight, int maxGrade, DateTime dueDate)>
            {
                ("Variables and Data Types", "Practice with C# basic syntax and data types", 0.10f, 100, new DateTime(2024, 9, 20)),
                ("Object-Oriented Programming", "Create classes with inheritance and polymorphism", 0.25f, 100, new DateTime(2024, 10, 20)),
                ("LINQ and Collections", "Work with Lists, Arrays, and LINQ queries", 0.20f, 100, new DateTime(2024, 11, 5)),
                ("Exception Handling", "Implement try-catch blocks and custom exceptions", 0.15f, 100, new DateTime(2024, 11, 20)),
                ("Final Console Application", "Build a complete console-based management system", 0.30f, 100, new DateTime(2024, 12, 12))
            };

            // Entity Framework Assignments
            var efAssignments = new List<(string title, string description, float weight, int maxGrade, DateTime dueDate)>
            {
                ("Code First Migrations", "Set up EF Core with Code First approach", 0.20f, 100, new DateTime(2025, 2, 1)),
                ("CRUD Operations Lab", "Implement Create, Read, Update, Delete operations", 0.20f, 100, new DateTime(2025, 2, 15)),
                ("Advanced Querying", "Use Include, ThenInclude, and complex LINQ queries", 0.20f, 100, new DateTime(2025, 3, 15)),
                ("Data Relationships", "Configure one-to-many and many-to-many relationships", 0.20f, 100, new DateTime(2025, 4, 15)),
                ("Performance Optimization", "Implement efficient queries and avoid N+1 problems", 0.20f, 100, new DateTime(2025, 5, 15))
            };

            // Web API Assignments
            var webApiAssignments = new List<(string title, string description, float weight, int maxGrade, DateTime dueDate)>
            {
                ("REST API Basics", "Create basic GET, POST, PUT, DELETE endpoints", 0.20f, 100, new DateTime(2025, 2, 5)),
                ("Authentication & Authorization", "Implement JWT authentication and role-based access", 0.25f, 100, new DateTime(2025, 3, 1)),
                ("Data Validation", "Add model validation and error handling", 0.15f, 100, new DateTime(2025, 3, 20)),
                ("API Documentation", "Document API with Swagger/OpenAPI", 0.15f, 100, new DateTime(2025, 4, 20)),
                ("Complete API Project", "Build a full-featured Web API with all concepts", 0.25f, 100, new DateTime(2025, 5, 20))
            };

            // React Assignments
            var reactAssignments = new List<(string title, string description, float weight, int maxGrade, DateTime dueDate)>
            {
                ("React Components", "Create functional and class components with props", 0.20f, 100, new DateTime(2025, 6, 15)),
                ("State Management", "Use useState and useEffect hooks effectively", 0.20f, 100, new DateTime(2025, 7, 1)),
                ("React Router", "Implement navigation with React Router", 0.15f, 100, new DateTime(2025, 7, 20)),
                ("API Integration", "Connect React app to backend APIs", 0.20f, 100, new DateTime(2025, 8, 15)),
                ("Final React Application", "Build a complete single-page application", 0.25f, 100, new DateTime(2025, 9, 10))
            };

            var allAssignmentLists = new List<(List<(string title, string description, float weight, int maxGrade, DateTime dueDate)> assignments, int courseIndex)>
            {
                (sqlAssignments, 0),
                (csharpAssignments, 1),
                (efAssignments, 2),
                (webApiAssignments, 3),
                (reactAssignments, 4)
            };

            int totalAssignments = 0;
            for (int i = 0; i < allAssignmentLists.Count; i++)
            {
                var assignmentList = allAssignmentLists[i];
                var courseId = courses[assignmentList.courseIndex].CourseId;

                for (int j = 0; j < assignmentList.assignments.Count; j++)
                {
                    var assignment = assignmentList.assignments[j];
                    var newAssignment = new Assignment
                    {
                        CourseId = courseId,
                        AssignmentTitle = assignment.title,
                        Description = assignment.description,
                        Weight = assignment.weight,
                        MaxGrade = assignment.maxGrade,
                        DueDate = assignment.dueDate
                    };
                    context.Assignments.Add(newAssignment);
                    totalAssignments++;
                }
            }

            context.SaveChanges();
        }

        private static void SeedComments(UniversityDbContext context)
        {

            var assignments = context.Assignments.ToList();
            var users = context.Users.ToList();

            var commentData = new List<(string content, DateTime date, string userType)>
            {
                ("Great work on this assignment! Your solution shows good understanding of the concepts.", new DateTime(2024, 9, 16), "Teacher"),
                ("Please review the requirements again. Some parts are missing.", new DateTime(2024, 9, 20), "Teacher"),
                ("This assignment was challenging but I learned a lot from it.", new DateTime(2024, 10, 1), "Student"),
                ("Excellent implementation! Consider optimizing the performance in the next iteration.", new DateTime(2024, 10, 5), "Teacher"),
                ("I found this topic very interesting and would like to learn more.", new DateTime(2024, 10, 10), "Student"),
                ("Good effort, but there are some syntax errors that need to be fixed.", new DateTime(2024, 10, 15), "Teacher"),
                ("Can you provide more examples for this concept?", new DateTime(2024, 10, 20), "Student"),
                ("This is a creative approach to the problem. Well done!", new DateTime(2024, 10, 25), "Teacher"),
                ("Please add more comments to your code for better readability.", new DateTime(2024, 11, 1), "Teacher"),
                ("Outstanding work! This exceeds the assignment requirements.", new DateTime(2024, 11, 5), "Teacher"),
                ("The Entity Framework concepts are becoming clearer now.", new DateTime(2025, 2, 5), "Student"),
                ("Good start, but please complete all the required features.", new DateTime(2025, 2, 10), "Teacher")
            };

            for (int i = 0; i < commentData.Count; i++)
            {
                var comment = commentData[i];
                var user = comment.userType == "Teacher"
                    ? users.First(u => u.Role == "Teacher")
                    : users.First(u => u.Role == "Student");

                var newComment = new Comment
                {
                    AssignmentId = assignments[i % assignments.Count].AssignmentId,
                    CreatedByUserId = user.UserId,
                    CreatedDate = comment.date,
                    CommentContent = comment.content
                };
                context.Comments.Add(newComment);
            }

            context.SaveChanges();
        }

        private static void SeedGrades(UniversityDbContext context)
        {

            var assignments = context.Assignments.ToList();
            var students = context.Users.Where(u => u.Role == "Student").ToList();

            var gradeValues = new List<int> { 85, 92, 78, 88, 95, 82, 89, 91, 86, 93, 87, 94, 83, 90, 88, 85, 92, 89, 91, 87, 93, 86, 88, 92, 90 };

            for (int i = 0; i < assignments.Count; i++)
            {
                var studentIndex = i % students.Count;
                var gradeIndex = i % gradeValues.Count;

                var newGrade = new Grade
                {
                    AssignmentId = assignments[i].AssignmentId,
                    StudentId = students[studentIndex].UserId,
                    NumericGrade = gradeValues[gradeIndex]
                };
                context.Grades.Add(newGrade);
            }

            context.SaveChanges();
        }
    }
}