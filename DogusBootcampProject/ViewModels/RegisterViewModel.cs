using System.ComponentModel.DataAnnotations;

namespace DogusBootcampProject.ViewModels;
public class RegisterViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email boş bırakılamaz.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Şifre tekrar gerekli.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
    public string ConfirmPassword { get; set; }
}
