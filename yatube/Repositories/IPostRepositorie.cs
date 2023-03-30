using yatube.Models;

namespace yatube.Repositories
{
    public interface IPostRepositorie
    {
        public List<PostForGet> GetPosts();
        public void AddPost(PostForCreate post);
        public void ReplacePost(Post post);
        public void RemovePost(int postId);
    }
}
