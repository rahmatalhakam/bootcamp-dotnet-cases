namespace Twittor.Dtos
{
  public class UpdatePassInput
  {
    public string Username { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }

  }
}