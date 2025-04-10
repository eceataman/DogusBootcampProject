using Microsoft.AspNetCore.Identity;

namespace DogusBootcampProject.Models
{
	public class User : IdentityUser<int>
	{
		public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
