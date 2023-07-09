namespace BookStoreApp.XUnitTest.Controller
{
    using AutoMapper;
    using BookStoreApp.Interfaces;
    using Moq;
    public class AuthorControllerTest
    {       
        private readonly Mock<IAuthorRepository> _authorRepository;
        private readonly Mock<IMapper> _mapper;
        public AuthorControllerTest()
        {
            _mapper = new Mock<IMapper>();
            _authorRepository = new Mock<IAuthorRepository>();
        }       
    }
}
