using Bloggy.web.Models.Domain;

namespace Bloggy.web.Models.ViewModels
{
    public class BlogPostLikeViewModel
    {
        public Guid id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string? ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public Boolean Visible { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int TotalLikes { get; set; }
        public Boolean Liked { get; set; }
        public String Comment { get; set; }

        public IEnumerable<BlogComment> allcomments { get; set; }

    }
}
