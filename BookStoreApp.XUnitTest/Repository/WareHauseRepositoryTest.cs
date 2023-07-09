namespace BookStoreApp.XUnitTest.Repository
{
    using BookStoreApp.Models;
    using BookStoreApp.Repository;
    using BookStoreApp.XUnitTest.MemoryData;
    using FluentAssertions;
    public class WareHauseRepositoryTest
    {
        private readonly MemoryDataContex _contex;
        public WareHauseRepositoryTest()
        {
            _contex = new MemoryDataContex();
        }
      
        [Fact]
        public async void WareHousesRepository_GetWareHouses_ReturnWareHouses()
        {
            //Arrange           
            var dbContext = await _contex.GetDatabaseContext();
            var wareHousesRepository = new WareHouseRepository(dbContext);
            //Act
            var result = await wareHousesRepository.GetWareHouses();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<WareHouse>>();
        }

        [Fact]
        public async void WareHouseRepository_GetWareHouseId_ReturnsWareHouse()
        {
            //Arrange
            var id = 2;
            var dbContext = await _contex.GetDatabaseContext();
            var wareHouseRepository = new WareHouseRepository(dbContext);
            //Act
            var result = await wareHouseRepository.GetWareHouse(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<WareHouse>();
        }

        [Fact]
        public async void WareHouseIRepository_CreateWareHouse_ReturnsBool()
        {
            //Arrange
            var wareHouse = new WareHouse()
            {
                Id = 441,
                Address = "Даме Груев 8",
                Phone = "0886 665666"               
            };
            var dbContext = await _contex.GetDatabaseContext();
            var wareHouseRepository = new WareHouseRepository(dbContext);
            //Act
            var result = await wareHouseRepository.CreateWareHouse(wareHouse);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void WareHouseIRepository_WareHouseExists_ReturnsBool()
        {
            //Arrange
            var wareHouseExistsIdShouldBeTrue = 2;
            var wareHouseExistsIdShouldBeNotExist = 111;          
            var dbContext = await _contex.GetDatabaseContext();
            var wareHouseRepository = new WareHouseRepository(dbContext);
            //Act
            var resultShouldBeTrue = await wareHouseRepository.WareHouseExists(wareHouseExistsIdShouldBeTrue);
            var resultwareHouseExistsIdShouldBeNotExist = await wareHouseRepository.WareHouseExists(wareHouseExistsIdShouldBeNotExist);
          //Assert
            resultShouldBeTrue.Should().BeTrue();
            resultwareHouseExistsIdShouldBeNotExist.Should().BeFalse();
        }

        [Fact]
        public async void WareHouseIRepository_UpdateWareHouse_ReturnsBool()
        {
            //Arrange
            var wareHouse = new WareHouse()
            {
                Id = 2,
                Address = "Даме Груев 8",
                Phone = "0886 665666"
            };
            var dbContext = await _contex.GetDatabaseContext();
            var wareHouseRepository = new WareHouseRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            dbContext.WareHouses.Update(wareHouse);
            var result = await wareHouseRepository.UpdateWareHouse(wareHouse);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void DeleteWareHouseIRepository_DeleteWareHouse_ReturnsBool()
        {
            //Arrange
            var wareHouse = new WareHouse()
            {
                Id = 2,              
            };
            var dbContext = await _contex.GetDatabaseContext();
            var wareHouseRepository = new WareHouseRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            dbContext.WareHouses.Remove(wareHouse);
            var result = await wareHouseRepository.DeleteWareHouse(wareHouse);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void WareHouseIRepository_GetBooksByWareHouse_ReturnsListOfBook()
        {
            //Arrange
            var wareHouseId = 2;
            var dbContext = await _contex.GetDatabaseContext();
            var wareHouseRepository = new WareHouseRepository(dbContext);
            //Act
            var result = await wareHouseRepository.GetBooksByWareHouse(wareHouseId);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Book>>();
        }
    }
}
