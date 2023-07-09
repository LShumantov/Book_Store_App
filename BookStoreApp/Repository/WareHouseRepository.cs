namespace BookStoreApp.Repository
{
    using BookStoreApp.Data;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using Microsoft.EntityFrameworkCore;
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly DataContex _context;

        public WareHouseRepository(DataContex contex)
        {
            _context = contex;
        }

        public async Task<bool> WareHouseExists(int id)
        {
            return await _context.WareHouses.AnyAsync(e => e.Id == id);
        }

        public async Task<ICollection<WareHouse>> GetWareHouses()
        {
            return await _context.WareHouses.ToListAsync();
        }

        public async Task<WareHouse> GetWareHouse(int id)
        {
            return await _context.WareHouses.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Book>> GetBooksByWareHouse(int wareHouseId)
        {
            return await _context.WareHouseBooks
                .Where(e => e.WareHouseId == wareHouseId)
                .Select(b => b.Book).ToListAsync();
        }

        public async Task<bool> CreateWareHouse(WareHouse wareHouse)
        {
            _context.Add(wareHouse);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateWareHouse(WareHouse wareHouse)
        {
           _context.Update(wareHouse);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWareHouse(WareHouse wareHouse)
        {
            _context.Remove(wareHouse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
