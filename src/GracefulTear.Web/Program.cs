using System;
using System.Linq;
using GracefulTear.Core;
using GracefulTear.Core.Services;
using GracefulTear.Web.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace GracefulTear.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.MinimumLevel.Override("System", LogEventLevel.Warning)
				.MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
				.Enrich.FromLogContext()
				.WriteTo.File(@"DNIC.AccountCenter.log")
				.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
				.CreateLogger();
			var host = BuildWebHost(args);
			Migrate(host);
			host.Run();
		}

		public static void Migrate(IWebHost app)
		{
			using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var dbContext = serviceScope.ServiceProvider.GetService<GracefulTearDbContext>();
				dbContext.Database.Migrate();

				AddSeedData(dbContext);

				serviceScope.ServiceProvider.GetService<ISeedData>().EnsureSeedData();
			}
		}

		private static void AddSeedData(GracefulTearDbContext dbContext)
		{
			if (!dbContext.Roles.Any())
			{
				var role1 = new Core.Models.Role
				{
					Name = "role1"
				};
				dbContext.Roles.Add(role1);
				var role2 = new Core.Models.Role
				{
					Name = "role2"
				};
				role2.ChildRoles = new Core.Models.Role[] { role1 };
				dbContext.Roles.Add(role2);

				dbContext.SaveChanges();
			}
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}
