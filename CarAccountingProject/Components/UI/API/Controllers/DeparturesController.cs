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
    public class DeparturesController: Controller
    {
        private BL.Facade _facade;

        public DeparturesController(BL.Facade facade)
        {
            _facade = facade;
        }

        [Authorize(Roles = "ADMIN, ANALYST")]
		[HttpGet]
		public IActionResult Index()
		{
            List<API.Departure> departuresAPI = new List<API.Departure>();

			var departuresBL = _facade.GetDepartures();
            foreach (var departure in departuresBL)
            {
                departuresAPI.Add(DepartureConverter.BLToAPI(departure));
            }
            
			string jsonString = JsonSerializer.Serialize(departuresAPI, Options.JsonOptions());
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
		public IActionResult GetDepartureById([FromRoute] int id)
		{
			try
			{
				var departure = DepartureConverter.BLToAPI(_facade.GetDepartureById(id));
				string jsonString = JsonSerializer.Serialize(departure, Options.JsonOptions());

				return new ContentResult
				{
					Content = jsonString,
					ContentType = "application/json",
					StatusCode = 200
				};
			}
			catch (Exception)
			{
				return NotFound($"Departure with id = {id} not found");
			}
		}

        //addComing
        [Authorize(Roles = "EMPLOYEE")]
		[HttpPost]
		public IActionResult AddDeparture([FromBody] API.LinkOwnerCarDeparture link)
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

				var departure = new Departure{UserId = userId};
				var departureBL = DepartureConverter.APIToBL(departure);
                var linkBL = LinkOwnerCarDepartureConverter.APIToBL(link);
				_facade.AddDeparture(departureBL, linkBL);

				return Ok("Successful add");
			}
			catch (Exception)
			{
				return BadRequest($"Such Departure also exists");
			}
		}

        //findByDate
        [Authorize(Roles = "ADMIN, ANALYST")]
		[HttpGet("FindByDate")]
		public IActionResult GetDeparturesByDate(string date)
		{
            DateTime Date = DateTime.ParseExact(date, "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
            Date = DateTime.SpecifyKind(Date, DateTimeKind.Utc);

			List<API.Departure> departuresAPI = new List<API.Departure>();

			var departuresBL = _facade.GetDeparturesByDate(Date);
            foreach (var departure in departuresBL)
            {
                departuresAPI.Add(DepartureConverter.BLToAPI(departure));
            }
            
			string jsonString = JsonSerializer.Serialize(departuresAPI, Options.JsonOptions());
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
		public IActionResult GetDeparturesBetweenDates(string date1, string date2)
		{
            DateTime Date1 = DateTime.ParseExact(date1, "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
            Date1 = DateTime.SpecifyKind(Date1, DateTimeKind.Utc);

            DateTime Date2 = DateTime.ParseExact(date2, "yyyy-MM-dd",
                                        System.Globalization.CultureInfo.InvariantCulture);
            Date2 = DateTime.SpecifyKind(Date2, DateTimeKind.Utc);

			List<API.Departure> departuresAPI = new List<API.Departure>();

			var departuresBL = _facade.GetDeparturesBetweenDates(Date1, Date2);
            foreach (var departure in departuresBL)
            {
                departuresAPI.Add(DepartureConverter.BLToAPI(departure));
            }
            
			string jsonString = JsonSerializer.Serialize(departuresAPI, Options.JsonOptions());
			return new ContentResult
			{
				Content = jsonString,
				ContentType = "application/json",
				StatusCode = 200
			};
		}
    }
}