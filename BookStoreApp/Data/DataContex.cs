namespace BookStoreApp.Data
{
    using BookStoreApp.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Migrations;
    public class DataContex : DbContext
    {
        public DataContex(DbContextOptions<DataContex> options)
            : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }       
        public DbSet<ShoppingBasket> ShoppingBaskets { get; set; }
        public DbSet<ShoppingBasketBook> ShoppingBasketBooks { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<WareHouseBook> WareHouseBooks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingBasketBook>()
                .HasKey(sbb => new { sbb.ShopingBasketId, sbb.BookId });
            modelBuilder.Entity<ShoppingBasketBook>()
                .HasOne(sb => sb.ShoppingBasket)
                .WithMany(sbb => sbb.ShoppingBasketBooks)
                .HasForeignKey(sb => sb.ShopingBasketId);
            modelBuilder.Entity<ShoppingBasketBook>()
                .HasOne(b => b.Book)
                .WithMany(sbb => sbb.ShoppingBasketBooks)
                .HasForeignKey(b => b.BookId);
            modelBuilder.Entity<WareHouseBook>()
                .HasKey(whb => new { whb.WareHouseId, whb.BookId });
            modelBuilder.Entity<WareHouseBook>()
                .HasOne(wh => wh.WareHouse)
                .WithMany(whb => whb.WareHouseBooks)
                .HasForeignKey(wh => wh.WareHouseId);
            modelBuilder.Entity<WareHouseBook>()
                .HasOne(b => b.Book)
                .WithMany(whb => whb.WareHouseBooks)
                .HasForeignKey(b => b.BookId);          
        }
        public partial class AddAndRemoveColumn : Migration
        {           
        protected override void Up(MigrationBuilder migrationBuilder)
        {                
              migrationBuilder.DropColumn(
                  name: "Count",
                  table: "WareHouseBooks");

              migrationBuilder.DropColumn(
                 name: "Count",
                 table: "ShoppingBasketBooks");
        }
        }
        public partial class AddShoppingBasketsTitleColumn : Migration
        {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ShoppingBaskets",
                type: "nvarchar(max)",
                nullable: true);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "ShoppingBaskets");
        }
        }
    }
}
