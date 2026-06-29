using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Category.Dtos;
using PrimeShop.Application.features.Product.Dtos;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;


        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProduct([FromQuery] Guid? categoryId,  decimal? minPrice, decimal? maxPrice)
        {
            var result = await _mediator.Send(new GetAllProductQueries(categoryId, minPrice, maxPrice));

            return Ok(result);
        }

            
        [HttpGet("GetByIdProduct")]
        public async Task<IActionResult> GetByIdProduct([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetByIdProductQueries(id));

            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return Ok(product);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductCommand command)
        {
            var product = await _mediator.Send(command);
            return Ok(product);
        }

    }
}
