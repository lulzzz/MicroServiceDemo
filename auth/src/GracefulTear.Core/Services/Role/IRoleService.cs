using GracefulTear.Core.Applications;
using GracefulTear.Core.Domains.Repositories;
using GracefulTear.Core.Services.Role.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GracefulTear.Core.Services.Role
{
	public interface IRoleService : IService
	{
		Task<IEnumerable<RoleDto>> GetAll();
		PaginationQueryResult<RoleDto> Find(PaginationQuery input);
	}
}
