using Bloggy.web.Models.Domain;

namespace Bloggy.web.Models.ViewModels
{
    public class HomeBlogTag
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
