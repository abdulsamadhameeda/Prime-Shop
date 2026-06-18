using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.BasketItem.Dtos;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly ISender _mediator;


        public BasketItemController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBasketItems()
        {
            var result = await _mediator.Send(new GetAllBasketItemQueries());

            return Ok(result);
        }


        [HttpGet("GetByIdBasketItems")]
        public async Task<IActionResult> GetByIdBasketItem([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetByIdBasketItemQueries(id));

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
