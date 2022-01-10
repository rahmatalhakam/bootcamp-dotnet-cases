namespace TwittorDAL.Dtos
{
  public class UserRoleUpdate
  {
    public int OldRolesId { get; set; }
    public int NewRolesId { get; set; }
    public int UsersId { get; set; }
  }
}