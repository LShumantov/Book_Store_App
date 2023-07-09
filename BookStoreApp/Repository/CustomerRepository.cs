namespace BookStoreApp.Repository
{
    using BookStoreApp.Data;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using Microsoft.EntityFrameworkCore;
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContex _context;     
        
        public CustomerRepository(DataContex contex) 
        { 
            _context = contex;   
        }

        public async Task<bool> CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CustomerExists(int id)
        {
            return await _context.Customers.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> DeleteCustomer(Customer customer)
        {
          _context.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerByShoppingBasket(int shoppingBasketId)
        {
            return await _context.ShoppingBaskets.Where(sb => sb.Id == shoppingBasketId)
                   .Select(c => c.Customer).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<ICollection<ShoppingBasket>> GetShoppingBasketIdByCustomer(int customerId)
        {
            return await _context.ShoppingBaskets.Where(c => c.Id == customerId).ToListAsync();               
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
