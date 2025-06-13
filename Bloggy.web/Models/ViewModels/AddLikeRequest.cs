namespace Bloggy.web.Models.ViewModels
{
    public class AddLikeRequest
    {
        public Guid BlogPostId  { get; set; }
        public Guid UserID  { get; set; }
    }
}
