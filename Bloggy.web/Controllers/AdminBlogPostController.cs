using Bloggy.web.Models.Domain;
using Bloggy.web.Models.ViewModels;
using Bloggy.web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bloggy.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Displayname, Value = x.Id.ToString() })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var post = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };
            var t = new List<Tag>();
            foreach (var id in addBlogPostRequest.SelectedTag)
            {
                var tag = await tagRepository.GetAsync(Guid.Parse(id));
                if (tag != null)
                {
                    t.Add(tag);
                }
            }
            post.Tags = t;
            var e=await blogPostRepository.AddAsync(post);
            
            return RedirectToAction("List");

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var model = await blogPostRepository.GetAllAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var post = await blogPostRepository.GetAsync(id);
            var tags = await tagRepository.GetAllAsync();
            //map the domain model to view model
            if (post != null)
            {
                var model = new EditBlogPostRequest
                {
                    id = post.id,
                    Heading = post.Heading,
                    PageTitle = post.PageTitle,
                    Content = post.Content,
                    ShortDescription = post.ShortDescription,
                    FeaturedImageUrl = post.FeaturedImageUrl,
                    UrlHandle = post.UrlHandle,
                    PublishedDate = post.PublishedDate,
                    Author = post.Author,
                    Visible = post.Visible,
                    Tags = tags.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTag = post.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var p = await blogPostRepository.GetAsync(editBlogPostRequest.id);
            if (p != null)
            {
                var post = new BlogPost
                {
                    id = editBlogPostRequest.id,

                    Heading = editBlogPostRequest.Heading,
                    PageTitle = editBlogPostRequest.PageTitle,
                    Content = editBlogPostRequest.Content,
                    ShortDescription = editBlogPostRequest.ShortDescription,
                    FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                    UrlHandle = editBlogPostRequest.UrlHandle,
                    PublishedDate = editBlogPostRequest.PublishedDate,
                    Author = editBlogPostRequest.Author,
                    Visible = editBlogPostRequest.Visible };
                var t = new List<Tag>();
                foreach (var id in editBlogPostRequest.SelectedTag)
                {
                    var tag = await tagRepository.GetAsync(Guid.Parse(id));
                    if (tag != null)
                    {
                        t.Add(tag);
                    }
                }
                post.Tags = t;
                await blogPostRepository.UpdateAsync(post);
                TempData["SuccessMessage"] = "Saved successfully!";

                return RedirectToAction("List");
            }
            TempData["Error"] = "Something went wrong.";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await blogPostRepository.GetAsync(id);
            if (post != null)
            {
                await blogPostRepository.DeleteAsync(id);
                TempData["SuccessMessage"] = "Deleted successfully!";
                return RedirectToAction("List");
            }
            TempData["Error"] = "Something went wrong.";
            return RedirectToAction("List");
        }

        }
}
