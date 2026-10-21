using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeShop.Application.features.Basket.Dtos;
using PrimeShop.Application.features.Order.Dtos;
using PrimeShop.Domain.Entities;

namespace PrimeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;


        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrders([FromQuery] int? status )
        {
            var result = await _mediator.Send(new GetAllOrderQueries(status ));

            return Ok(result);
        }


        [HttpGet("GetByIdOrder")]
        public async Task<IActionResult> GetByIdOrder([FromQuery] Guid orderId)
        {
            var result = await _mediator.Send(new GetByIdOrderQueries(orderId));

            return Ok(result);
        }
        [HttpGet("GetByIdAndUserId")]
        public async Task<IActionResult> GetByIdAndUserId([FromQuery] Guid? orderId, [FromQuery] string userId)
        {
            var result = await _mediator.Send(new GetOrderByIdAndCustomerIdQuery(orderId, userId));
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetTotalProfit")]
        public async Task<IActionResult> GetTotalProfit()
        {
            var result = await _mediator.Send(new TotalProfitQueries());
            return Ok(result);
        }
        [HttpGet("GetTotalOrders")]
        public async Task<IActionResult> GetTotalOrders()
        {
            var result = await _mediator.Send(new TotalNumsOfOrderQueries());
            return Ok(result);
        }

        [HttpGet("GetOrdersPerMonth")]
        public async Task<IActionResult> GetOrdersPerMonth([FromQuery] int year)
        {
            var result = await _mediator.Send(new GetOrdersPerMonthQuery(year));
            return Ok(result);
        }
        [HttpGet("GetOrderStatus")]
        public async Task<IActionResult> GetOrderStatus()
        {
            var result = await _mediator.Send(new GetOrderStatusQuery());
            return Ok(result);
        }

        [HttpGet("GetTodaysSales")]
        public async Task<IActionResult> GetTodaysSales()
        {
            var result = await _mediator.Send(new TodaysSalesQueries());
            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteOrder([FromQuery] DeleteOrderCommand command)
        {
            var order = await _mediator.Send(command);
            return Ok(order);
        }



        [HttpPost("datatable")]
        public async Task<IActionResult> GetOrdersData([FromBody] GetDataTableOrdersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
