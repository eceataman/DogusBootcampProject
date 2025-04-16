using System.ComponentModel.DataAnnotations;

namespace DogusBootcampProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yorum boş olamaz.")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        public List<Comment> Replies { get; set; }  // <-- Burası önemli

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
