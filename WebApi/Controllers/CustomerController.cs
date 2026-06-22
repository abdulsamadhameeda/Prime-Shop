using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Customer.Dtos;


namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IMediator   _mediator;


        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] string? name)
        {
            var result = await _mediator.Send(new GetAllCustomerQueries(name));

            return Ok(result);
        }


        [HttpGet("GetByIdCustomer")]
        public async Task<IActionResult> GetByIdCustomer([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetByIdCustomerQueries(id));

            return Ok(result);
        }



        [HttpPost("Create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] DeleteCustomerCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }




    }
}
