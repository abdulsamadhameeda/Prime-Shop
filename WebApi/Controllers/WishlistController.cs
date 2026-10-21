using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Wishlist.Dtos;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {

        private readonly IMediator _mediator;


        public WishlistController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("GetProductIdsByUserId")]
        public async Task<IActionResult> GetProductIdsByUserId([FromQuery] string userId)
        {
            var result = await _mediator.Send(
                new GetWishlistProductIdsByUserIdQueries(userId));

            return Ok(result);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddToWishlistCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteWishlistItem([FromQuery] DeleteWishlistCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
