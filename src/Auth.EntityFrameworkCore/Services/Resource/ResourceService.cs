using AutoMapper;
using Auth.Core;
using Auth.Core.Services.Resource;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.EntityFrameworkCore.Services.Resource
{
	public class ResourceService : IResourceService
	{
		private readonly IConfigurationDbContext _configurationDbContext;

		public ResourceService(IConfigurationDbContext configurationDbContext)
		{
			_configurationDbContext = configurationDbContext;
		}

		public async Task AddApiResource(ApiResource entity)
		{
			await _configurationDbContext.ApiResources.AddAsync(entity.ToEntity());
		}

		public async Task AddIdentityResource(IdentityResource entity)
		{
			await _configurationDbContext.IdentityResources.AddAsync(entity.ToEntity());
		}
	}
}
