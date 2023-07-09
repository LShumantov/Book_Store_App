namespace BookStoreApp.Models
{
    public class WareHouse
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<WareHouseBook> WareHouseBooks { get; set; }
    }
}
