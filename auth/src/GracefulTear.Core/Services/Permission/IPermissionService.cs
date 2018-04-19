using GracefulTear.Core.Applications;
using GracefulTear.Core.Domains.Repositories;
using GracefulTear.Core.Services.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GracefulTear.Core.Services.Permission
{
	public interface IPermissionService : IService
	{
		PaginationQueryResult<PermissionDto> Find(PaginationQuery input);
	}
}
