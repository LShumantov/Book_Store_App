namespace BookStoreApp.Repository
{
    using BookStoreApp.Data;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Data;
    public class ShoppingBasketRepository : IShoppingBasketRepository
    {
        private readonly DataContex _context;

        public ShoppingBasketRepository(DataContex contex)
        {
            _context = contex;
        }
        public async Task<ICollection<Book>> GetBooksByShoppingBasket(int shoppingBasketId)
        {
            return await _context.ShoppingBasketBooks
                .Where(e => e.ShopingBasketId == shoppingBasketId)
                .Select(b => b.Book).ToListAsync();
        }
        public async Task<ICollection<ShoppingBasket>> GetShoppingBaskets()
        {
            return await _context.ShoppingBaskets.ToListAsync();
        }
        public async Task<ShoppingBasket> GetShoppingBasket(int id)
        {
            return await _context.ShoppingBaskets.Where(e => e.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> ShoppingBasketExists(int id)
        {
            return await _context.ShoppingBaskets.AnyAsync(e => e.Id == id);
        }
        public async Task<bool> CreateShoppingBasket(ShoppingBasket shoppingBasket)
        {
            _context.AddAsync(shoppingBasket);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public async Task<bool> UpdateShoppingBasket(ShoppingBasket shoppingBasket)
        {
           _context.Update(shoppingBasket);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteeShoppingBasket(ShoppingBasket shoppingBasket)
        {
            _context.Remove(shoppingBasket);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
