using System;

namespace Twittor.Dtos
{
  public class UserOutput
  {
    public int Id { get; set; }
    public string Token { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public Boolean Lock { get; set; }

  }
}