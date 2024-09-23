using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.Models;
public class UserModel {
    public const int MAX_USERNAME_LENGTH = 40;
    public const int MAX_PASSWORD_LENGTH = 40;

    public int ID { get; set; }
    public string? FirstName { get; set; }

    public string? Surname { get; set; }

    [Unique(ErrorMessage = "Username is already taken")]
    [Required(ErrorMessage = "Username is required")]
    [StringLength(MAX_USERNAME_LENGTH, ErrorMessage = $"Username cannot exceed 40 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(MAX_PASSWORD_LENGTH, ErrorMessage = "Password cannot exceed 40 characters")]
    public string Password { get; set; }
}