﻿using System;
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
				var role1 = new Core.Identity.Role
				{
					Name = "role1",
					RoleType = Core.Identity.RoleType.Role
				};
				dbContext.Roles.Add(role1);
				var role2 = new Core.Identity.Role
				{
					Name = "role2",
					RoleType = Core.Identity.RoleType.Role
				};
				role2.ChildRoles = new Core.Identity.Role[] { role1 };
				dbContext.Roles.Add(role2);

				for (int i = 5; i < 100; ++i)
				{
					var role = new Core.Identity.Role
					{
						Name = "role" + i,
						RoleType = Core.Identity.RoleType.Role
					};
					dbContext.Roles.Add(role);
				}

				for (int i = 105; i < 200; ++i)
				{
					var role = new Core.Identity.Role
					{
						Name = "permission" + i,
						RoleType = Core.Identity.RoleType.Permission
					};
					dbContext.Roles.Add(role);
				}

				dbContext.SaveChanges();
			}
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}
