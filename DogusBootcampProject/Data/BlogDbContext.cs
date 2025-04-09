using DogusBootcampProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogusBootcampProject.Data
{
	public class BlogDbContext : IdentityDbContext<User, IdentityRole<int>, int>
	{
		public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
