using AutoMapper;
using Auth.Core.Services.Client;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auth.EntityFrameworkCore.Services.Client
{
	public class ClientService : IClientService
	{
		private readonly IConfigurationDbContext _configurationDbContext;

		public ClientService(IConfigurationDbContext configurationDbContext)
		{
			_configurationDbContext = configurationDbContext;
		}

		public async Task AddClient(IdentityServer4.Models.Client entity)
		{
			await _configurationDbContext.Clients.AddAsync(entity.ToEntity());
		}

		public Task<IEnumerable<IdentityServer4.Models.Client>> GetAll()
		{
			return Task.FromResult(Mapper.Map<IEnumerable<IdentityServer4.Models.Client>>(_configurationDbContext.Clients));
		}
	}
}
