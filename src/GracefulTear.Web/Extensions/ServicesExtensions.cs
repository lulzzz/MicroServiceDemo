using GracefulTear.Core;
using GracefulTear.Core.Services;
using GracefulTear.Core.Services.Resource;
using GracefulTear.EntityFrameworkCore;
using GracefulTear.EntityFrameworkCore.Services.Resource;
using GracefulTear.Web.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GracefulTear.Web.Extensions
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
