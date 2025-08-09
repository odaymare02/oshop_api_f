using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oshop.DAL.Data;
using Oshop.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.DAL.Utalities
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;//to check if migration added or not
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task DataSeedingAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())//if have any migration don't apply apply it
            {
                await _context.Database.MigrateAsync();
            }
            if (!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Category { Name = "Clothes" },
                    new Category { Name = "Mobiles" },
                    new Category { Name = "Labtop" }
                    );
            }
            if (!await _context.Brands.AnyAsync())
            {
               await _context.Brands.AddRangeAsync(
                    new Brand { Name="Nike"},
                    new Brand { Name="Apple"},
                    new Brand { Name = "Samsung" }
                    );
            }
            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if(! await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            if(!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "soso@gmail.com",
                    FullName = "soso samo",
                    UserName = "sosGb",
                    EmailConfirmed = true,
                };
                var user2 = new ApplicationUser()
                {
                    Email = "so@gmail.com",
                    FullName = "soso samo",
                    UserName = "sos",
                    EmailConfirmed = true,

                };
                await _userManager.CreateAsync(user1,"Oo@1234");
                await _userManager.CreateAsync(user2,"Oo@1234");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
            }
            await _context.SaveChangesAsync();
        }
    }
}
