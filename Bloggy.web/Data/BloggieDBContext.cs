using Bloggy.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Data
{
    public class BloggieDBContext : DbContext
    {
        public BloggieDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
