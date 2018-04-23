using Auth.Core;
using Auth.Core.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auth.EntityFrameworkCore
{
	public class SeedData : ISeedData
	{
		private readonly IServiceProvider serviceProvider;

		public SeedData(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public void EnsureSeedData()
		{
			Console.WriteLine("Seeding database...");
			var connectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("MySqlConnection");

			using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

				var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
				context.Database.Migrate();
				if (!context.Clients.Any())
				{
					foreach (var client in Config.GetClients())
					{
						context.Clients.Add(client.ToEntity());
					}
					context.SaveChanges();
				}

				if (!context.IdentityResources.Any())
				{
					foreach (var resource in Config.GetIdentityResources())
					{
						context.IdentityResources.Add(resource.ToEntity());
					}
					context.SaveChanges();
				}

				if (!context.ApiResources.Any())
				{
					foreach (var resource in Config.GetApiResources())
					{
						context.ApiResources.Add(resource.ToEntity());
					}
					context.SaveChanges();
				}
			}

			Console.WriteLine("Done seeding database.");
			Console.WriteLine();
		}
	}
}
