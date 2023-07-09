namespace BookStoreApp.Controllers
{
    using AutoMapper;
    using BookStoreApp.Dto;
    using BookStoreApp.Interfaces;
    using BookStoreApp.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public async Task<IActionResult> GetCustomers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }             
            var customer = _mapper.Map<List<CustomerDto>>(await _customerRepository.GetCustomers());
            return Ok(customer);
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            var customer = _mapper.Map<CustomerDto>(await _customerRepository.GetCustomer(customerId));
            if (customer == null)
            {
                return NotFound();
            }              
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }          
            return Ok(customer);
        }

        [HttpGet("/customer/{shopinBasketId}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCustomerByShoppinBasket(int customerId)
        {
            var customer = _mapper.Map<CustomerDto>(await _customerRepository.GetCustomerByShoppingBasket(customerId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(customer);
        }

        [HttpGet("/shopinBasket/{customerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoppingBasket>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShoppingBasketByCustomer(int customerId)
        {
            var shoppingBasket = _mapper.Map<List<ShoppingBasketDto>>(await _customerRepository.GetShoppingBasketIdByCustomer(customerId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(shoppingBasket);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerDto customerCreate)
        {
            if (customerCreate == null)
            {
                return BadRequest(ModelState);
            }
            var getCustomer = await _customerRepository.GetCustomers();
            var customer = getCustomer
                .Where(a => a.Name.Trim().ToUpper() == customerCreate.Name
                .TrimEnd().ToUpper())
                .FirstOrDefault();
            if (customer != null)
            {
                ModelState.AddModelError("", "Customer already exist!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerMap = _mapper.Map<Customer>(customerCreate);
            if (!await _customerRepository.CreateCustomer(customerMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{customerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateCustomer(int customerId, [FromBody] CustomerDto updateCustomer)
        {
            if (updateCustomer == null)
            {
                return BadRequest(ModelState);
            }
            if (customerId != updateCustomer.Id)
            {
                return BadRequest(ModelState);
            }
            if (!await _customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customerMap = _mapper.Map<Customer>(updateCustomer);

            if (!await _customerRepository.UpdateCustomer(customerMap))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteCustomer(int customerId)
        {
            var customerToDelete = await _customerRepository.GetCustomer(customerId);
            if (customerToDelete == null)
            {
                return NotFound();
            }          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _customerRepository.DeleteCustomer(customerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
