public class Comments {
    public Comments(Int64 id, Int64 userId, Int64 postId, string comment, DateTime date, Int64 upVotes, Int64 downVotes)
    {
        Id = id;
        UserId = userId;
        PostId = postId;
        Comment = comment;
        Date = date;
        UpVotes = upVotes;
        DownVotes = downVotes;
    }

    public Int64 Id { get; set; }
    public Int64 UserId { get; set; }
    public Int64 PostId { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public Int64 UpVotes { get; set; }
    public Int64 DownVotes { get; set; }
}