using System.ComponentModel.DataAnnotations;

namespace TwittorDAL.Models
{
  public class UserRole
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public int RoleId { get; set; }

    [Required]
    public int UserId { get; set; }

    public User User { get; set; }

    public Role Role { get; set; }
  }
}