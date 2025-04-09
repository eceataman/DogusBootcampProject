using DogusBootcampProject.Models;

namespace DogusBootcampProject.Repositories
{
	public interface IBlogRepository : IRepository<Blog>
	{
		Task<IEnumerable<Blog>> GetAllWithUserAndCategoryAsync();
	}

}
