using Bloggy.web.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggy.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageUploadRepository imageUploadRepository;

        public ImagesController(IImageUploadRepository imageUploadRepository)
        {
            this.imageUploadRepository = imageUploadRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadimageAsync(IFormFile file)
        {
            var url=  await imageUploadRepository.UploadImageAsync(file);
            if(url==null)
            {
                return Problem("Image upload failed.",null,(int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link=url });

        }
    }
}
