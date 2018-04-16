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
using GracefulTear.Core.Domains;
using Microsoft.AspNetCore.Identity;
using GracefulTear.EntityFrameworkCore.Services.Role;
using GracefulTear.Core.Services.Role;
using GracefulTear.EntityFrameworkCore.Services.User;
using GracefulTear.Core.Services.User;
using Microsoft.EntityFrameworkCore;
using GracefulTear.Core.Domains.Repositories;
using System.Linq.Expressions;
using System.Linq;
using GracefulTear.Core.Applications.Dtos;
using System.Collections.Generic;

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

		public static PaginationQueryResult<TDto> PageList<TEntity, TKey, TDto>(this IQueryable<TEntity> dbSet, PaginationQuery input,
			Expression<Func<TEntity, bool>> where = null,
			Expression<Func<TEntity, TKey>> orderBy = null, bool orderByDesc = false) where TEntity : class, IEntity<TKey> where TDto : IDto
		{
			input.Validate();
			PaginationQueryResult<TDto> output = new PaginationQueryResult<TDto>();
			IQueryable<TEntity> entities = dbSet.AsQueryable();
			if (where != null)
			{
				entities = entities.Where(where);
			}

			output.TotalCount = entities.Count();

			if (orderBy == null)
			{
				if (orderByDesc)
				{
					entities = entities.OrderByDescending(e => e.Id).Skip((input.Page - 1) * input.PageSize).Take(input.PageSize);
				}
				else
				{
					entities = entities.Skip((input.Page - 1) * input.PageSize).Take(input.PageSize);
				}
			}
			else
			{
				if (orderByDesc)
				{
					entities = entities.OrderByDescending(orderBy).Skip((input.Page - 1) * input.PageSize).Take(input.PageSize);
				}
				else
				{
					entities = entities.OrderBy(orderBy).Skip((input.Page - 1) * input.PageSize).Take(input.PageSize);
				}
			}

			output.Page = input.Page;
			output.PageSize = input.PageSize;
			output.Data = (IEnumerable<TDto>)Mapper.Map(entities, typeof(IEnumerable<TEntity>), typeof(IEnumerable<TDto>));
			return output;
		}

		private static void CreateMap()
		{
			AutoMapperConfiguration.CreateMap();
			Mapper.Instance.ConfigurationProvider.BuildExecutionPlan(typeof(ID4EFM.Client), typeof(ID4M.Client));
		}
	}
}
