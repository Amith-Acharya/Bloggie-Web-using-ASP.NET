using Bloggy.web.Models.ViewModels;
using Bloggy.web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bloggy.web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminUserManagerController : Controller
    {
        private readonly IUserManagerRepository userManagerRepository;

        public AdminUserManagerController(IUserManagerRepository userManagerRepository)
        {
            this.userManagerRepository = userManagerRepository;
        }
        public async Task<IActionResult> List()
        {
            var allusers = await userManagerRepository.GetAll();
            var model = new ListofUser();
            model.Users = new List<User>();
            foreach (var item in allusers)
            {
                model.Users.Add(new User
                {
                    Id = Guid.Parse(item.Id),
                    Username = item.UserName ?? string.Empty, // Fix for CS8601: Provide a default value
                    email = item.Email ?? string.Empty       // Fix for CS8601: Provide a default value
                });
            }

            return View(model);
        }
    }
}
