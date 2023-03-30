namespace yatube.Models
{
    public class CommentForGet
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public string Created { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
    }
}
