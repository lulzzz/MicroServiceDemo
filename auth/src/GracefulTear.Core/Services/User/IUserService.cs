using GracefulTear.Core.Applications;
using GracefulTear.Core.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GracefulTear.Core.Services.User
{
	public interface IUserService : IService
	{
		Task<IEnumerable<UserDto>> GetAll();
	}
}
