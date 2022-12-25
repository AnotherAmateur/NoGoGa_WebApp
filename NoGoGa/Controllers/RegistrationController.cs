using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NoGoGa.Models;
using BuissnesLayaer.Interfaces;
using BuissnesLayaer;
using DataLayer.Entities;

namespace NoGoGa.Controllers
{
	public class RegistrationController : Controller
	{
		private DataManager dataManager;
		public RegistrationController(DataManager dataManager)
		{
			this.dataManager = dataManager;
		}

		[HttpPost]
		public async Task<IActionResult> RegMe(VMRegistration modelRegistration)
		{
			var user = dataManager.Users.GetUserByLogin(modelRegistration.Login);

			if (user == null)
			{
				dataManager.Users.SaveUser(new()
				{
					Login = modelRegistration.Login,
					Password = modelRegistration.PassWord,
					Email = modelRegistration.Email,
					FullName = modelRegistration.FullName,
					RegistrationDate = DateTime.Now
				});

				List<Claim> claims = new() {
					new Claim(ClaimTypes.NameIdentifier, modelRegistration.Login)
				};

				ClaimsIdentity claimsIdentity = new(claims,
					CookieAuthenticationDefaults.AuthenticationScheme);

				AuthenticationProperties properties = new()
				{
					AllowRefresh = true
					//IsPersistent = modelRegistration.KeepLoggedIn
				};

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity), properties);

				return RedirectToAction("Index", "Home");
			}

			ViewData["ValidataMessage"] = "Такой пользователь уже существует";
			return RedirectToAction("Index", "Home");
		}
	}
}