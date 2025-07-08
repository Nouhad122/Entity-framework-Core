namespace EFCore.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int? TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? SyllabusId { get; set; }

        public ICollection<Assignment> Assignments { get; set; }
        public User User { get; set; }
        public Syllabus Syllabus { get; set; }
    }
}
