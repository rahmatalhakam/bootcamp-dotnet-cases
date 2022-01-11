using System.ComponentModel.DataAnnotations;

namespace Twittor.Dtos
{
  public class LoginInput
  {
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}