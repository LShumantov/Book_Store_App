namespace BookStoreApp.XUnitTest.Repository
{
    using BookStoreApp.Models;
    using BookStoreApp.Repository;
    using BookStoreApp.XUnitTest.MemoryData;
    using FluentAssertions;
    public class BookRepositoryTest
    {
        private readonly MemoryDataContex _contex;
        public BookRepositoryTest()
        {
            _contex = new MemoryDataContex();
        }

        [Fact]
        public async void BookRepository_GetBook_ReturnsBook()
        {
            //Arrange
            var title = "Светът под микроскоп";
            var dbContext = await _contex.GetDatabaseContext();
            var bookRepository = new BookRepository(dbContext);
            //Act
            var result = await bookRepository.GetBook(title);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Book>();
        }

        [Fact]
        public async void BookRepository_CreateBook_ReturnsBool()
        {
            //Arrange
            var wareHouseId = 2;
            var shoppingBasketId = 2;
            var book = new Book()
            {
                AutorId = 33,
                Title = "Java advanced",
                Price = 33,
                Year = 1233              
            };           
            var dbContext = await _contex.GetDatabaseContext();
            var bookRepository = new BookRepository(dbContext);
            //Act
            var result = await bookRepository.CreateBook(wareHouseId, shoppingBasketId, book);
            //Assert
            result.Should().BeTrue();        
        }

        [Fact]
        public async void BookRepository_BookExisting_ReturnsBool()
        {
            //Arrange
            var bookIdShouldBeTrue = 1;
            var bookIdShouldBeFalse = 111;
            var dbContext = await _contex.GetDatabaseContext();
            var bookRepository = new BookRepository(dbContext);
            //Act
            var resultShouldBeTrue = await bookRepository.BookExisting(bookIdShouldBeTrue);
            var resultShouldBeFalse = await bookRepository.BookExisting(bookIdShouldBeFalse);
            //Assert
            resultShouldBeTrue.Should().BeTrue();
            resultShouldBeFalse.Should().BeFalse();
        }

        [Fact]
        public async void BookRepository_GetBooks_ReturnsListOfBooks()
        {
            //Arrange          
            var dbContext = await _contex.GetDatabaseContext();
            var bookRepository = new BookRepository(dbContext);
            //Act
            var result = await bookRepository.GetBooks();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Book>>();
        }

        [Fact]
        public async void BookRepository_GetBookId_ReturnsBool()
        {
            var bookIdShouldBeTrue = 1;
            var bookIdShouldBeFalse = 111;
            var dbContext = await _contex.GetDatabaseContext();
            var bookRepository = new BookRepository(dbContext);
            //Act
            var resultShouldBeTrue = await bookRepository.GetBook(bookIdShouldBeTrue);
            var resultShouldBeFalse = await bookRepository.GetBook(bookIdShouldBeFalse);
            //Assert
            resultShouldBeTrue.Should().NotBeNull();       
            resultShouldBeFalse.Should().BeNull();
            resultShouldBeTrue.Should().BeOfType<Book>();
        }

        [Fact]
        public async void BookRepository_UpdateBook_ReturnsBool()
        {
            //Arrange
            var wareHouseId = 12;
            var shoppingBasketId = 12;
            var book = new Book()
            {                
                AutorId = 3,
                Id = 6,
                Title = "С",
                Year = 1858885,
                Price = 1588885.70
            };
            var dbContext = await _contex.GetDatabaseContext();          
            var bookRepository = new BookRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            dbContext.Books.Update(book);
            //dbContext.SaveChanges();
            var result = await bookRepository.UpdateBook(wareHouseId, shoppingBasketId, book);
            //Assert
            result.Should().BeTrue();           
        }

        [Fact]
        public async void BookRepository_DeleteBook_ReturnsBool()
        {
            //Arrange
            var book = new Book()
            {
                AutorId = 1,
                Id = 1,
                Title = "Светът под микроскоп",
                Year = 1855,
                Price = 155.7
            };
            var dbContext = await _contex.GetDatabaseContext();
            var bookRepository = new BookRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            var result = await bookRepository.DdeleteBook(book);
            //Assert
            result.Should().BeTrue();
        }
    }
}

