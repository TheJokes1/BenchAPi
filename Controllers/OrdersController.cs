using MediatR;
using Microsoft.AspNetCore.Mvc;
using Business.Orders.Queries;
using Business.Orders.Commands;
using Entities.Models;
using CommonDTO.DTO;
using Entities.MainEntities;
using System.ComponentModel.DataAnnotations;

namespace BenchAPI.Controllers
{
	//[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : Controller
	{
		private readonly IMediator _mediator;
		public OrdersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/customers/5/orders
		[HttpGet("api/customers/{customerId}/orders")]
		public async Task<IActionResult> GetAllOrdersForCustomer(int customerId)
		{
			try
			{
				var query = new GetOrdersForCustomerQuery(customerId);
				var orders = await _mediator.Send(query);
				return Ok(orders);
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
			
		}

		// GET: api/Order/1-2-2024, 5-2-2024
		[HttpGet("api/orders")]
		public async Task<IActionResult> GetOrdersInTimeFrame([FromQuery] ResourceParametersOrder resourceParameters)
		{
			try
			{
				var query = new GetOrdersInTimeframeQuery(resourceParameters.DateFrom, resourceParameters.DateUntil);
				var orders = await _mediator.Send(query);
				return Ok(orders);
			}
			catch (Exception ex)
			{
				return BadRequest($"{ex.Message}");
			}
		}

		// PUT: api/Order/5
		[HttpPut("api/orders/{orderId}/state")]
		public async Task<IActionResult> UpdateOrder([FromQuery] OrderState orderState, int orderId)
		{
			if (orderId <= 0) return BadRequest("Id must be greater than 0");
			try
			{
				await _mediator.Send(new UpdateOrderStateCommand(orderState, orderId));
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// POST: api/Order
		[HttpPost("api/orders")]
		public async Task<IActionResult> AddOrder([FromBody] OrderDTO orderDTO)
		{
			try
			{
				var newOrderId = await _mediator.Send(new CreateOrderCommand(orderDTO));
				return Ok(newOrderId);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// GET: api/order/{orderId}/orderlines
		[HttpGet("api/orders/{orderId}/orderlines")]
		public async Task<IActionResult> GetOrderlinesForOrder(int orderId)
		{
			try
			{
				var query = new GetOrderlinesByOrderIdQuery(orderId);
				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
