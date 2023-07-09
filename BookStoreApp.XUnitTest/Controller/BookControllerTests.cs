namespace BookStoreApp.XUnitTest.Controller
{
    using AutoMapper;
    using BookStoreApp.Controllers;
    using BookStoreApp.Dto;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using FakeItEasy;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;

    public class BookControllerTests
    {       
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookControllerTests() 
        {
            _bookRepository = A.Fake<IBookRepository>();
            _authorRepository = A.Fake<IAuthorRepository>();           
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async void BookController_GetBook_ReturnOk()
        {
            //Arrange
            var books = A.Fake<ICollection<BookDto>>();
            var bookList = A.Fake<List<BookDto>>();
            A.CallTo(() => _mapper.Map<List<BookDto>>(books)).Returns(bookList);
            var controller = new BookController(_bookRepository, _authorRepository, _mapper);
            //Act
            var result = await controller.GetBooks();
            var okResult =  result as OkObjectResult;
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
       
        [Fact]
        public async void BookController_CreateBook_ReturnOk()
        {
            //Arrange 
            var shoppingBasketId = 1;
            var wareHouseId = 2;
            var authorId = 3;
            var author = A.Fake<Author>();
            var bookMap = A.Fake<Book>();
            var book = A.Fake<Book>();    
            var bookCreate = A.Fake<BookDto>();
            var bookList = A.Fake<IList<BookDto>>();
            var books = A.Fake<ICollection<BookDto>>();
            A.CallTo(() => _bookRepository.GetBookTrimToUpper(bookCreate))
              .Returns(book);
            A.CallTo(() => _mapper.Map<Book>(bookCreate)).Returns(book);          
            A.CallTo(() => _bookRepository.CreateBook(shoppingBasketId, wareHouseId, bookMap)).Returns(true);
            A.CallTo(() => _authorRepository.GetAuthor(authorId)).Returns(author);
            var controller = new BookController(_bookRepository, _authorRepository, _mapper);
            //Act
            var result = controller.CreateBook(shoppingBasketId,wareHouseId, authorId, bookCreate);
            //Assert
            result.Should().NotBeNull();
        }
    }
}
