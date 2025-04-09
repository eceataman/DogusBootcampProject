using System.ComponentModel.DataAnnotations;

namespace DogusBootcampProject.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık boş bırakılamaz.")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik boş bırakılamaz.")]
        public string Content { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Kategori seçmelisiniz.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ImageUrl { get; set; } // required değil!
    }
}
