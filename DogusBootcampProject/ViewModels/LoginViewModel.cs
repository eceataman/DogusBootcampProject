using System.ComponentModel.DataAnnotations;

namespace DogusBootcampProject.ViewModels;
public class LoginViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı giriniz.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Şifre giriniz.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
