namespace BookStoreApp.Models
{
    public class WareHouseBook
    {
        public int WareHouseId { get; set; }
        public virtual WareHouse WareHouse { get; set; }
        public int BookId { get; set; }      
        public virtual Book Book { get; set; }   
    }
}
