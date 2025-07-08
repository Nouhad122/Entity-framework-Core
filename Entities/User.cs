namespace EFCore.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Grade> Grades { get; set; }

    }
}
