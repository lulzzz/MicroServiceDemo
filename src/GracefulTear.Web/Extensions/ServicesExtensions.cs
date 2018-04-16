using GracefulTear.Core;
using GracefulTear.EntityFrameworkCore;
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
			services.AddScoped<IResourceService, ResourceService>();
			services.AddScoped<IRepositorySeedData, RepositorySeedData>();
		}
	}
}
