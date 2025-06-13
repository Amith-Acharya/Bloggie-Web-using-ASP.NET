using Bloggy.web.Models.ViewModels;
using Bloggy.web.Models.Domain;
using Bloggy.web.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bloggy.web.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BlogPostLikeController : Controller
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository,SignInManager<IdentityUser> signInManager,  UserManager<IdentityUser> userManager)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            //if (addLikeRequest != null)
            //{

            //}
            var model = new BlogPostLikes
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserID = addLikeRequest.UserID
            };
            await blogPostLikeRepository.AddLikeForBlog(model);
            return Ok(new { message = "Like added successfully" });
        }
        [HttpGet]
        [Route("{blogPostID:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikes([FromRoute]Guid blogpostID)
        {
            var like=await blogPostLikeRepository.GetTotalLikes(blogpostID);
            return Ok(like);
        }
    }
}
