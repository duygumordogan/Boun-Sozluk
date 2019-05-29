using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BounSozluk.Data;
using BounSozluk.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BounSozluk.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BounSozlukUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(ApplicationDbContext context, UserManager<BounSozlukUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // User management system from CetStore project
        public IActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }
        
        public async Task<ActionResult> MakeAdmin(string id)
        {
            // Role system from CetStore
            if (!(await _roleManager.RoleExistsAsync("admin")))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            }
            if (!(await _roleManager.RoleExistsAsync("author")))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "author" });
            }

            var user = await _userManager.FindByIdAsync(id);

            await _userManager.RemoveFromRoleAsync(user, "author");
            await _userManager.AddToRoleAsync(user, "admin");

            return RedirectToAction("index");
        }

        public async Task<ActionResult> MakeAuthor(string id)
        {
            // Role system from CetStore
            if (!(await _roleManager.RoleExistsAsync("admin")))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            }
            if (!(await _roleManager.RoleExistsAsync("author")))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "author" });
            }

            var user = await _userManager.FindByIdAsync(id);

            await _userManager.RemoveFromRoleAsync(user, "admin");
            await _userManager.AddToRoleAsync(user, "author");

            return RedirectToAction("index");
        }
    }
}