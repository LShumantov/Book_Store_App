namespace BookStoreApp.Models
{
    public class ShoppingBasketBook
    {
        public int ShopingBasketId { get; set; }
        public int BookId { get; set; }
        public ShoppingBasket ShoppingBasket { get; set; }
        public Book Book { get; set; }
    }
}
