using GracefulTear.Core.Services.User;
using GracefulTear.Core.Services.User.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GracefulTear.Web.Controllers.Api
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly IUserService userService;

		public UsersController(IUserService userService)
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
