namespace BookStoreApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<ShoppingBasket> ShoppingBaskets { get; set; }
    }
}
