namespace BookStoreApp.XUnitTest.Repository
{
    using BookStoreApp.Models;
    using BookStoreApp.Repository;
    using BookStoreApp.XUnitTest.MemoryData;
    using FluentAssertions;
    public class AuthorIRepositoryTest
    {
        private readonly MemoryDataContex _contex;
        public AuthorIRepositoryTest()
        {
            _contex = new MemoryDataContex();
        }

        [Fact]
        public async void AuthorIRepository_GetAuthors_ReturnsAuthos()
        {
            //Arrange           
            var dbContext = await _contex.GetDatabaseContext();
            var authorRepository = new AuthorRepository(dbContext);
            //Act
            var result = await authorRepository.GetAuthors();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Author>>();           
        }

        [Fact]
        public async void AuthorIRepository_GetAuthorId_ReturnsAuthor()
        {
            //Arrange
            var id = 1;
            var dbContext = await _contex.GetDatabaseContext();
            var authorRepository = new AuthorRepository(dbContext);
            //Act
            var result = await authorRepository.GetAuthor(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Author>();
        }

        [Fact]
        public async void AuthorIRepository_CreateAuthor_ReturnsBool()
        {
            //Arrange
            var author = new Author()
            {
               Id = 100,
               Name = "Test",
               Address = "Доиран 20",
            };
            var dbContext = await _contex.GetDatabaseContext();
            var authorRepository = new AuthorRepository(dbContext);
            //Act
            var result = await authorRepository.CreateAuthor(author);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void AuthorIRepository_AuthorExisting_ReturnsBool()
        {
            //Arrange
            var authorIdShouldBeTrue = 9;
            var authorIdShouldBeFalse = 111;
            var dbContext = await _contex.GetDatabaseContext();
            var authorRepository = new AuthorRepository(dbContext);
            //Act
            var resultShouldBeTrue = await authorRepository.AuthorExists(authorIdShouldBeTrue);
            var resultShouldBeFalse = await authorRepository.AuthorExists(authorIdShouldBeFalse);
            //Assert
            resultShouldBeTrue.Should().BeTrue();
            resultShouldBeFalse.Should().BeFalse();
        }
      
        [Fact]
        public async void AuthorIRepository_UpdateAuthor_ReturnsBool()
        {
            //Arrange
            var author = new Author()
            {
                Id = 1,
                Name = "Test",
                Address = "Доиран 20",
            };
            var dbContext = await _contex.GetDatabaseContext();
            var authorRepository = new AuthorRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            dbContext.Authors.Update(author);           
            var result = await authorRepository.UpdateAuthor(author);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void AuthorIRepository_DeleteAuthor_ReturnsBool()
        {
            //Arrange
            var author = new Author()
            {
                Id = 1,
                Name = "Lyubomir Shumantov",
                Address = "Doiran 18",
            };
            var dbContext = await _contex.GetDatabaseContext();
            var authorRepository = new AuthorRepository(dbContext);
            //Act
            dbContext.ChangeTracker.Clear();
            dbContext.Authors.Remove(author);
            var result = await authorRepository.DeleteAuthor(author);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void AuthorIRepository_GetBooksByAuthorId_ReturnsBook()
        {
            //Arrange
            var id = 1;
            var dbContext = await _contex.GetDatabaseContext();
            var authorRepository = new AuthorRepository(dbContext);
            //Act
            var result = await authorRepository.GetBooksByAuthor(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Book>>();
        }

        [Fact]
        public async void AuthorIRepository_GetAuthorByBook_ReturnsAuthor()
        {
            //Arrange
            var id = 1;
            var dbContext = await _contex.GetDatabaseContext();
            var authorRepository = new AuthorRepository(dbContext);
            //Act
            var result = await authorRepository.GetAuthorByBook(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Author>();
        }
    }
}
