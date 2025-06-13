using Bloggy.web.Data;
using Bloggy.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Repository
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggieDBContext bloggieDBContext;

        public BlogPostCommentRepository(BloggieDBContext bloggieDBContext)
        {
            this.bloggieDBContext = bloggieDBContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment comment)
        {
            await bloggieDBContext.BlogPostComment.AddAsync(comment);
            await bloggieDBContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentByIDAsync(Guid id)
        {
            return await bloggieDBContext.BlogPostComment
                .Where(x => x.Id == id).ToListAsync();
        }
    }
}
