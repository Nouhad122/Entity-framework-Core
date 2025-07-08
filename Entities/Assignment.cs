namespace EFCore.Entities
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public string AssignmentTitle { get; set; }
        public string? Description { get; set; }
        public float Weight { get; set; }
        public int MaxGrade { get; set; }
        public DateTime DueDate { get; set; }

        public Course Course { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Grade Grade { get; set; }
    }
}
