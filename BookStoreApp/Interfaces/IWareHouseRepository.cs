namespace BookStoreApp.Interfaces
{
    using BookStoreApp.Models;
    public interface IWareHouseRepository
    {
        Task<ICollection<WareHouse>> GetWareHouses();
        Task<WareHouse> GetWareHouse(int id);
        Task<ICollection<Book>> GetBooksByWareHouse(int wareHouseId);             
        Task<bool> WareHouseExists(int id);
        Task<bool> CreateWareHouse(WareHouse wareHouse);
        Task<bool> UpdateWareHouse(WareHouse wareHouse);
        Task<bool> DeleteWareHouse(WareHouse wareHouse);
        Task<bool> Save();
    }
}
