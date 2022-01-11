using System.ComponentModel.DataAnnotations;

namespace Twittor.Dtos
{
  public class RegisterInput
  {
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

  }
}