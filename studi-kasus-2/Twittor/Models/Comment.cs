using System;
using System.ComponentModel.DataAnnotations;

namespace Twittor.Models
{
  public class Comment
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(280)]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    public Twittor Twittor { get; set; }
    public User User { get; set; }
  }
}