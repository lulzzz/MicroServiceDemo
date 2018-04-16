using AutoMapper;
using GracefulTear.Core.Identity;
using GracefulTear.Core.Services.Role.Dto;
using GracefulTear.Core.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GracefulTear.Core
{
	public class AutoMapperConfiguration
	{
		public static void CreateMap()
		{
			Mapper.Initialize(config =>
			{
				config.CreateMap<User, UserDto>();
				config.CreateMap<Role, RoleDto>();
			});
		}
	}
}
