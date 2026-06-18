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

        private readonly ISender _mediator;


        public CategoryController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand command)
        {
            var category = await _mediator.Send(command);
            return Ok(category);
        }

    }
}
