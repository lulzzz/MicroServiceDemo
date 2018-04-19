using Auth.Core;
using Auth.Core.Dependency;
using Auth.Core.Services;
using Auth.Core.Services.Resource;
using Auth.EntityFrameworkCore;
using Auth.EntityFrameworkCore.Services.Resource;
using Auth.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auth.Web.Extensions
{
	public static class ServicesExtensions
	{
		public static void AddAccountCenterServices(this IServiceCollection services)
		{
			// Add application services.
			services.AddScoped<IEmailSender, EmailSender>();
			services.AddScoped<ISeedData, SeedData>();
		}
	}
}
