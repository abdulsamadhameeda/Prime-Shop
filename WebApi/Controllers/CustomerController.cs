using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Customer.Dtos;
using PrimeShop.Application.features.Order.Dtos;


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
        public async Task<IActionResult> GetByIdCustomer([FromQuery] string id)
        {
            var result = await _mediator.Send(new GetByIdCustomerQueries(id));

            return Ok(result);
        }

        [HttpGet("GetTotalCustomers")]
        public async Task<IActionResult> GetTotalCustomers()
        {
            var result = await _mediator.Send(new TotalNumsOfCustomerQueries());
            return Ok(result);
        }



        [HttpGet("GetTop6NewCustomers")]
        public async Task<IActionResult> GetTop6NewCustomers()
        {
            var result = await _mediator.Send(new Top6NewCustomersQueries());
            return Ok(result);
        }


      
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] DeleteCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }




    }
}
