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

namespace GracefulTear.EntityFrameworkCore
{
	public static class EntityFrameworkCoreAdminExtensions
	{
		public static IIdentityServerBuilder AddAdminEFProvider(this IIdentityServerBuilder builder)
		{
			CreateMap();

			ServicesExtension.Register(builder.Services);

			builder.Services.AddTransient<IResourceService, ResourceService>();
			builder.Services.AddTransient<IClientService, ClientService>();
			return builder;
		}

		private static void CreateMap()
		{
			Mapper.Initialize(config =>
			{
				config.CreateMap<ID4EFM.Client, ID4M.Client>();
			});
		}
	}
}
