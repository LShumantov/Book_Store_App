namespace BookStoreApp.Controllers
{
    using AutoMapper;
    using BookStoreApp.Dto;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingBasketController : ControllerBase
    {
        private readonly IShoppingBasketRepository _shoppingBasketRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ShoppingBasketController(IShoppingBasketRepository shoppingBasketRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _shoppingBasketRepository = shoppingBasketRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoppingBasket>))]
        public async Task<IActionResult> GetShopingBaskets()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }          
            var wareHouse = _mapper.Map<List<ShoppingBasketDto>>(await _shoppingBasketRepository.GetShoppingBaskets());
            return Ok(wareHouse);
        }

        [HttpGet("{shoppingBasketId}")]
        [ProducesResponseType(200, Type = typeof(ShoppingBasket))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShopingBasket(int shoppingBasketId)
        {
            if (!await _shoppingBasketRepository.ShoppingBasketExists(shoppingBasketId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var shoppingBasket = _mapper.Map<ShoppingBasketDto>(await _shoppingBasketRepository.GetShoppingBasket(shoppingBasketId));
            return Ok(shoppingBasket);
        }

        [HttpGet("books/{shoppingBasketId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBooksByShoppingBasket(int shoppingBasketId)
        {
            var books = _mapper.Map<List<BookDto>>(await _shoppingBasketRepository.GetBooksByShoppingBasket(shoppingBasketId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(books);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateShoppingBasket([FromQuery] int customerId, [FromBody] ShoppingBasketDto shoppingBasketCreate)
        {
            if (shoppingBasketCreate == null)
            {
                return BadRequest(ModelState);
            }
            var getShoppingBaskets = await _shoppingBasketRepository.GetShoppingBaskets();
            var shoppingBaskets = getShoppingBaskets
                .Where(sb => sb.Title.Trim()
                .ToUpper() == shoppingBasketCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();


            if (shoppingBaskets != null)
            {
                ModelState.AddModelError("", "Shopping Basket already exist!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var shoppingBasketMap = _mapper.Map<ShoppingBasket>(shoppingBasketCreate);
            shoppingBasketMap.Customer = await _customerRepository.GetCustomer(customerId);
            if (!await _shoppingBasketRepository.CreateShoppingBasket(shoppingBasketMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{shoppingBasketId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateShoppingBasket(int shoppingBasketId, [FromQuery] int customerId, [FromBody] ShoppingBasketDto updateShoppingBasket)
        {
            if (updateShoppingBasket == null)
            {
                return BadRequest(ModelState);
            }
            if (shoppingBasketId != updateShoppingBasket.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _shoppingBasketRepository.ShoppingBasketExists(shoppingBasketId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var shoppingBasketMap = _mapper.Map<ShoppingBasket>(updateShoppingBasket);
            shoppingBasketMap.Customer = await _customerRepository.GetCustomer(customerId);
            if (!await _customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }
            if (!await _shoppingBasketRepository.UpdateShoppingBasket(shoppingBasketMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{shoppingBasketId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteAuthor(int shoppingBasketId)
        {
            if (!await _shoppingBasketRepository.ShoppingBasketExists(shoppingBasketId))
            {
                return NotFound();
            }
            var shoppingBasketToDelete = await _shoppingBasketRepository.GetShoppingBasket(shoppingBasketId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _shoppingBasketRepository.DeleteeShoppingBasket(shoppingBasketToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

