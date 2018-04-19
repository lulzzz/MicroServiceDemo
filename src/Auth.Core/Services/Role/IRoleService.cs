using Auth.Core.Applications;
using Auth.Core.Domains.Repositories;
using Auth.Core.Services.Role.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Services.Role
{
	public interface IRoleService : IService
	{
		Task<IEnumerable<RoleDto>> GetAll();
		PaginationQueryResult<RoleDto> Find(PaginationQuery input);
	}
}
