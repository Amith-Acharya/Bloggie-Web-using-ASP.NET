using Bloggy.web.Models.Domain;

namespace Bloggy.web.Repository
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment comment);
        Task<IEnumerable<BlogPostComment>> GetCommentByIDAsync(Guid id);
    }
}
