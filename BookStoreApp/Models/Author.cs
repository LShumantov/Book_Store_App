namespace BookStoreApp.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }     
        public virtual ICollection<Book> Books { get; set; }
    }
}
