using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Data
{
    public class AuothDBContext : IdentityDbContext
    {
        public AuothDBContext(DbContextOptions<AuothDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //seeding user admin and superadmin-roles --- Identity role
            var userid = "8bd1fb22-bf75-4cd9-bb63-3fb862e251bf";
            var adminid = "c55a2c9b-23c1-42e5-ae73-ec02094a912a";
            var superadminid = "f4f43d9a-16c7-4d8e-966f-0a2a2eb30e4c";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id=adminid,
                    ConcurrencyStamp=adminid
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id=userid,
                    ConcurrencyStamp=userid
                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superadminid,
                    ConcurrencyStamp=superadminid
                }

            };
            builder.Entity<IdentityRole>().HasData(roles);
            // seeding superadmin --- Identity user
            var Isuperadminid = "9cd7d888-4805-42b6-a9c9-85f4496b674f";
            var Iroles = new IdentityUser
            {
                Id = Isuperadminid,
                UserName = "superuser@bloggie.com",
                NormalizedUserName = "SUPERUSER@BLOGGIE.COM",
                Email = "superuser@bloggie.com",
                NormalizedEmail = "SUPERUSER@BLOGGIE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEMQiFlt4O7QEMBS9CsiR/YLY6gkNRwKH++EoZiBdohever9JSrwqGh/b8TherIPYkw==",
                SecurityStamp = "e64bd123-4a89-4fda-8540-627af90d5caa",
                ConcurrencyStamp = "52c9f5c1-f0e7-4cb5-96b8-798c00ee5ef3"

            };
            


            builder.Entity<IdentityUser>().HasData(Iroles);
            //add all roles to the super admin
            var superroles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>{ 
                    
                    RoleId=userid,
                    UserId=Isuperadminid
                },
                new IdentityUserRole<string>{

                    RoleId=adminid,
                    UserId=Isuperadminid
                },
                new IdentityUserRole<string>{

                    RoleId=superadminid,
                    UserId=Isuperadminid
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superroles);
        }
    }
}
