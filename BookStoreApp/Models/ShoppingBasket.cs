namespace BookStoreApp.Models
{
    public class ShoppingBasket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ShoppingBasketBook> ShoppingBasketBooks { get; set; }
    }
}
