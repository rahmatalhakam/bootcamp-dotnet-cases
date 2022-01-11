using System.ComponentModel.DataAnnotations;

namespace Twittor.Dtos
{
  public class CommentInput
  {
    [Required]
    public string Description { get; set; }

    [Required]
    public int UserID { get; set; }
    [Required]
    public int TwittorModelID { get; set; }
  }
}