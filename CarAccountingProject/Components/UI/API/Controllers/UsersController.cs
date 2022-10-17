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
    public class UsersController: Controller
    {
		private BL.Facade _facade;
		private ILogger _logger;

        public UsersController(BL.Facade facade, ILogger<UsersController> logger)
        {
            _facade = facade;
			_logger = logger;
        }

		[Authorize(Roles = "ADMIN, ANALYST")]
		[Produces("application/json")]
		[HttpGet]
		public IActionResult GetUsers()
		{
			_logger.LogInformation("GetUsers method in UsersController");

			var account = HttpContext.User.Identity;
            if (account == null)
            {
                return new ContentResult
				{
					Content = JsonSerializer.Serialize(new ResultDTO() {Title = "ERROR: Not authorized"}, Options.JsonOptions()),
					ContentType = "application/json",
					StatusCode = 200
				};
            }

            List<API.User> usersAPI = new List<API.User>();

			var usersBL = _facade.GetUsers();
            foreach (var user in usersBL)
            {
                usersAPI.Add(UserConverter.BLToAPI(user));
            }
            
			string jsonString = JsonSerializer.Serialize(usersAPI, Options.JsonOptions());
			return new ContentResult
			{
				Content = jsonString,
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		// [Authorize(Roles = "ADMIN")]
		[Authorize]
		[Produces("application/json")]
		[HttpGet("{id}")]
		public IActionResult GetUserById([FromRoute] int id)
		{
			_logger.LogInformation("GetUserById method in UsersController");

			try
			{
				var user = UserConverter.BLToAPI(_facade.GetUserById(id));
				string jsonString = JsonSerializer.Serialize(user, Options.JsonOptions());

				return new ContentResult
				{
					Content = jsonString,
					ContentType = "application/json",
					StatusCode = 200
				};
			}
			catch (Exception)
			{
				return NotFound($"User with id = {id} not found");
			}
		}

		[Authorize(Roles = "ADMIN")]
		[Produces("application/json")]
		[HttpPost]
		public async Task<IActionResult> AddUserAsync([FromBody] API.UserToAdd newUser)
		{
			_logger.LogInformation("AddUserAsync method in UsersController");

			try
			{
				var user = UserToAddConverter.APIToBL(newUser);
				await _facade.AddUserAsync(user);

				return Ok("Successful add");
			}
			catch (Exception)
			{
				return BadRequest($"Such user also exists");
			}
		}

		[Authorize]
		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] API.UserToUpdate newUser)
		{
			_logger.LogInformation("UpdateUserAsync method in UsersController");

			try
			{
				var userIdstring = this.HttpContext.User.Claims
            	.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

				int userId;
				if (!int.TryParse(userIdstring, out userId))
				{
					return Unauthorized("Login, please");
				}
				
				if (id != userId)
				{
					return BadRequest("No access");
				}

				var user = UserToUpdateConverter.APIToBL(newUser);
				await _facade.UpdateUserAsync(id, user);

				return Ok("Successful update");
			}
			catch (Exception)
			{
				return NotFound($"User with id = {id} not found");
			}
		}
    }
}