using AutoMapper;
using Auth.Core.Identity;
using Auth.Core.Services.Role.Dto;
using Auth.Core.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Core
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
