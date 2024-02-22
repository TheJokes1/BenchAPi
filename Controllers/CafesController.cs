using Business.Cafes.Queries;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommonDTO.DTO;
using Business.Cafes.Commands;
using FluentValidation;

namespace BenchAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CafesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CafesController(IMediator mediator)
		{
			_mediator = mediator;
		}


		// GET : api/Cafe
		[HttpGet]
		public async Task<IActionResult> GetCafesByEmail([FromQuery] ResourceParametersCafe resourceParameters)
		{
			var query = new GetCafesByEmailQuery(resourceParameters.SearchQueryEmail);
			var response = await _mediator.Send(query);
			return Ok(response);
		}


	   // POST: api/Cafe
	   [HttpPost]
		public async Task<ActionResult> Add([FromBody] CafeDTO cafeDTO)
		{
			try
			{
				var newCafeId = await _mediator.Send(new AddCafeCommand(cafeDTO));
				return Ok(newCafeId);
			}
			catch (ValidationException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(500, "An error occurred while processing the request.");
			}
		}

		// PUT: api/Cafe/5
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCafe(int id, [FromBody] CafeDTO cafeDTO)
		{
			if (id <= 0) return BadRequest("Id must be greater than 0");
			cafeDTO.Id = id;
			try
			{
				await _mediator.Send(new UpdateCafeCommand(cafeDTO));
				return Ok();
			}
			catch (ValidationException ex)
			{
				var errorMessages = ex.Errors.Select(error => error.ErrorMessage).ToList();
				return BadRequest(errorMessages); // Validation errors
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message); // Other errors
			}
		}







		//// GET: CafeController/Delete/5
		//public ActionResult Delete(int id)
		//{
		//	return null;
		//}



		//// POST: CafeController/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, IFormCollection collection)
		//{
		//	try
		//	{
		//		return RedirectToAction(nameof(Index));
		//	}
		//	catch
		//	{
		//		return null;
		//	}
		//}
	}
}
