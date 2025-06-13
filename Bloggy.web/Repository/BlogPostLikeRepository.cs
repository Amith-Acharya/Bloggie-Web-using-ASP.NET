
using Bloggy.web.Data;
using Bloggy.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Repository
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDBContext bloggieDBContext;

        public BlogPostLikeRepository(BloggieDBContext bloggieDBContext)
        {
            this.bloggieDBContext = bloggieDBContext;
        }

        public async Task<BlogPostLikes> AddLikeForBlog(BlogPostLikes blogpost)
        {
            await bloggieDBContext.BlogPostLike.AddAsync(blogpost);
            await bloggieDBContext.SaveChangesAsync();
            return blogpost;
        }

        public async Task<IEnumerable<BlogPostLikes>> GetLikesForBlog(Guid blogpostId)
        {
            return await bloggieDBContext.BlogPostLike.Where(x=>x.BlogPostId==blogpostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogpostId)
        {
            return await bloggieDBContext.BlogPostLike.CountAsync(x=>x.BlogPostId==blogpostId);
        }
    }
}
