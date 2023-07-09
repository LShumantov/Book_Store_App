namespace BookStoreApp.Controllers
{
    using AutoMapper;
    using BookStoreApp.Dto;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository,IAuthorRepository authorRepository, IMapper mapper) 
        { 
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;          
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public async Task<IActionResult> GetBooks()
        {          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var books = await _bookRepository.GetBooks();
            var booksDto =  _mapper.Map<List<BookDto>>(books);
            return new OkObjectResult(booksDto);
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBook(int bookId)
        {
            var bookExisting = await _bookRepository.BookExisting(bookId);
            if(!bookExisting)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var getBook = await _bookRepository.GetBook(bookId);
            var book = _mapper.Map<BookDto>(getBook);
            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateBook([FromQuery] int shoppingBasketId,
            [FromQuery] int wareHouseId, [FromQuery] int authorId , [FromBody] BookDto bookCreate)
        {
            if (bookCreate == null)
            {
                return BadRequest(ModelState);
            }
            var getBooks = await _bookRepository.GetBooks();
            var books= getBooks
                .Where(c => c.Title.Trim().ToUpper() == bookCreate.Title
                .TrimEnd().ToUpper())
                .FirstOrDefault();
            if (books != null)
            {
                ModelState.AddModelError("", "Book already exist!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookMap = _mapper.Map<Book>(bookCreate);
            bookMap.Autor = await _authorRepository.GetAuthor(authorId);
            var createBook = await _bookRepository.CreateBook(wareHouseId, shoppingBasketId, bookMap);
            if (!createBook)
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateBook(int bookId, [FromQuery] int shoppingBaskeId, 
            [FromQuery] int wareHouseId, [FromQuery] int authorId, [FromBody] BookDto updateBook)
        {
            if (UpdateBook == null)
            {
                return BadRequest(ModelState);
            }
            if (bookId != updateBook.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _bookRepository.BookExisting(bookId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var bookMap = _mapper.Map<Book>(updateBook);
            bookMap.Autor =await _authorRepository.GetAuthor(authorId);

            if (!await _authorRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            if (!await _bookRepository.UpdateBook(shoppingBaskeId, wareHouseId, bookMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            if (!await _bookRepository.BookExisting(bookId))
            {
                return NotFound();
            }
            var authorToDelete = await _authorRepository.GetAuthorByBook(bookId);
            var bookToDelete = await _bookRepository.GetBook(bookId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _authorRepository.DeleteAuthor(authorToDelete)) 
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            if (!await _bookRepository.DdeleteBook(bookToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
