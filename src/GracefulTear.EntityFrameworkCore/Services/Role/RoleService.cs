using AutoMapper;
using GracefulTear.Core.Services.Role;
using GracefulTear.Core.Services.Role.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IDM = IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;

namespace GracefulTear.EntityFrameworkCore.Services.Role
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<Core.Models.Role> roleManager;

		public RoleService(RoleManager<Core.Models.Role> roleManager)
		{
			this.roleManager = roleManager;
		}

		public async Task<IEnumerable<RoleDto>> GetAll()
		{
			return await Task.FromResult(Mapper.Map<IEnumerable<Core.Models.Role>, IEnumerable<RoleDto>>(roleManager.Roles.Include(d => d.ChildRoles)));
		}
	}
}
