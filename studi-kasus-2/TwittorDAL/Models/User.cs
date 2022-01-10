using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Twittor.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(50)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }


    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }


    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    [Required]
    public Boolean Lock { get; set; }


    public ICollection<UserRole> UserRoles { get; set; }

    public ICollection<TwittorModel> TwittorModels { get; set; }

    public ICollection<Comment> Comments { get; set; }

  }
}