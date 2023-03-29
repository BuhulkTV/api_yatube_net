namespace yatube.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public string Created { get; set; }
        public int AuthorId { get; set; } 
        public int PostId { get; set; }
    }
}
