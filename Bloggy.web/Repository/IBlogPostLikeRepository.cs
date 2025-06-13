using Bloggy.web.Models.Domain;

namespace Bloggy.web.Repository
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogpostId);
        Task<BlogPostLikes> AddLikeForBlog(BlogPostLikes blogpost);
        Task<IEnumerable<BlogPostLikes> >  GetLikesForBlog(Guid blogpostId);
    }
}
