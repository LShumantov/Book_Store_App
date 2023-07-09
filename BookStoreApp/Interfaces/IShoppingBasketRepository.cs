namespace BookStoreApp.Interfaces
{
    using BookStoreApp.Models;
    public interface IShoppingBasketRepository
    {
        Task<ICollection<ShoppingBasket>> GetShoppingBaskets();
        Task<ShoppingBasket> GetShoppingBasket(int id);
        Task<ICollection<Book>> GetBooksByShoppingBasket(int shoppingBasketId);
        Task<bool> ShoppingBasketExists(int id);
        Task<bool> CreateShoppingBasket(ShoppingBasket shoppingBasket);
        Task<bool> UpdateShoppingBasket(ShoppingBasket shoppingBasket);
        Task<bool> DeleteeShoppingBasket(ShoppingBasket shoppingBasket);
        Task<bool> Save();
    }
}
