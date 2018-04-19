using GracefulTear.Core.Services.Role;
using GracefulTear.Core.Services.Role.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GracefulTear.Web.Controllers.Api
{
	[Route("api/[controller]")]
	public class RoleController
	{
		private readonly IRoleService roleService;

		public RoleController(IRoleService roleService)
		{
			this.roleService = roleService;
		}

		[HttpGet]
		public async Task<IEnumerable<RoleDto>> GetAll()
		{
			return await roleService.GetAll();
		}
	}
}
