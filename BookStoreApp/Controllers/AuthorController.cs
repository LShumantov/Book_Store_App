namespace BookStoreApp.Controllers
{
    using AutoMapper;
    using BookStoreApp.Dto;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using BookStoreApp.Repository;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        public async Task<IActionResult> GetAuthors()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authors = _mapper.Map<List<AuthorDto>>(await _authorRepository.GetAuthors());
            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        [ProducesResponseType(200, Type = typeof(Author))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAuthor(int authorId)
        {
            if (!await _authorRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var author = _mapper.Map<AuthorDto>(await _authorRepository.GetAuthor(authorId));
            return Ok(author);
        }

        [HttpGet("/author/{bookId}")]
        [ProducesResponseType(200, Type = typeof(Author))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAuthorByBook(int bookId)
        {
            var author = _mapper.Map<AuthorDto>(await _authorRepository.GetAuthorByBook(bookId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(author);
        }

        [HttpGet("/books/{authorId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            var books = _mapper.Map<List<BookDto>>(await _authorRepository.GetBooksByAuthor(authorId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(books);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateAuthor([FromBody] AuthorDto authorCreate)
        {
            if (authorCreate == null)
            {
                return BadRequest(ModelState);
            }
            var getAuthors = await _authorRepository.GetAuthors();
            var author = getAuthors
                .Where(a => a.Name.Trim().ToUpper() == authorCreate.Name
                .TrimEnd().ToUpper())
                .FirstOrDefault();
            if (author != null)
            {
                ModelState.AddModelError("", "Author already exist!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorMap = _mapper.Map<Author>(authorCreate);
            if (!await _authorRepository.CreateAuthor(authorMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateAuthor( int authorId, [FromBody] AuthorDto updateAutor)
        {
            if (UpdateAuthor == null )
            {
                return BadRequest(ModelState);
            }
            if (authorId != updateAutor.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _authorRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var authorMap = _mapper.Map<Author>(updateAutor);

            if (!await _authorRepository.UpdateAuthor(authorMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent(); 
        }

        [HttpDelete("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteAuthor(int authorId)
        {         
            if (!await _authorRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var autorToDelete = await _authorRepository.GetAuthor(authorId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _authorRepository.DeleteAuthor(autorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }       
    }
}
