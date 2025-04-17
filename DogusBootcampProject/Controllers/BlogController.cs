using DogusBootcampProject.Data;
using DogusBootcampProject.Models;
using DogusBootcampProject.Repositories;
using DogusBootcampProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogusBootcampProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly UserManager<User> _userManager;
        private readonly BlogDbContext _context;

        public BlogController(
            IBlogRepository blogRepository,
            IRepository<Category> categoryRepository,
            UserManager<User> userManager,
            BlogDbContext context)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.GetAllWithUserAndCategoryAsync();
            return View(blogs);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Category)
                .Include(b => b.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null) return NotFound();
            return View(blog);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BlogCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryRepository.GetAllAsync();
                return View(model);
            }

            string imageUrl = null;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);

                imageUrl = "/uploads/" + fileName;
            }

            var user = await _userManager.GetUserAsync(User);

            var blog = new Blog
            {
                Title = model.Title,
                Content = model.Content,
                CategoryId = model.CategoryId,
                ImageUrl = imageUrl,
                UserId = user.Id,
                PublishDate = DateTime.Now
            };

            await _blogRepository.AddAsync(blog);
            await _blogRepository.SaveAsync();

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) return NotFound();

            var model = new BlogEditViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                CategoryId = blog.CategoryId,
                ImageUrl = blog.ImageUrl
            };

            ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(BlogEditViewModel model, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryRepository.GetAllAsync();
                return View(model);
            }

            var blog = await _blogRepository.GetByIdAsync(model.Id);
            if (blog == null) return NotFound();

            blog.Title = model.Title;
            blog.Content = model.Content;
            blog.CategoryId = model.CategoryId;

            if (imageFile != null && imageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(blog.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", blog.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                blog.ImageUrl = "/uploads/" + uniqueFileName;
            }

            _blogRepository.Update(blog);
            await _blogRepository.SaveAsync();

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) return NotFound();

            _blogRepository.Delete(blog);
            await _blogRepository.SaveAsync();

            return RedirectToAction("Index");
        }
    }
}