using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BounSozluk.Models;
using BounSozluk.Data;
using Microsoft.EntityFrameworkCore;

namespace BounSozluk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();

            viewModel.LastPosts = new List<Post>();
            viewModel.LastPosts = _context.Post.Include(p => p.Comments).OrderByDescending(p => p.CreateDate).Take(10).ToList();

            Random rnd = new Random();
            int randomPost = -1;

            if (viewModel.LastPosts != null && viewModel.LastPosts.Count() > 0)
            {
                randomPost = rnd.Next(viewModel.LastPosts.Count);
                viewModel.RandomPost = viewModel.LastPosts[randomPost];
            }
            else
            {
                viewModel.RandomPost = new Post();
            }  

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
