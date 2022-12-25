using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NoGoGa.Models;
using BuissnesLayaer.Interfaces;

namespace NoGoGa.Controllers
{
	public class LoginController : Controller
	{
		private IUserRep users;
		public LoginController (IUserRep users)
		{
			this.users = users;
		}

		[HttpPost]
		public async Task<IActionResult> Login(VMLogin modelLogin)
		{
			var user = users.GetUserByLogin(modelLogin.Login);

			if (user != null && user.Password == modelLogin.PassWord)
			{
				List<Claim> claims = new() {
					new Claim(ClaimTypes.NameIdentifier, modelLogin.Login)
				};

				ClaimsIdentity claimsIdentity = new(claims,
					CookieAuthenticationDefaults.AuthenticationScheme);

				AuthenticationProperties properties = new()
				{
					AllowRefresh = true
					//IsPersistent = modelLogin.KeepLoggedIn					
				};

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity), properties);

				return RedirectToAction("Index", "Home");
			}

			ViewData["ValidataMessage"] = "Неверный логин или пароль";
			return View("/Views/Access/Login.cshtml");
		}
	}
}