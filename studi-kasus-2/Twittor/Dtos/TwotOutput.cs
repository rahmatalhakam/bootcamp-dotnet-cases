

using System;
using System.ComponentModel.DataAnnotations;

namespace Twittor.Dtos
{
  public class TwotOutput
  {
    public int Id { get; set; }
    public string Description { get; set; }

    public int UserID { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}