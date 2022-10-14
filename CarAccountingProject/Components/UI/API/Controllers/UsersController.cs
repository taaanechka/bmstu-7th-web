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
    public class UsersController: Controller
    {
		private BL.Facade _facade;

        public UsersController(BL.Facade facade)
        {
            _facade = facade;
        }

		[Authorize(Roles = "ADMIN, ANALYST")]
		[Produces("application/json")]
		[HttpGet]
		public IActionResult Index()
		{
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
		public IActionResult AddUser([FromBody] API.UserToAdd newUser)
		{
			try
			{
				var user = UserToAddConverter.APIToBL(newUser);
				_facade.AddUser(user);

				return Ok("Successful add");
			}
			catch (Exception)
			{
				return BadRequest($"Such user also exists");
			}
		}

		[Authorize]
		[HttpPatch("{id}")]
		public IActionResult UpdateUser(int id, [FromBody] API.UserToUpdate newUser)
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
				
				if (id != userId)
				{
					return BadRequest("No access");
				}

				var user = UserToUpdateConverter.APIToBL(newUser);
				_facade.UpdateUser(id, user);

				return Ok("Successful update");
			}
			catch (Exception)
			{
				return NotFound($"User with id = {id} not found");
			}
		}

		// [Authorize(Roles = "ADMIN")]
		// [Produces("application/json")]
		// [HttpPatch("{id}/Block")]
		// public IActionResult BlockUserById([FromRoute] int id)
		// {
		// 	try
		// 	{
		// 		_facade.BlockUser(id);

		// 		return Ok("Successful block");
		// 	}
		// 	catch (Exception)
		// 	{
		// 		return NotFound($"User with id = {id} not found");
		// 	}
		// }
    }
}