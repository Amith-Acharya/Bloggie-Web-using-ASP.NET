using Bloggy.web.Models.Domain;
using Bloggy.web.Models.ViewModels;
using Bloggy.web.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bloggy.web.Controllers
{
    public class BlogDisplayController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public BlogDisplayController(IBlogPostRepository blogPostRepository,IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager,IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blog = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var liked = false;
            var model = new BlogPostLikeViewModel();
            if (blog == null)
            {
                return NotFound();
            }
            if (blog != null) {
                if (signInManager.IsSignedIn(User))
                {
                    var like=await blogPostLikeRepository.GetLikesForBlog(blog.id);
                    var id=userManager.GetUserId(User);
                    if (id != null)
                    {
                        var likeforuser=like.FirstOrDefault(x => x.UserID == Guid.Parse(id));
                        liked= likeforuser != null;
                    }
                }
                var Allcomments=await blogPostCommentRepository.GetCommentByIDAsync(blog.id);
                var list=new List<BlogComment>();
                foreach (var comment in Allcomments)
                {
                    
                        list.Add(new BlogComment
                        {
                            Comment = comment.Comment,
                             CommentedOn= comment.CreatedAt,
                             Username= (await userManager.FindByIdAsync(comment.UserID.ToString())).UserName
                        });
                    
                }

                model = new BlogPostLikeViewModel
                {
                    id = blog.id,
                    Heading = blog.Heading,
                    PageTitle = blog.PageTitle,
                    Content = blog.Content,
                    ShortDescription = blog.ShortDescription,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    UrlHandle = blog.UrlHandle,
                    PublishedDate = blog.PublishedDate,
                    Author = blog.Author,
                    Visible = blog.Visible,
                    Tags = blog.Tags,
                    TotalLikes = await blogPostLikeRepository.GetTotalLikes(blog.id),
                    Liked = liked,
                    allcomments=list

                };

                
            }
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Index(BlogPostLikeViewModel blogPostLikeViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var comment = new BlogPostComment
                {
                    Comment= blogPostLikeViewModel.Comment,
                    CreatedAt=DateTime.Now,
                    BlogPostID=blogPostLikeViewModel.id,
                    UserID=Guid.Parse(userManager.GetUserId(User))
                };
                await blogPostCommentRepository.AddAsync(comment);
                return RedirectToAction("Index","BlogDisplay", new { urlHandle = blogPostLikeViewModel.UrlHandle });

            }
            return View();
        }
    }
}
