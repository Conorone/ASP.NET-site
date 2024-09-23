public class CommentModel {
    public int commentID { get; set; }
    public required string comment { get; set; }
    public DateTime date { get; set; }
    public int userID { get; set; }
}