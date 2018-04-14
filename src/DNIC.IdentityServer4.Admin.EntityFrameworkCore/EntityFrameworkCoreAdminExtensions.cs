using AutoMapper;
using DNIC.IdentityServer4.Admin.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using ID4M = IdentityServer4.Models;
using ID4EFM = IdentityServer4.EntityFramework.Entities;
using DNIC.IdentityServer4.Admin;

namespace DNIC.IdentityServer4.Admin.EntityFrameworkCore
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
