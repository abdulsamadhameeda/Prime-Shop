using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Category.Dtos;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IMediator  _mediator;


        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _mediator.Send(new GetAllCategoryQueries());

            return Ok(result);
        }


        [HttpGet("GetByIdCategories")]
        public async Task<IActionResult> GetByIdCategories([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetByIdCategoryQueries(id));

            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory([FromQuery] DeleteCategoryCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }

    }
}
