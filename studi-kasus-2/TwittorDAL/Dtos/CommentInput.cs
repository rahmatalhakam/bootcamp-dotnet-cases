namespace TwittorDAL.Dtos
{
  public class CommentInput
  {
    public string Description { get; set; }

    public int UserID { get; set; }
    public int TwittorModelID { get; set; }
  }
}