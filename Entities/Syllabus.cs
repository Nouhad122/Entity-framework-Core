namespace EFCore.Entities
{
    public class Syllabus
    {
        public int SyllabusId { get; set; }
        public string Description { get; set; }
        public Course Course { get; set; }
    }
}
