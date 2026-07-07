using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Lookup.Dtos;
using PrimeShop.Application.features.Order.Dtos;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {

        private readonly IMediator _mediator;


        public LookupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetSpecificType")]
        public async Task<IActionResult> GetSpecificType([FromQuery] string type)
        {
            var result = await _mediator.Send(new GetSpecificTypeQueries(type));

            return Ok(result);
        }


    }
}
