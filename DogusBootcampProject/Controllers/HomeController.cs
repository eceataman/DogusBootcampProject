using DogusBootcampProject.Models;
using DogusBootcampProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DogusBootcampProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository _blogRepository;
        private readonly IRepository<Category> _categoryRepository;
        public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepository, IRepository<Category> categoryRepository)
        {
			_logger = logger;
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.GetAllWithUserAndCategoryAsync();
            ViewBag.Categories = await _categoryRepository.GetAllAsync(); // bu sayede ViewBag.Categories dolacak
            return View(blogs);
        }

        public async Task<IActionResult> Search(int? categoryId, string? search)
        {
            var blogs = await _blogRepository.GetAllWithUserAndCategoryAsync();
            ViewBag.Categories = await _categoryRepository.GetAllAsync();



            if (categoryId.HasValue)
            {
                blogs = blogs.Where(b => b.CategoryId == categoryId.Value).ToList();
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                blogs = blogs.Where(b =>
                    b.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    b.Content.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return PartialView("_BlogResults", blogs); // Burasý aþaðýda geliyor ??
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
