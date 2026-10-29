using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Auth.Login.Dtos;
using PrimeShop.Application.features.Auth.Register.Dtos;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;


        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                var errors = new ValidationProblemDetails();
                errors.Errors.Add(nameof(command.Email), new[] { ex.Message });

                return BadRequest(errors);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _mediator.Send(command);

            if (token == null)
            {
                return BadRequest();
            }

            return Ok(token);
        }
    }
}
