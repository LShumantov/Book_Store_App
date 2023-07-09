namespace BookStoreApp.Interfaces
{
    using BookStoreApp.Models;

    public interface ICustomerRepository
    {
        Task<ICollection<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task<Customer> GetCustomerByShoppingBasket(int shoppingBasketId);
        Task<ICollection<ShoppingBasket>> GetShoppingBasketIdByCustomer(int customerId);
        Task<bool> CustomerExists(int id);
        Task<bool> CreateCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(Customer customer);
        Task<bool> Save();
    }
}
