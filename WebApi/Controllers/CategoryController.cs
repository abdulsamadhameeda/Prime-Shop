using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Category.Commands.CreateCategory;

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


        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            return Ok(categoryId);
        }
    }
}
