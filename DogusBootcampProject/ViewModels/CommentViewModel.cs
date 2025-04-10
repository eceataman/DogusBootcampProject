using System.ComponentModel.DataAnnotations;

namespace DogusBootcampProject.ViewModels
{
    public class CommentViewModel
    {
        [Required(ErrorMessage = "Yorum boş bırakılamaz.")]
        public string Content { get; set; }

        public int BlogId { get; set; }
    }
}
