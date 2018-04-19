using Auth.Core.Applications;
using Auth.Core.Domains.Repositories;
using Auth.Core.Services.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Core.Services.Permission
{
	public interface IPermissionService : IService
	{
		PaginationQueryResult<PermissionDto> Find(PaginationQuery input);
	}
}
