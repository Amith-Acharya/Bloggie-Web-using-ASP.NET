using Azure;
using Bloggy.web.Data;
using Bloggy.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDBContext bloggieDBContext;

        public TagRepository(BloggieDBContext bloggieDBContext)
        {
            this.bloggieDBContext = bloggieDBContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDBContext.Tags.AddAsync(tag);
            await bloggieDBContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await bloggieDBContext.Tags.FindAsync(id);
            if (tag != null)
            {
                bloggieDBContext.Tags.Remove(tag);
                await bloggieDBContext.SaveChangesAsync();
                return tag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return  await bloggieDBContext.Tags.ToListAsync();
            
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await bloggieDBContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existing= await bloggieDBContext.Tags.FindAsync(tag.Id);
            if (existing != null)
            {
                existing.Name = tag.Name;
                existing.Displayname = tag.Displayname;
                await bloggieDBContext.SaveChangesAsync();
                return existing;
            }
            return null;    
        }
    }
}
