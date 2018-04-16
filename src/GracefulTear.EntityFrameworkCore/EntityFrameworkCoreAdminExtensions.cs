using AutoMapper;
using GracefulTear.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using ID4M = IdentityServer4.Models;
using ID4EFM = IdentityServer4.EntityFramework.Entities;

namespace GracefulTear.EntityFrameworkCore
{
	public static class EntityFrameworkCoreAdminExtensions
	{
		public static IIdentityServerBuilder AddAdminEFProvider(this IIdentityServerBuilder builder)
		{
			Mapper.Initialize(config =>
			{
				config.CreateMap<ID4EFM.Client, ID4M.Client>();
			});
			builder.Services.AddTransient<IResourceDbContext, ResourceDbContext>();
			return builder;
		}
	}
}
