namespace BookStoreApp
{
    using BookStoreApp.Data;
    using BookStoreApp.Models;
    public class Seed
    {
        private readonly DataContex dataContex;
        public Seed(DataContex contex)
        {
            this.dataContex = contex;
        }
        public void SeedDataContex()
        {       
            if (!dataContex.Authors.Any())
            {
                var author = new List<Author>
                {
                      new Author
                      {                       
                       Name = "Lyubomir Shumantov",
                       Address = "Doiran 18",
                       Books = new List<Book>()
                       {
                         new Book
                         {
                            Title = "Светът под микроскоп",Year = 1855,Price = 155.70,
                            WareHouseBooks = new List<WareHouseBook>()
                            {
                                    new WareHouseBook
                                    {
                                        WareHouse = new WareHouse
                                        {
                                            Phone = "0886 822 883",
                                            Address = "Bosilek 1"
                                        }
                                    }
                            },
                            ShoppingBasketBooks = new List<ShoppingBasketBook>()
                            {
                                   new ShoppingBasketBook
                                   {                                        
                                        ShoppingBasket = new ShoppingBasket()
                                        {
                                            Customer = new Customer()
                                            {
                                                Name ="Gosho Trencov",
                                                Phone = "0886 845 444",
                                                Address="Yrumov 5"
                                            }
                                        }
                                   }
                            },                            
                         },
                         new Book
                         {
                            Title = "Въведение в програмирането",Year = 2015,Price = 111.70,
                            WareHouseBooks = new List<WareHouseBook>()
                            {
                                    new WareHouseBook
                                    {
                                        WareHouse = new WareHouse
                                        {
                                            Phone = "0886 111 883",
                                            Address = "Марица 1"
                                        }
                                    }
                            },
                            ShoppingBasketBooks = new List<ShoppingBasketBook>()
                            {
                                   new ShoppingBasketBook
                                   {
                                        ShoppingBasket = new ShoppingBasket()
                                        {
                                            Customer = new Customer()
                                            {
                                                Name ="Мирослав Шумантож",
                                                Phone = "0886 111 444",
                                                Address="Доиран 55"
                                            }
                                        }
                                   }
                            },
                         }
                       }
                      }
                };
                dataContex.Authors.AddRange(author);
                dataContex.SaveChanges();
            }
        }               
    }          
}

