using System.Collections.Generic;
using System.Threading.Tasks;
using GracefulTear.Core;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace GracefulTear.Web.Services
{
	public class ResourceService : IResourceService
	{
		private readonly IResourceStore _resourceStore;
		private readonly IResourceDbContext _resourceDbContext;

		public ResourceService(IResourceStore resourceStore, IResourceDbContext resourceDbContext)
		{
			_resourceStore = resourceStore;
			_resourceDbContext = resourceDbContext;
		}

		public async Task AddApiResource(ApiResource apiResource)
		{
			await _resourceDbContext.AddApiResource(apiResource);
		}

		public async Task<IEnumerable<Client>> GetAll()
		{
			return await _resourceDbContext.GetAll();
		}
	}
}
