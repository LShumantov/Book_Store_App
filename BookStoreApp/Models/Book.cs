namespace BookStoreApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }     
        public int AutorId { get; set; }
        public Author Autor { get; set; }
        public ICollection<WareHouseBook> WareHouseBooks { get; set; }
        public ICollection<ShoppingBasketBook> ShoppingBasketBooks { get; set; }
    }
}
