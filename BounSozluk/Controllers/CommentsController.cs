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

namespace BounSozluk.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comment.Include(c => c.Post);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);

            ViewBag.PostId = post.Id;
            ViewBag.PostTitle = post.Title;

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,BounSozlukUserId,PostId")] Comment comment)
        {
            var currentUser = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            comment.BounSozlukUserId = currentUser.Id;
            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.Now;

                comment.Id = 0;

                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }

            var post = await _context.Post.FindAsync(comment.PostId);

            ViewBag.PostId = post.Id;
            ViewBag.PostTitle = post.Title;

            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id", comment.PostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,BounSozlukUserId,PostId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
        
        [HttpGet]
        public async Task<IActionResult> LikeComment(int id)
        {
            var currentUser = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var like = _context.Like.Where(l => l.BounSozlukUserId == currentUser.Id && l.CommentId == id).FirstOrDefault();

            if(like != null)
            {
                _context.Like.Remove(like);
            }
            else
            {
                var newLike = new Like();
                newLike.BounSozlukUserId = currentUser.Id;
                newLike.CommentId = id;

                _context.Like.Add(newLike);
            }

            await _context.SaveChangesAsync();

            var comment = await _context.Comment.FindAsync(id);

            return RedirectToAction("Details", "Posts", new { id = comment.PostId });
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            var currentUserId = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault().Id;

            var comment = _context.Comment.Where(c => c.Id == id).FirstOrDefault();

            if(comment == null || (comment.BounSozlukUserId != currentUserId && !User.IsInRole("admin")))
            {
                return NotFound();
            }

            var likes = _context.Like.Where(l => l.CommentId == id).ToList();

            if(likes != null)
            {
                foreach (var like in likes)
                {
                    _context.Like.Remove(like);
                }
            }

            await _context.SaveChangesAsync();

            _context.Comment.Remove(comment);

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Posts", new { id = comment.PostId });
        }
    }
}
