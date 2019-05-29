using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BounSozluk.Data;
using BounSozluk.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BounSozluk.Controllers
{
    public class PostGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PostGroupsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: PostGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostGroup.ToListAsync());
        }

        // GET: PostGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postGroup = await _context.PostGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postGroup == null)
            {
                return NotFound();
            }

            return View(postGroup);
        }

        public async Task<IActionResult> PostList(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var postGroup = await _context.PostGroup
                .FirstOrDefaultAsync(m => m.Name == id);
            if (postGroup == null)
            {
                return NotFound();
            }

            ViewBag.PostGroupName = postGroup.Name;

            var postListViewModel = new PostListViewModel();

            var posts = _context.Post.Where(p => p.PostGroupId == postGroup.Id).Include(p => p.BounSozlukUser).Include(p => p.Comments).ToList();

            if(posts != null && posts.Count() > 0)
            {
                postListViewModel.Posts = posts;
            }
            else
            {
                postListViewModel.Posts = new List<Post>();
            }

            return View(postListViewModel);
        }

        // GET: PostGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] PostGroup postGroup, IFormFile FileUrl)
        {
            if (ModelState.IsValid)
            {
                if (FileUrl != null)
                {
                    string dirPath = Path.Combine(_hostingEnvironment.WebRootPath, @"uploads\");
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + FileUrl.FileName;
                    using (var fileStream = new FileStream(dirPath + fileName, FileMode.Create))
                    {
                        await FileUrl.CopyToAsync(fileStream);
                    }

                    postGroup.PhotoUrl = fileName;
                }

                _context.Add(postGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postGroup);
        }

        // GET: PostGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postGroup = await _context.PostGroup.FindAsync(id);
            if (postGroup == null)
            {
                return NotFound();
            }
            return View(postGroup);
        }

        // POST: PostGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PostGroup postGroup)
        {
            if (id != postGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostGroupExists(postGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postGroup);
        }

        // GET: PostGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postGroup = await _context.PostGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postGroup == null)
            {
                return NotFound();
            }

            return View(postGroup);
        }

        // POST: PostGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postGroup = await _context.PostGroup.FindAsync(id);
            _context.PostGroup.Remove(postGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostGroupExists(int id)
        {
            return _context.PostGroup.Any(e => e.Id == id);
        }
    }
}
