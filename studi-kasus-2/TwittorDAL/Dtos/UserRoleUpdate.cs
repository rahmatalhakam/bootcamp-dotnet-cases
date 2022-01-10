namespace TwittorDAL.Dtos
{
  public class UserRoleUpdate
  {
    public int OldRoleId { get; set; }
    public int NewRoleId { get; set; }
    public int UserId { get; set; }
  }
}