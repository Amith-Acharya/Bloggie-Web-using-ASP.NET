namespace Bloggy.web.Models.Domain
{
    public class BlogPostLikes
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserID { get; set; }
    }
}
