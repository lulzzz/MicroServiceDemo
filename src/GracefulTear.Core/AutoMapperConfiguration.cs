using AutoMapper;
using GracefulTear.Core.Models;
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
			});
		}
	}
}
