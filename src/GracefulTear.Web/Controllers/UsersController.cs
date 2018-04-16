using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GracefulTear.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class UsersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
