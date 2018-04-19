using Auth.Core.Applications;
using Auth.Core.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Services.User
{
	public interface IUserService : IService
	{
		Task<IEnumerable<UserDto>> GetAll();
	}
}
