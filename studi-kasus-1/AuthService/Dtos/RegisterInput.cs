using System.ComponentModel.DataAnnotations;

namespace AuthService.Dtos
{
  public class RegisterInput
  {
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
  }
}
