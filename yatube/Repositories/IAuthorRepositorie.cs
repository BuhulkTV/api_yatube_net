using yatube.Models;

namespace yatube.Repositories
{
    public interface IAuthorRepositorie
    {
        public List<Author> GetAuthors();
        public void AddAuthor(AuthorForCreate author);
        public void RemoveAuthor(int authorId);
    }
}
