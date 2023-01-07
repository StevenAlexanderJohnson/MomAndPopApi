namespace Api.Models.Response
{
    public class PostResponse
    {
        public Int64 Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Attachment { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserName { get; set; }
    }
}
