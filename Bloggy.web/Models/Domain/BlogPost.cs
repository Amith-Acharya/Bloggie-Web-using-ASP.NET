﻿namespace Bloggy.web.Models.Domain
{
    public class BlogPost
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
        //navigation properties
        public ICollection<Tag> Tags { get; set; }
        public ICollection<BlogPostLikes> Likes { get; set; }
        public ICollection<BlogPostComment> BlogPostComment { get; set; }
    }
}
