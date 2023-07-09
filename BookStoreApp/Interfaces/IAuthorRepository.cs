namespace BookStoreApp.Interfaces
{
    using BookStoreApp.Models;
    public interface IAuthorRepository
    {
        Task<ICollection<Author>> GetAuthors();
        Task<Author> GetAuthor(int id);
        Task<Author> GetAuthorByBook(int bookId);
        Task<ICollection<Book>> GetBooksByAuthor(int authorId);
        Task<bool> AuthorExists(int id);
        Task<bool> CreateAuthor(Author author);
        Task<bool> UpdateAuthor(Author author);
        Task<bool> DeleteAuthor(Author author);
        Task<bool> Save();        
    }
}
