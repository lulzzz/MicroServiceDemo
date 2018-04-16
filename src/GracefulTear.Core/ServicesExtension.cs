using System;
using System.Collections.Generic;
using System.Text;
using GracefulTear.Core.Services.Client;
using GracefulTear.Core.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace GracefulTear.Core
{
	public class ServicesExtension
	{
		public static void Register(IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
		}
	}
}
