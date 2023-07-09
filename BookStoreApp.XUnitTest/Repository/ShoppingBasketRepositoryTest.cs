namespace BookStoreApp.XUnitTest.Repository
{
    using BookStoreApp.Models;
    using BookStoreApp.Repository;
    using BookStoreApp.XUnitTest.MemoryData;
    using FluentAssertions;
    public class ShoppingBasketRepositoryTest
    {
        private readonly MemoryDataContex _contex;
        public ShoppingBasketRepositoryTest()
        {
            _contex = new MemoryDataContex();
        }

        [Fact]
        public async void ShoppingBasketsRepository_GetShoppingBaskets_ReturnsShoppingBaskets()
        {
            //Arrange           
            var dbContext = await _contex.GetDatabaseContext();
            var shoppingBasketRepository = new ShoppingBasketRepository(dbContext);
            //Act
            var result = await shoppingBasketRepository.GetShoppingBaskets();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ShoppingBasket>>();
        }

        [Fact]
        public async void ShoppingBasketRepository_GetShoppingBasketId_ReturnsCustomer()
        {
            //Arrange
            var id = 2;
            var dbContext = await _contex.GetDatabaseContext();
            var shoppingBasketRepository = new ShoppingBasketRepository(dbContext);
            //Act
            var result = await shoppingBasketRepository.GetShoppingBasket(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ShoppingBasket>();
        }

        [Fact]
        public async void ShoppingBasketIRepository_CreateShoppingBasket_ReturnsBool()
        {
            //Arrange
            var shoppingBasket = new ShoppingBasket()
            {
               Id = 111,
               Title = "Test",  
            };
            var dbContext = await _contex.GetDatabaseContext();
            var shoppingBasketRepository = new ShoppingBasketRepository(dbContext);
            //Act
            var result = await shoppingBasketRepository.CreateShoppingBasket(shoppingBasket);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void ShoppingBasketIRepository_ShoppingBasketExists_ReturnsBool()
        {
            //Arrange
            var shoppingBasketIdShouldBeTrue = 2;
            var shoppingBasketIdShouldBeFalse = 111;
            var dbContext = await _contex.GetDatabaseContext();
            var shoppingBasketRepository = new ShoppingBasketRepository(dbContext);
            //Act
            var resultShouldBeTrue = await shoppingBasketRepository.ShoppingBasketExists(shoppingBasketIdShouldBeTrue);
            var resultShouldBeFalse = await shoppingBasketRepository.ShoppingBasketExists(shoppingBasketIdShouldBeFalse);
            //Assert
            resultShouldBeTrue.Should().BeTrue();
            resultShouldBeFalse.Should().BeFalse();
        }

        [Fact]
        public async void ShoppingBasketIRepository_UpdateShoppingBasket_ReturnsBool()
        {
            //Arrange
            var shoppingBasket = new ShoppingBasket()
            {
                Id = 2,
                Title = "Test",
            };
            var dbContext = await _contex.GetDatabaseContext();
            var shoppingBasketRepository = new ShoppingBasketRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            dbContext.ShoppingBaskets.Update(shoppingBasket);
            var result = await shoppingBasketRepository.UpdateShoppingBasket(shoppingBasket);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void ShoppingBasketIRepository_DeleteeShoppingBasket_ReturnsBool()
        {
            //Arrange
            var shoppingBasket = new ShoppingBasket()
            {
                Id = 2,
                Title = "Some Text..",
            };
            var dbContext = await _contex.GetDatabaseContext();
            var shoppingBasketRepository = new ShoppingBasketRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            dbContext.ShoppingBaskets.Remove(shoppingBasket);
            var result = await shoppingBasketRepository.DeleteeShoppingBasket(shoppingBasket);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void ShoppingBasketIRepository_GetBooksByShoppingBasket_ReturnsBook()
        {
            //Arrange
            var shoppingBasketId = 2;
            var dbContext = await _contex.GetDatabaseContext();
            var shoppingBasketRepository = new ShoppingBasketRepository(dbContext);
            //Act
            var result = await shoppingBasketRepository.GetBooksByShoppingBasket(shoppingBasketId);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Book>>();
        }
    }
}
