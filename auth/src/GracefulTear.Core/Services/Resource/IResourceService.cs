using ISM = IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Models;
using GracefulTear.Core.Applications;

namespace GracefulTear.Core.Services.Resource
{
	public interface IResourceService : IService
	{
		Task AddIdentityResource(IdentityResource entity);
		Task AddApiResource(ApiResource entity);
	}
}
