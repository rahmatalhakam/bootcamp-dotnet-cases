using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Twittor.Models
{
  public class TwittorModel
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(280)]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    public User User { get; set; }

    public ICollection<Comment> Comments { get; set; }
  }
}