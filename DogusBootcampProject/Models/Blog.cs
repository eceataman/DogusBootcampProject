namespace DogusBootcampProject.Models
{
	public class Blog
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public DateTime PublishDate { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public string ImageUrl { get; set; }
	}
}
