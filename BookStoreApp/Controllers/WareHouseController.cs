namespace BookStoreApp.Controllers
{
    using AutoMapper;
    using BookStoreApp.Dto;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly IWareHouseRepository _wareHowseRepository;
        private readonly IMapper _mapper;

        public WareHouseController(IWareHouseRepository wareHouseRepository, IMapper mapper)
        {
            _wareHowseRepository = wareHouseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WareHouse>))]
        public async Task<IActionResult> GetWareHouse()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var wareHouse = _mapper.Map<List<WareHouseDto>>(await _wareHowseRepository.GetWareHouses());
            return Ok(wareHouse);
        }

        [HttpGet("{wareHouseId}")]
        [ProducesResponseType(200, Type = typeof(WareHouse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWareHouse(int wareHouseId)
        {
            if (!await _wareHowseRepository.WareHouseExists(wareHouseId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var wareHouse = _mapper.Map<WareHouseDto>(await _wareHowseRepository.GetWareHouse(wareHouseId));
            return Ok(wareHouse);
        }

        [HttpGet("books/{wareHouseId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBooksByWareHouse(int wareHouseId)
        {
            var books = _mapper.Map<List<BookDto>>(
                await _wareHowseRepository.GetBooksByWareHouse(wareHouseId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(books);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateWareHouse([FromBody] WareHouseDto wareHouseCreate)
        {
            if (wareHouseCreate == null)
            {
                return BadRequest(ModelState);
            }
            var getWareHouses = await _wareHowseRepository.GetWareHouses();
            var wareHouse = getWareHouses
                .Where(wh => wh.Address.Trim()
                .ToUpper() == wareHouseCreate.Address.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (wareHouse != null)
            {
                ModelState.AddModelError("", "Ware Howse already exist!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var wareHouseMap = _mapper.Map<WareHouse>(wareHouseCreate);
            if (!await _wareHowseRepository.CreateWareHouse(wareHouseMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{wareHouseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateWareHouse(int wareHouseId, [FromBody] WareHouseDto updateWareHouse)
        {
            if (updateWareHouse == null)
            {
                return BadRequest(ModelState);
            }
            if (wareHouseId != updateWareHouse.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _wareHowseRepository.WareHouseExists(wareHouseId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var wareHouseMap = _mapper.Map<WareHouse>(updateWareHouse);
            if (!await _wareHowseRepository.UpdateWareHouse(wareHouseMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{wareHouseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteWareHouse(int wareHouseId)
        {
            var wareHouseToDelete = await _wareHowseRepository.GetWareHouse(wareHouseId);
            if (!await _wareHowseRepository.WareHouseExists(wareHouseId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _wareHowseRepository.DeleteWareHouse(wareHouseToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}