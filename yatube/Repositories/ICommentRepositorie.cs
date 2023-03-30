using yatube.Models;

namespace yatube.Repositories
{
    public interface ICommentRepositorie
    {
        public List<CommentForGet> GetComments(int postId);
        public void AddComment(CommentForCreate comment, int postId);
        public void ReplaceComment(CommentForReplace comment, int postId);
        public void RemoveComment(int commentId, int postId);

    }
}
