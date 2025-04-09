using DogusBootcampProject.Data;
using DogusBootcampProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DogusBootcampProject.Repositories
{
	public class BlogRepository : Repository<Blog>, IBlogRepository
	{
		public BlogRepository(BlogDbContext context) : base(context) { }

		public async Task<IEnumerable<Blog>> GetAllWithUserAndCategoryAsync()
		{
			return await _context.Blogs
				.Include(b => b.User)
				.Include(b => b.Category)
				.ToListAsync();
		}
	}

}
