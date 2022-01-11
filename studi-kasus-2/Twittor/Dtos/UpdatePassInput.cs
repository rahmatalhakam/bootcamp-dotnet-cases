using System.ComponentModel.DataAnnotations;

namespace Twittor.Dtos
{
  public class UpdatePassInput
  {
    [Required]
    public string Username { get; set; }
    [Required]
    public string OldPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }

  }
}