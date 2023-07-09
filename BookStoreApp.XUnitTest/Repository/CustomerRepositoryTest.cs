namespace BookStoreApp.XUnitTest.Repository
{
    using BookStoreApp.Models;
    using BookStoreApp.Repository;
    using BookStoreApp.XUnitTest.MemoryData;
    using FluentAssertions;
    using System.Collections.Generic;
    public class CustomerRepositoryTest
    {
        private readonly MemoryDataContex _contex;
        public CustomerRepositoryTest()
        {
            _contex = new MemoryDataContex();
        }

        [Fact]
        public async void CustomerRepository_GetCustomers_ReturnsCustomers()
        {
            //Arrange           
            var dbContext = await _contex.GetDatabaseContext();
            var customerRepository = new CustomerRepository(dbContext);
            //Act
            var result = await customerRepository.GetCustomers();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Customer>>();
        }

        [Fact]
        public async void CustomerRepository_GetCustomerId_ReturnsCustomer()
        {
            //Arrange
            var id = 1;
            var dbContext = await _contex.GetDatabaseContext();
            var customerRepository = new CustomerRepository(dbContext);
            //Act
            var result = await customerRepository.GetCustomer(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Customer>();
        }

        [Fact]
        public async void CustomerIRepository_CreateCustomer_ReturnsBool()
        {
            //Arrange
            var customer = new Customer()
            {
                Id = 111,
                Name = "Test",
                Address = "Босилек 4",
                Phone = "0886 843333"
            };
            var dbContext = await _contex.GetDatabaseContext();
            var customerRepository = new CustomerRepository(dbContext);
            //Act
            var result = await customerRepository.CreateCustomer(customer);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void CustomerIRepository_CustomerExisting_ReturnsBool()
        {
            //Arrange
            var customerIdShouldBeTrue = 9;
            var customerIdShouldBeFalse = 111;
            var dbContext = await _contex.GetDatabaseContext();
            var customerRepository = new CustomerRepository(dbContext);
            //Act
            var resultShouldBeTrue = await customerRepository.CustomerExists(customerIdShouldBeTrue);
            var resultShouldBeFalse = await customerRepository.CustomerExists(customerIdShouldBeFalse);
            //Assert
            resultShouldBeTrue.Should().BeTrue();
            resultShouldBeFalse.Should().BeFalse();
        }

        [Fact]
        public async void CustomerIRepository_UpdateCustomer_ReturnsBool()
        {
            //Arrange
            var customer = new Customer()
            {
                Id = 1,
                Name = "Test",
                Address = "Босилек 4",
                Phone = "0886 843333"
            };
            var dbContext = await _contex.GetDatabaseContext();
            var customerRepository = new CustomerRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            dbContext.Customers.Update(customer);
            var result = await customerRepository.UpdateCustomer(customer);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void CustomerIRepository_DeleteCustomer_ReturnsBool()
        {
            //Arrange
            var customer = new Customer()
            {
                Id = 1,
                Name = "Gosho Trencov",
                Address = "Yrumov 5",
                Phone = "0886 845444"
            };
            var dbContext = await _contex.GetDatabaseContext();
            var customerRepository = new CustomerRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            dbContext.Customers.Remove(customer);
            var result = await customerRepository.DeleteCustomer(customer);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void CustomerIRepository_GetShoppingBasketIdByCustomer_ReturnsBook()
        {
            //Arrange
            var customerId = 2;
            var dbContext = await _contex.GetDatabaseContext();
            var customerRepository = new CustomerRepository(dbContext);
            //Act
            var result = await customerRepository.GetShoppingBasketIdByCustomer(customerId);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ShoppingBasket>>();
        }

        [Fact]
        public async void CustomerIRepository_GetCustomerByShoppingBasket_Customer()
        {
            //Arrange
            var shoppingBasketId = 2;
            var dbContext = await _contex.GetDatabaseContext();
            var customerRepository = new CustomerRepository(dbContext);
            //Act
            var result = await customerRepository.GetCustomerByShoppingBasket(shoppingBasketId);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Customer>();
        }
    }
}
