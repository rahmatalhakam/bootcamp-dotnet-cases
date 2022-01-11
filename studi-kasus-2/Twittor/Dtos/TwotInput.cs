

using System.ComponentModel.DataAnnotations;

namespace Twittor.Dtos
{
  public class TwotInput
  {
    [Required]
    public string Description { get; set; }

    [Required]
    public int UserID { get; set; }
  }
}