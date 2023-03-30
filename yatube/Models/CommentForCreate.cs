namespace yatube.Models
{
    public class CommentForCreate
    {
        public string Text { get; set; }
        public string Created { get; set; }
        public int AuthorId { get; set; }
    }
}
