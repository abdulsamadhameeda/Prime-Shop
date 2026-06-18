using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Basket.Dtos;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ISender _mediator;


        public BasketController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBaskets()
        {
            var result = await _mediator.Send(new GetAllBasketQueries());

            return Ok(result);
        }


        [HttpGet("GetByIdBasket")]
        public async Task<IActionResult> GetByIdBasket([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetByIdBasketQueries(id));

            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateBasket([FromBody] CreateBasketCommand command)
        {
            var Basket = await _mediator.Send(command);
            return Ok(Basket);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketCommand command)
        {
            var Basket = await _mediator.Send(command);
            return Ok(Basket);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteBasket([FromQuery] DeleteBasketCommand command)
        {
            var Basket = await _mediator.Send(command);
            return Ok(Basket);
        }
    }
}
