using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BounSozluk.Data;
using BounSozluk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace BounSozluk.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BounSozlukUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PostsController(ApplicationDbContext context, UserManager<BounSozlukUser> userManager, IHostingEnvironment hostingEnvironment, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Post.Include(p => p.PostGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.Include(p => p.Comments).ThenInclude(c => c.Likes).ThenInclude(c => c.BounSozlukUser)
                .Include(p => p.PostGroup).Include(p => p.BounSozlukUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            if (User != null)
            {
                var email = User.Identity.Name;
                var currentUser = _context.Users.Where(u => u.Email == email).FirstOrDefault();

                if(currentUser != null)
                {
                    ViewData["IsAdmin"] = User.IsInRole("admin");
                    ViewData["CurrentUserId"] = currentUser.Id;
                }                
            }    
            else
            {
                ViewData["IsAdmin"] = false;
                ViewData["CurrentUserId"] = "";
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["PostGroupId"] = new SelectList(_context.PostGroup, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,PostGroupId,BounSozlukUserId")] Post post, IFormFile FileUrl)
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

                    post.PhotoUrl = fileName;
                }

                var email = User.Identity.Name;

                var currentUser = _context.Users.Where(u => u.Email == email).FirstOrDefault();
                post.BounSozlukUserId = currentUser.Id;

                post.CreateDate = DateTime.Now;

                _context.Add(post);
                await _context.SaveChangesAsync();

                var postGroup = _context.PostGroup.Where(p => p.Id == post.PostGroupId).FirstOrDefault();

                return RedirectToAction("PostList", "PostGroups", new { id = postGroup.Name });
            }
            ViewData["PostGroupId"] = new SelectList(_context.PostGroup, "Id", "Id", post.PostGroupId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["PostGroupId"] = new SelectList(_context.PostGroup, "Id", "Id", post.PostGroupId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PostGroupId,BounSozlukUserId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["PostGroupId"] = new SelectList(_context.PostGroup, "Id", "Id", post.PostGroupId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.PostGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // // GET: Posts/AddComment/5
        public async Task<IActionResult> AddComment(int id)
        {
            var post = await _context.Post.FindAsync(id);
            ViewData["PostTitle"] = post.Title;
            ViewData["PostId"] = post.Id;
            return View();
        }
        [Authorize]
        // POST: Posts/AddComment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment([Bind("Content,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var email = User.Identity.Name;

                var currentUser = _context.Users.Where(u => u.Email == email).FirstOrDefault();
                comment.BounSozlukUserId = currentUser.Id;

                _context.Add(comment);

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }

            var post = await _context.Post.FindAsync(comment.PostId);
            ViewData["PostTitle"] = post.Title;
            ViewData["PostId"] = post.Id;
            return View();
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeletePost(int id)
        {
            var currentUserId = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault().Id;

            var post = await _context.Post.FindAsync(id);

            if (post == null || (post.BounSozlukUserId != currentUserId && !User.IsInRole("admin")))
            {
                return NotFound();
            }

            var comments = _context.Comment.Where(c => c.PostId == id).ToList();

            if(comments != null && comments.Count() > 0)
            {
                foreach (var comment in comments)
                {
                    var likes = _context.Like.Where(l => l.CommentId == comment.Id).ToList();

                    if(likes != null && likes.Count() > 0)
                    {
                        foreach (var like in likes)
                        {
                            _context.Like.Remove(like);
                            await _context.SaveChangesAsync();
                        }
                    }

                    _context.Comment.Remove(comment);
                    await _context.SaveChangesAsync();
                }
            }

            var postGroupName = _context.PostGroup.Find(post.PostGroupId).Name;

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("PostList", "PostGroups", new { id = postGroupName });
        }
    }
}
