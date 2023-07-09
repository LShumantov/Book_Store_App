namespace BookStoreApp.Models
{
    public class ShoppingBasketBook
    {
        public int ShopingBasketId { get; set; }
        public int BookId { get; set; }
        public virtual ShoppingBasket ShoppingBasket { get; set; }
        public virtual Book Book { get; set; }
    }
}
