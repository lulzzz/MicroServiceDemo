using GracefulTear.Core.Services.Role.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GracefulTear.Core.Services.Role
{
	public interface IRoleService
	{
		Task<IEnumerable<RoleDto>> GetAll();
	}
}
