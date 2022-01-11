using System.ComponentModel.DataAnnotations;

namespace Twittor.Dtos
{
  public class UserRoleUpdate
  {
    [Required]
    public int OldRoleId { get; set; }
    [Required]
    public int NewRoleId { get; set; }
    [Required]
    public int UserId { get; set; }
  }
}