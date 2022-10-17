using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using API.Helpers;
using API.DTO;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
	[ApiController]
	[Produces("application/json")]
    public class ComingsController: Controller
    {
        private BL.Facade _facade;
		private ILogger _logger;

        public ComingsController(BL.Facade facade, ILogger<ComingsController> logger)
        {
            _facade = facade;
			_logger = logger;
        }

        [Authorize(Roles = "ADMIN, ANALYST")]
		[HttpGet]
		// public IActionResult GetComings([FromUri] string date, [FromUri] string dateFrom, [FromUri] string dateTo)
		public IActionResult GetComings([FromQuery] Dates dates)
		{
			_logger.LogInformation("GetComings method in ComingsController");

			List<API.Coming> comingsAPI = new List<API.Coming>();

			List<BL.Coming> comingsBL;

			string date = dates.Date;
			string dateFrom = dates.DateFrom;
			string dateTo = dates.DateTo;

			if (String.IsNullOrEmpty(date) && String.IsNullOrEmpty(dateFrom) && String.IsNullOrEmpty(dateTo))
			{
				comingsBL = _facade.GetComings();
			}
			else if (!String.IsNullOrEmpty(date) && String.IsNullOrEmpty(dateFrom) && String.IsNullOrEmpty(dateTo))
			{
				DateTime Date = DateTime.ParseExact(date, "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
				Date = DateTime.SpecifyKind(Date, DateTimeKind.Utc);

				comingsBL = _facade.GetComingsByDate(Date);
			}
			else if (String.IsNullOrEmpty(date) && !String.IsNullOrEmpty(dateFrom) && !String.IsNullOrEmpty(dateTo))
			{
				DateTime Date1 = DateTime.ParseExact(dateFrom, "yyyy-MM-dd",
			                                System.Globalization.CultureInfo.InvariantCulture);
				Date1 = DateTime.SpecifyKind(Date1, DateTimeKind.Utc);

			    DateTime Date2 = DateTime.ParseExact(dateTo, "yyyy-MM-dd",
			                                System.Globalization.CultureInfo.InvariantCulture);
				Date2 = DateTime.SpecifyKind(Date2, DateTimeKind.Utc);

				comingsBL = _facade.GetComingsBetweenDates(Date1, Date2);
			}
			else 
			{
				return BadRequest("Invalid data of query");
			}

			foreach (var coming in comingsBL)
			{
				comingsAPI.Add(ComingConverter.BLToAPI(coming));
			}
			
			string jsonString = JsonSerializer.Serialize(comingsAPI, Options.JsonOptions());
			return new ContentResult
			{
				Content = jsonString,
				ContentType = "application/json",
				StatusCode = 200
			};
		}

        // [Authorize(Roles = "ADMIN")]
		[Authorize]
		[HttpGet("{id}")]
		public IActionResult GetComingById([FromRoute] int id)
		{
			_logger.LogInformation("GetComingById method in ComingsController");

			try
			{
				var coming = ComingConverter.BLToAPI(_facade.GetComingById(id));
				string jsonString = JsonSerializer.Serialize(coming, Options.JsonOptions());

				return new ContentResult
				{
					Content = jsonString,
					ContentType = "application/json",
					StatusCode = 200
				};
			}
			catch (Exception)
			{
				return NotFound($"Coming with id = {id} not found");
			}
		}

        //addComing
        [Authorize(Roles = "EMPLOYEE")]
		[HttpPost]
		public async Task<IActionResult> AddComingAsync([FromBody] API.Car car)
		{
			_logger.LogInformation("AddComingAsync method in ComingsController");

			try
			{
				var userIdstring = this.HttpContext.User.Claims
            	.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

				int userId;
				if (!int.TryParse(userIdstring, out userId))
				{
					return Unauthorized("Login, please");
				}

				Console.WriteLine($"UserId = {userId}\n");

				// var coming = ComingConverter.APIToBL(newComingCar.Coming);
				var coming = new Coming{UserId = userId};
				// Console.WriteLine("Coming\n");
				var comingBL = ComingConverter.APIToBL(coming);
				// Console.WriteLine("ComingBL\n");
                var carBL = CarConverter.APIToBL(car);
				// Console.WriteLine("CarBL\n");
				await _facade.AddComingAsync(comingBL, carBL);
				Console.WriteLine("Add\n");

				return Ok("Successful add");
			}
			catch (Exception)
			{
				return BadRequest($"Such coming also exists");
			}
		}

        // //findByDate
        // [Authorize(Roles = "ADMIN, ANALYST")]
		// [HttpGet]
		// public IActionResult GetComingsByDate(string date)
		// {
        //     DateTime Date = DateTime.ParseExact(date, "yyyy-MM-dd",
        //                                 System.Globalization.CultureInfo.InvariantCulture);
		// 	Date = DateTime.SpecifyKind(Date, DateTimeKind.Utc);

		// 	List<API.Coming> comingsAPI = new List<API.Coming>();

		// 	var comingsBL = _facade.GetComingsByDate(Date);
        //     foreach (var coming in comingsBL)
        //     {
        //         comingsAPI.Add(ComingConverter.BLToAPI(coming));
        //     }
            
		// 	string jsonString = JsonSerializer.Serialize(comingsAPI, Options.JsonOptions());
		// 	return new ContentResult
		// 	{
		// 		Content = jsonString,
		// 		ContentType = "application/json",
		// 		StatusCode = 200
		// 	};
		// }

        // //findBetweenDates
        // [Authorize(Roles = "ADMIN, ANALYST")]
		// [HttpGet]
		// public IActionResult GetComingsBetweenDates(string dateFrom, string dateTo)
		// {
        //     DateTime Date1 = DateTime.ParseExact(dateFrom, "yyyy-MM-dd",
        //                                 System.Globalization.CultureInfo.InvariantCulture);
		// 	Date1 = DateTime.SpecifyKind(Date1, DateTimeKind.Utc);

        //     DateTime Date2 = DateTime.ParseExact(dateTo, "yyyy-MM-dd",
        //                                 System.Globalization.CultureInfo.InvariantCulture);
		// 	Date2 = DateTime.SpecifyKind(Date2, DateTimeKind.Utc);

		// 	List<API.Coming> comingsAPI = new List<API.Coming>();

		// 	var comingsBL = _facade.GetComingsBetweenDates(Date1, Date2);
        //     foreach (var coming in comingsBL)
        //     {
        //         comingsAPI.Add(ComingConverter.BLToAPI(coming));
        //     }
            
		// 	string jsonString = JsonSerializer.Serialize(comingsAPI, Options.JsonOptions());
		// 	return new ContentResult
		// 	{
		// 		Content = jsonString,
		// 		ContentType = "application/json",
		// 		StatusCode = 200
		// 	};
		// }
    }
}