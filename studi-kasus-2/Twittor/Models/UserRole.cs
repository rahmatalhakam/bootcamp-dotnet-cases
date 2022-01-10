using System.ComponentModel.DataAnnotations;

namespace Twittor.Models
{
  public class UserRole
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public int RolesId { get; set; }

    [Required]
    public int UsersId { get; set; }

    public User User { get; set; }

    public Role Role { get; set; }
  }
}