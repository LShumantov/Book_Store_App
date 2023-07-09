namespace BookStoreApp.Repository
{
    using BookStoreApp.Data;
    using BookStoreApp.Dto;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using Microsoft.EntityFrameworkCore;
    public class BookRepository : IBookRepository
    {
        private readonly DataContex _context;

        public BookRepository(DataContex contex) 
        { 
            _context = contex;
        }
        public async Task<bool> CreateBook(int wareHouseId, int shoppingBasketId, Book book)
        {
            var bookWareHouseEntity = await _context.WareHouses.Where(a => a.Id == wareHouseId).FirstOrDefaultAsync();
            var bookShoppingBasketEntity = await _context.ShoppingBaskets.Where(a => a.Id == shoppingBasketId).FirstOrDefaultAsync();          
            var bookWareHouse = new WareHouseBook()
            {
                WareHouse = bookWareHouseEntity,
                Book = book,
            };
            await _context.AddAsync(bookWareHouse);
            var bookShoppingBasket = new ShoppingBasketBook()
            {
                ShoppingBasket = bookShoppingBasketEntity,
                Book = book,
            };
            await _context.AddAsync(bookShoppingBasket);
            await _context.AddAsync(book);
            await Save();
            return true;
        }
        public async Task<ICollection<Book>> GetBooks()
        {
            return await _context.Books.OrderBy(b => b.Id).ToListAsync();
        }
        public async Task<Book> GetBook(int id)
        {
            return await _context.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Book> GetBook(string title)
        {
            return await _context.Books.Where(b => b.Title == title).FirstOrDefaultAsync();
        }
        public async Task<bool> BookExisting(int bookId)
        {
            return await _context.Books.AnyAsync(b => b.Id == bookId);
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public async Task<bool> UpdateBook(int wareHouseId, int shoppingBasketId, Book book)
        {
             _context.Update(book);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DdeleteBook(Book book)
        {          
            _context.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Book> GetBookTrimToUpper(BookDto bookCreate)
        {
            var getBooks = await GetBooks();
            return getBooks.Where(c => c.Title.Trim().ToUpper() == bookCreate.Title
                .TrimEnd().ToUpper())
                .FirstOrDefault();
        }
    }
}
