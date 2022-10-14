using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using BL;
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

        public ComingsController(BL.Facade facade)
        {
            _facade = facade;
        }

        [Authorize(Roles = "ADMIN, ANALYST")]
		[HttpGet]
		public IActionResult Index()
		{
            List<API.Coming> comingsAPI = new List<API.Coming>();

			var comingsBL = _facade.GetComings();
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
		// public IActionResult AddComing([FromBody] API.Coming newComing, [FromBody] API.Car carC)
		// public IActionResult AddComing([FromBody] API.ComingCar newComingCar)
		public IActionResult AddComing([FromBody] API.Car car)
		{
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
				_facade.AddComing(comingBL, carBL);
				Console.WriteLine("Add\n");

				return Ok("Successful add");
			}
			catch (Exception)
			{
				return BadRequest($"Such coming also exists");
			}
		}

        //findByDate
        [Authorize(Roles = "ADMIN, ANALYST")]
		[HttpGet("FindByDate")]
		public IActionResult GetComingsByDate(string date)
		{
            DateTime Date = DateTime.ParseExact(date, "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
			Date = DateTime.SpecifyKind(Date, DateTimeKind.Utc);

			List<API.Coming> comingsAPI = new List<API.Coming>();

			var comingsBL = _facade.GetComingsByDate(Date);
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

        //findBetweenDates
        [Authorize(Roles = "ADMIN, ANALYST")]
		[HttpGet("FindBetweenDates")]
		public IActionResult GetComingsBetweenDates(string date1, string date2)
		{
            DateTime Date1 = DateTime.ParseExact(date1, "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
			Date1 = DateTime.SpecifyKind(Date1, DateTimeKind.Utc);

            DateTime Date2 = DateTime.ParseExact(date2, "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
			Date2 = DateTime.SpecifyKind(Date2, DateTimeKind.Utc);

			List<API.Coming> comingsAPI = new List<API.Coming>();

			var comingsBL = _facade.GetComingsBetweenDates(Date1, Date2);
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
    }
}