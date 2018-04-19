using Auth.Core.Services.User;
using Auth.Core.Services.User.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Web.Controllers.Api
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		private readonly IUserService userService;

		public UserController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IEnumerable<UserDto>> GetAll()
		{
			return await userService.GetAll();
		}
	}
}
