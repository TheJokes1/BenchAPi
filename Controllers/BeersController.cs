using Business.Cafes.Queries;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommonDTO.DTO;
using Business.Cafes.Commands;
using FluentValidation;
using Business.Beers.Queries;

namespace BenchAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BeersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public BeersController(IMediator mediator)
		{
			_mediator = mediator;
		}
		// GET : api/Cafe
		[HttpGet]
		public async Task<IActionResult> GetBeersByEmail([FromQuery] ResourceParametersBeer resourceParameters)
		{
			var query = new GetBeersQuery(resourceParameters.Name);
			var response = await _mediator.Send(query);
			return Ok(response);
		}
	}
}
