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

        public async Task<IActionResult> Index(int? categoryId)
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync();

            var blogs = await _blogRepository.GetAllWithUserAndCategoryAsync();

            if (categoryId.HasValue)
            {
                blogs = blogs.Where(b => b.CategoryId == categoryId.Value).ToList();
            }

            return View(blogs);
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
