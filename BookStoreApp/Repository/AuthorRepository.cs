namespace BookStoreApp.Repository
{
    using AutoMapper;
    using BookStoreApp.Data;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using Microsoft.EntityFrameworkCore;
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContex _context;         
        public AuthorRepository(DataContex contex) 
        { 
            _context = contex;          
        }
        public async Task<bool> AuthorExists(int id)
        {
           return await _context.Authors.AnyAsync(a => a.Id == id);
        }
        public async Task<bool> CreateAuthor(Author author)
        {
            _context.AddAsync(author);
            await _context.SaveChangesAsync();
            return true;         
        }      
        public async Task<bool> DeleteAuthor(Author author)
        {
             _context.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Author> GetAuthor(int id)
        {
          return await _context.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();    
        }
        public async Task<Author> GetAuthorByBook(int bookId)
        {
            return await _context.Books.Where(b => b.Id == bookId).Select(a => a.Autor).FirstOrDefaultAsync(); 
        }      
        public async Task<ICollection<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<ICollection<Book>> GetBooksByAuthor(int authorId)
        {
            return await _context.Books.Where(a => a.Autor.Id == authorId).ToListAsync();
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public async Task<bool> UpdateAuthor(Author author)
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
