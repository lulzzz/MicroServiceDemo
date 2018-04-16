using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace GracefulTear.Web.Services
{
	public interface IResourceService
	{
		Task AddApiResource(ApiResource apiResource);
		Task<IEnumerable<Client>> GetAll();
	}
}
