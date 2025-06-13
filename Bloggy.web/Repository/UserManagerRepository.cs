using Bloggy.web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Repository
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly AuothDBContext auothDBContext;

        public UserManagerRepository(AuothDBContext auothDBContext)
        {
            this.auothDBContext = auothDBContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var all=await auothDBContext.Users.ToListAsync();
            var superAdmin = await auothDBContext.Users.FirstOrDefaultAsync(x => x.Email == "superuser@bloggie.com");
            if (superAdmin != null)
            {
                all.Remove(superAdmin);
            }
            return all;
        }
    }
}
