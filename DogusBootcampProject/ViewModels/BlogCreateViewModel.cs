using System.ComponentModel.DataAnnotations;

namespace DogusBootcampProject.ViewModels;
public class BlogCreateViewModel
{
    [Required(ErrorMessage = "Başlık boş bırakılamaz.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "İçerik boş bırakılamaz.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Kategori seçmelisiniz.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Görsel yüklenmesi zorunludur.")]
    public IFormFile ImageFile { get; set; }
}
