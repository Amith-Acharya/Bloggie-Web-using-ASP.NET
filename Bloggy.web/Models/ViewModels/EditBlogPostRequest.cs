﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggy.web.Models.ViewModels
{
    public class EditBlogPostRequest
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
        public IEnumerable<SelectListItem> Tags { get; set; }
        public string[] SelectedTag { get; set; } = Array.Empty<string>();

    }
}
