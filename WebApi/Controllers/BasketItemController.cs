using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.BasketItem.Dtos;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly IMediator _mediator;


        public BasketItemController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBasketItems()
        {
            var result = await _mediator.Send(new GetAllBasketItemQueries());

            return Ok(result);
        }


        [HttpGet("GetByBasketIdBasketItems")]
        public async Task<IActionResult> GetByBasketIdBasketItems([FromQuery] Guid BasketId)
        {
            var result = await _mediator.Send(new GetByIdBasketItemQueries(BasketId));

            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateBasketItem([FromBody] CreateBasketItemCommand command)
        {
            var Basket = await _mediator.Send(command);
            return Ok(Basket);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBasketItem([FromBody] UpdateBasketItemCommand command)
        {
            var Basket = await _mediator.Send(command);
            return Ok(Basket);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteBasketItem([FromQuery] DeleteBasketItemCommand command)
        {
            var Basket = await _mediator.Send(command);
            return Ok(Basket);
        }
    }
}
