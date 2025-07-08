namespace EFCore.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int AssignmentId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CommentContent { get; set; }

        public Assignment Assignment { get; set; }
        public User User { get; set; }

    }
}
