using AutoMapper;
using GracefulTear.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using ID4M = IdentityServer4.Models;
using ID4EFM = IdentityServer4.EntityFramework.Entities;
using GracefulTear.Core.Services.Resource;
using GracefulTear.EntityFrameworkCore.Services.Resource;
using GracefulTear.Core.Services.Client;
using GracefulTear.EntityFrameworkCore.Services.Client;
using GracefulTear.Core.Models;
using Microsoft.AspNetCore.Identity;
using GracefulTear.EntityFrameworkCore.Services.Role;
using GracefulTear.Core.Services.Role;
using GracefulTear.EntityFrameworkCore.Services.User;
using GracefulTear.Core.Services.User;

namespace GracefulTear.EntityFrameworkCore
{
	public static class EntityFrameworkCoreAdminExtensions
	{
		public static IIdentityServerBuilder AddAdminEFProvider(this IIdentityServerBuilder builder)
		{
			CreateMap();

			builder.Services.AddTransient<IResourceService, ResourceService>();
			builder.Services.AddTransient<IClientService, ClientService>();
			builder.Services.AddTransient<IUserService, UserService>();
			builder.Services.AddTransient<IRoleService, RoleService>();
			return builder;
		}

		private static void CreateMap()
		{
			AutoMapperConfiguration.CreateMap();
			Mapper.Instance.ConfigurationProvider.BuildExecutionPlan(typeof(ID4EFM.Client), typeof(ID4M.Client));
		}
	}
}
