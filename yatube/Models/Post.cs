namespace yatube.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public string PubDate { get; set; }
        public int AuthorId { get; set; }
        public Nullable<int> GroupId { get; set; }
    }
}
