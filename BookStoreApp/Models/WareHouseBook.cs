namespace BookStoreApp.Models
{
    public class WareHouseBook
    {
        public int WareHouseId { get; set; }
        public WareHouse WareHouse { get; set; }
        public int BookId { get; set; }      
        public Book Book { get; set; }   
    }
}
