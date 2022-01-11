using System.ComponentModel.DataAnnotations;

namespace Twittor.Dtos
{
  public class UserRoleInput
  {
    [Required]
    public int RoleId { get; set; }
    [Required]
    public int UserId { get; set; }
  }
}