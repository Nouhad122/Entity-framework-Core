namespace EFCore.Entities
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public int? NumericGrade { get; set; }
        public Assignment Assignment { get; set; }
        public User User { get; set; }
    }
}
