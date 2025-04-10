using System.ComponentModel.DataAnnotations;

namespace DogusBootcampProject.ViewModels;
public class BlogEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık boş bırakılamaz.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "İçerik boş bırakılamaz.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Kategori seçilmelidir.")]
    public int CategoryId { get; set; }

    public string? ImageUrl { get; set; } // Görsel zorunlu değil düzenleme için
}
