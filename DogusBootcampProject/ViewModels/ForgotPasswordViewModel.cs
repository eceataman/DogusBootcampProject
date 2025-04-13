using System.ComponentModel.DataAnnotations;
namespace DogusBootcampProject.ViewModels;
public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
