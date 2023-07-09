namespace BookStoreApp.Interfaces
{
    using BookStoreApp.Dto;
    using BookStoreApp.Models;
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetBooks();
        Task<Book> GetBook(int id);
        Task<Book> GetBook(string id);
        Task<Book> GetBookTrimToUpper(BookDto bookCreate);
        Task<bool> BookExisting(int bookId);
        Task<bool> CreateBook(int wareHouseId, int shoppingBasketId, Book book);
        Task<bool> UpdateBook(int wareHouseId, int shoppingBasketId, Book book);
        Task<bool> DdeleteBook(Book book);
        Task<bool> Save();
    }
}
