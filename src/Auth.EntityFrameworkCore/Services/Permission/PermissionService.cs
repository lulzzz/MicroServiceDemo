using Auth.Core.Domains.Repositories;
using Auth.Core.Services.Permission;
using Auth.Core.Services.Permission.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Auth.EntityFrameworkCore.Services.Permission
{
	public class PermissionService : IPermissionService
	{
		private readonly RoleManager<Core.Identity.Role> roleManager;

		public PermissionService(RoleManager<Core.Identity.Role> roleManager)
		{
			this.roleManager = roleManager;
		}

		public PaginationQueryResult<PermissionDto> Find(PaginationQuery input)
		{
			var name = input.GetFilter("name");
			PaginationQueryResult<PermissionDto> output;
			if (string.IsNullOrWhiteSpace(name))
			{
				output = roleManager.Roles.PageList<Core.Identity.Role, string, PermissionDto, ICollection<Core.Identity.Role>>(input, r => r.RoleType == Core.Identity.RoleType.Permission, null, false, t => t.ChildRoles);
			}
			else
			{
				output = roleManager.Roles.PageList<Core.Identity.Role, string, PermissionDto, ICollection<Core.Identity.Role>>(input, e => e.Name.ToLower().Contains(name) && e.RoleType == Core.Identity.RoleType.Permission, null, false, t => t.ChildRoles);
			}
			return output;
		}
	}
}
