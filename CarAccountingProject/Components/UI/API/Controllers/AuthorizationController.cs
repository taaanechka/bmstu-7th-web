using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

using BL;
using API.Helpers;
using API.DTO;

namespace API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
    public class AuthorizationController: Controller
    {
        // private Presenter Presenter;

        // public AuthorizationController(Presenter presenter)
        // {
        //     Presenter = presenter;
        // }

		// [HttpPost]
		// [Route("login")]
		// public ActionResult<string> Authorization([FromBody] UserDTO user)
		// {
		// 	var id = HttpContext.Session.GetString("id");

		// 	if (!string.IsNullOrEmpty(id))
		// 	{
		// 		return  JsonSerializer.Serialize(new ResultDTO() {Title = "Authorized!"}, Options.JsonOptions());
		// 	}

        //     try
        //     {
        //         var result = Presenter.LogIn(user.Login, user.Password); // можно сделать тип возврата {код_ошибки, msg}
		// 	    //Console.WriteLine(result.Msg);
        //     }
        //     catch (Exception exc)
        //     {
        //         // https://httpstatuses.com/
        //         this.HttpContext.Response.StatusCode = 418;

        //         if (exc.GetType().IsAssignableFrom(typeof(UserNotFoundException)))
        //         {
        //             return JsonSerializer.Serialize(new ResultDTO() {Title = "User not found"}, Options.JsonOptions());
        //         }
        //         else
        //         {
        //             return JsonSerializer.Serialize(new ResultDTO() {Title = "User validation error"}, Options.JsonOptions());
        //         }
        //     }
			
		// 	var authUser = Presenter.GetUserByLogin(user.Login);
		// 	Console.WriteLine($"{authUser.Id}");
		// 	HttpContext.Session.SetString("id", Convert.ToString(authUser.Id));

		// 	return JsonSerializer.Serialize(new ResultDTO() {Title = "Success authorization"}, Options.JsonOptions());
		// }

		// [HttpGet]
		// [Route("logout")]
		// public ActionResult<string> Logout()
		// {
		// 	var id = HttpContext.Session.GetString("id");
		// 	if (string.IsNullOrEmpty(id))
		// 	{
		// 		return  JsonSerializer.Serialize(new ResultDTO() {Title = "Not authorized"}, Options.JsonOptions());
		// 	}

		// 	Console.WriteLine($"id = {id}");
		// 	HttpContext.Session.Remove("id");
        //     Presenter.LogOut();
		// 	return  JsonSerializer.Serialize(new ResultDTO() {Title = "Success"}, Options.JsonOptions());
		// }

		// [HttpGet]
		// [Route("login")]
		// public ActionResult<string> Authorization()
		// {
		// 	return "Please, login";
		// }


		// [HttpGet]
		// [Route("check")]
		// public IActionResult CheckAuthorization()
		// {
		// 	var id = HttpContext.Session.GetString("id");
		// 	if (string.IsNullOrEmpty(id))
		// 	{
		// 		return new ContentResult { StatusCode = 401 };
		// 	}

		// 	return new ContentResult { StatusCode = 200};
		// }
    }
}