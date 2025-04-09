using DogusBootcampProject.Models;
using DogusBootcampProject.Repositories;
using DogusBootcampProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogusBootcampProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly UserManager<User> _userManager;

        public BlogController(IBlogRepository blogRepository, IRepository<Category> categoryRepository, UserManager<User> userManager)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
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
            var blog = await _blogRepository.GetByIdAsync(id);
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
        public async Task<IActionResult> Edit(BlogEditViewModel model)
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
            blog.ImageUrl = model.ImageUrl;

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
