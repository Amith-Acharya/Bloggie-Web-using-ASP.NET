using Microsoft.AspNetCore.Identity;

namespace Bloggy.web.Repository
{
    public interface IUserManagerRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
