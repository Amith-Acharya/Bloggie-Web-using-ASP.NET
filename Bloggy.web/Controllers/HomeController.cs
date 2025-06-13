using System.Diagnostics;
using System.Threading.Tasks;
using Bloggy.web.Models;
using Bloggy.web.Models.ViewModels;
using Bloggy.web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger,IBlogPostRepository blogPostRepository,ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await blogPostRepository.GetAllAsync();
            var tags= await tagRepository.GetAllAsync();
            var model = new HomeBlogTag
            {
                BlogPosts = blogs,
                Tags = tags
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
