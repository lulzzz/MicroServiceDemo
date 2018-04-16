using AutoMapper;
using GracefulTear.Core.Identity;
using GracefulTear.Core.Services.User;
using GracefulTear.Core.Services.User.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GracefulTear.EntityFrameworkCore.Services.User
{
	public class UserService : IUserService
	{
		private readonly UserManager<Core.Identity.User> userManager;

		public UserService(UserManager<Core.Identity.User> userManager)
		{
			this.userManager = userManager;
		}

		public async Task<IEnumerable<UserDto>> GetAll()
		{
			return await Task.FromResult(Mapper.Map<IEnumerable<Core.Identity.User>, IEnumerable<UserDto>>(userManager.Users));
		}
	}
}
