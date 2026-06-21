using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.OrderItem.Dtos;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {

        private readonly IMediator _mediator;


        public OrderItemController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var result = await _mediator.Send(new GetAllOrderItemQueries());

            return Ok(result);
        }


        [HttpGet("GetByIdOrderItem")]
        public async Task<IActionResult> GetByIdOrderItem([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetByIdOrderItemQueries(id));

            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemCommand command)
        {
            var orderItem = await _mediator.Send(command);
            return Ok(orderItem);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOrderItem([FromBody] UpdateOrderItemCommand command)
        {
            var orderItem = await _mediator.Send(command);
            return Ok(orderItem);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteOrderItem([FromQuery] DeleteOrderItemCommand command)
        {
            var orderItem = await _mediator.Send(command);
            return Ok(orderItem);
        }

    }
}
