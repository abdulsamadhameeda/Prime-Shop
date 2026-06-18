using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Domain.Entities;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISender _mediator;


        public OrderController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrderQueries());

            return Ok(result);
        }


        [HttpGet("GetByIdOrder")]
        public async Task<IActionResult> GetByIdOrder([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetByIdOrderQueries(id));

            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteOrder([FromQuery] DeleteOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }
    }
}
