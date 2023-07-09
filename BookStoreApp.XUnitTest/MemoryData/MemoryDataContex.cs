namespace BookStoreApp.XUnitTest.MemoryData
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BookStoreApp.Data;
    using BookStoreApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class MemoryDataContex : DbContext
    {
        public DataContex DataBaseContext { get; private set; }

        public async Task<DataContex> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContex>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dataBaseContext = new DataContex(options);
            dataBaseContext.Database.EnsureCreated();
            if (await dataBaseContext.Books.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    dataBaseContext.Authors.AddAsync(
                      new Author()
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
                                   Title = "Some Text..",
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
                                            Title = "Some..",
                                            Customer = new Customer()
                                            {
                                                Name ="Мирослав Шумантож",
                                                Phone = "0886 111 444",
                                                Address="Доиран 55"
                                            }
                                        }
                                   }
                              },
                            },
                        }
                      }); ;
                    await dataBaseContext.SaveChangesAsync();
                }
            }
            return dataBaseContext;
        }
    }
}
