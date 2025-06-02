using Bloggy.web.Data;
using Bloggy.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDBContext bloggieDBContext;
       

        public BlogPostRepository(BloggieDBContext bloggieDBContext)
        {
            this.bloggieDBContext = bloggieDBContext;
           
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggieDBContext.AddAsync(blogPost);
            await bloggieDBContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var tag = await bloggieDBContext.BlogPosts.FindAsync(id);
            if (tag != null)
            {
                bloggieDBContext.BlogPosts.Remove(tag);
                await bloggieDBContext.SaveChangesAsync();
                return tag;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDBContext.BlogPosts.Include(x => x.Tags).ToListAsync();

        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggieDBContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existing=await bloggieDBContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.id==blogPost.id);
            if(existing != null)
            {
                existing.Heading = blogPost.Heading;
                existing.PageTitle = blogPost.PageTitle;
                existing.Content = blogPost.Content;
                existing.ShortDescription = blogPost.ShortDescription;
                existing.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existing.UrlHandle = blogPost.UrlHandle;
                existing.PublishedDate = blogPost.PublishedDate;
                existing.Author = blogPost.Author;
                existing.Visible = blogPost.Visible;
                existing.Tags = blogPost.Tags; // Assuming Tags is a collection and handled correctly
                
                await bloggieDBContext.SaveChangesAsync();
                return existing;
            }
            return null;
        }
    }
}
