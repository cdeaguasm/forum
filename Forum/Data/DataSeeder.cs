using Data.Models;
using Forum.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedSuperUser()
        {
            var user = new ApplicationUser
            {
                UserName = "Admin",
                NormalizedUserName = "admin",
                Email = "admin@forum.com",
                NormalizedEmail = "admin@forum.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = hasher.HashPassword(user, "admin");
            user.PasswordHash = hashedPassword;

            var storeRole = new RoleStore<IdentityRole>(_context);
            var storeUser = new UserStore<IdentityUser>(_context);

            var hasAdminRole = await _context.Roles.AnyAsync(r => r.Name == "Admin");

            if (!hasAdminRole)
            {
                await storeRole.CreateAsync(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "admin",
                });
            }

            var hasSuperUser = await _context.Users.AnyAsync(u => u.NormalizedUserName == user.UserName);

            if (!hasSuperUser)
            {
                await storeUser.CreateAsync(user);
                await storeUser.AddToRoleAsync(user, "admin");
            }

            await _context.SaveChangesAsync();
        }
    }
}
