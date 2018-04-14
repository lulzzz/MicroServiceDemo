
using DNIC.AccountCenter.Services;
using DNIC.IdentityServer4.Admin.Core;
using DNIC.IdentityServer4.Admin.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNIC.AccountCenter.Extensions
{
	public static class ServicesExtensions
	{
		public static void AddAccountCenterServices(this IServiceCollection services)
		{
			// Add application services.
			services.AddScoped<IEmailSender, EmailSender>();
			services.AddScoped<IResourceService, ResourceService>();
			services.AddScoped<IRepositorySeedData, RepositorySeedData>();
		}
	}
}
