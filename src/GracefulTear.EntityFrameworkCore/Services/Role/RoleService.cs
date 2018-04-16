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
using GracefulTear.Core.Domains.Repositories;
using GracefulTear.EntityFrameworkCore;

namespace GracefulTear.EntityFrameworkCore.Services.Role
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<Core.Identity.Role> roleManager;

		public RoleService(RoleManager<Core.Identity.Role> roleManager)
		{
			this.roleManager = roleManager;
		}

		public PaginationQueryResult<RoleDto> Find(PaginationQuery input)
		{
			var name = input.GetFilter("name");
			PaginationQueryResult<RoleDto> output;
			if (string.IsNullOrWhiteSpace(name))
			{
				output = roleManager.Roles.PageList<Core.Identity.Role, string, RoleDto, ICollection<Core.Identity.Role>>(input, null, null, false, t => t.ChildRoles);
			}
			else
			{
				output = roleManager.Roles.PageList<Core.Identity.Role, string, RoleDto, ICollection<Core.Identity.Role>>(input, e => e.Name.ToLower().Contains(name), null, false, t => t.ChildRoles);
				output = roleManager.Roles.PageList<Core.Identity.Role, string, RoleDto, ICollection<Core.Identity.Role>>(input, e => e.Name.ToLower().Contains(name), null, false, t => t.ChildRoles);
			}
			return output;
		}

		public async Task<IEnumerable<RoleDto>> GetAll()
		{
			return await Task.FromResult(Mapper.Map<IEnumerable<Core.Identity.Role>, IEnumerable<RoleDto>>(roleManager.Roles.Include(d => d.ChildRoles)));
		}
	}
}
