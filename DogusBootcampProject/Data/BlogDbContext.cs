using DogusBootcampProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DogusBootcampProject.Data
{
	public class BlogDbContext:DbContext
	{
		public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Fluent API ile ilişkiler tanımlanabilir, ama modellerde navigation property'ler yeterli olabilir.
			base.OnModelCreating(modelBuilder);
		}
	}


}
