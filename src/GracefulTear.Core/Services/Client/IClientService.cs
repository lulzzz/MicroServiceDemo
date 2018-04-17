using GracefulTear.Core.Applications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ISM = IdentityServer4.Models;

namespace GracefulTear.Core.Services.Client
{
	public interface IClientService : IService
	{
		Task<IEnumerable<ISM.Client>> GetAll();
		Task AddClient(ISM.Client entity);
	}
}
