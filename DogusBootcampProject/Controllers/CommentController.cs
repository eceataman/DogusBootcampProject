using DogusBootcampProject.Data;
using DogusBootcampProject.Models;
using DogusBootcampProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogusBootcampProject.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly BlogDbContext _context;
        private readonly UserManager<User> _userManager;

        public CommentController(BlogDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReplyToComment(int ParentCommentId, int BlogId, string ReplyContent)
        {
            var adminUser = await _userManager.GetUserAsync(User);

            var reply = new Comment
            {
                Content = ReplyContent,
                BlogId = BlogId,
                ParentCommentId = ParentCommentId,
                UserId = adminUser.Id,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(reply);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Blog", new { id = BlogId });
        }


        [HttpPost]
        public async Task<IActionResult> Add(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Blog", new { id = model.BlogId });
            }

            var user = await _userManager.GetUserAsync(User);

            var comment = new Comment
            {
                Content = model.Content,
                BlogId = model.BlogId,
                UserId = user.Id,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Blog", new { id = model.BlogId });
        }
    }
}
