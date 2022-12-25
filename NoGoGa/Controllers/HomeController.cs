using Microsoft.AspNetCore.Mvc;
using NoGoGa.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Runtime.ExceptionServices;
using Microsoft.AspNetCore.Authorization;
using BuissnesLayaer;
using DataLayer.Entities;

namespace NoGoGa.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private DataManager dataManager;

		public HomeController(ILogger<HomeController> logger, DataManager dataManager)
		{
			_logger = logger;
			this.dataManager = dataManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public void SaveResult(bool result)
		{
			if (User.Identity.IsAuthenticated)
			{
				dataManager.Games.SaveGame(new()
				{
					UserId = dataManager.Users.GetUserByLogin(User.Claims.Last().Value).Id,
					Date = DateTime.Now,
					Win = result
				});
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Login(VMLogin modelLogin)
		{
			return View("/Views/Access/Login.cshtml");
		}

		public async Task<IActionResult> Logout(VMLogin modelLogin)
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Registration(VMRegistration modelReg)
		{
			return View("/Views/Access/Registration.cshtml");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
