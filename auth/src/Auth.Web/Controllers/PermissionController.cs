﻿using Auth.Core.Domains.Repositories;
using Auth.Core.Services.Permission;
using Auth.Core.Services.Role;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Web.Controllers
{
	public class PermissionController : Controller
	{
		private readonly IPermissionService permissionService;

		public PermissionController(IPermissionService permissionService)
		{
			this.permissionService = permissionService;
		}

		[HttpGet]
		public IActionResult Index([FromQuery]PaginationQuery input)
		{
			return View(permissionService.Find(input));
		}
	}
}
