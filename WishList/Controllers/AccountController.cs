using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WishList.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View("Register");
		}

		[HttpPost]
		[AllowAnonymous]
		public IActionResult Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
				return View("Register", model);

			ApplicationUser user = new ApplicationUser()
			{
				UserName = model.Email,
				Email = model.Email,
			};

			string userPassword = model.Password;

			var result = _userManager.CreateAsync(user, userPassword);
			if (!result.Result.Succeeded)
			{
				foreach (var error in result.Result.Errors)
				{
					ModelState.AddModelError(userPassword, error.Description);
				}
				return View("Register", model);
			}

			return RedirectToAction("Index", "Home");
		}
	}
}
