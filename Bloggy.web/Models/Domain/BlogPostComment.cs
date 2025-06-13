namespace Bloggy.web.Models.Domain
{
    public class BlogPostComment
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid BlogPostID { get; set; }
        public Guid UserID { get; set; }

    }
}
