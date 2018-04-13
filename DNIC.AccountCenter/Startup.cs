using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using DNIC.AccountCenter.Data;
using DNIC.AccountCenter.Core.Domain.Users;
using DNIC.AccountCenter.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using DNIC.AccountCenter.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DNIC.AccountCenter
{
	public class Startup
	{
		public IHostingEnvironment Environment { get; }
		public IConfiguration Configuration { get; }
		public IServiceProvider ServiceProvider { get; set; }

		public Startup(IConfiguration configuration, IHostingEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

			services.AddIdentity<ApplicationUser, IdentityRole>(config =>
			{
				config.User.RequireUniqueEmail = true;
				config.Password = new PasswordOptions
				{
					RequireDigit = true,
					RequireUppercase = false,
					RequireLowercase = true,
					RequiredLength = 8
				};
				config.SignIn.RequireConfirmedEmail = false;
				config.SignIn.RequireConfirmedPhoneNumber = false;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

			var identityServerBuilder = services.AddIdentityServer()
					.AddConfigurationStore(options =>
						options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly))
					)
					.AddOperationalStore(options =>
					{
						options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
						options.EnableTokenCleanup = true;
						options.TokenCleanupInterval = 30;
					})
					.AddAspNetIdentity<ApplicationUser>();


			if (Environment.IsDevelopment())
			{
				identityServerBuilder.AddDeveloperSigningCredential();
			}
			else
			{
				//RSA：证书长度2048以上，否则抛异常
				//配置AccessToken的加密证书
				var rsa = new RSACryptoServiceProvider();
				//从配置文件获取加密证书
				rsa.ImportCspBlob(Convert.FromBase64String(Configuration["SigningCredential"]));
				identityServerBuilder.AddSigningCredential(new RsaSecurityKey(rsa));
			}

			// 添加外部授权: 后面需要支持QQ, 微信, 微博等主流平台
			services.AddAuthentication()
				//.AddOpenIdConnect("oidc", "OpenID Connect", options =>
				//{
				//	options.Authority = "https://demo.identityserver.io/";
				//	options.ClientId = "implicit";
				//	options.SaveTokens = true;

				//	options.TokenValidationParameters = new TokenValidationParameters
				//	{
				//		NameClaimType = "name",
				//		RoleClaimType = "role"
				//	};
				//})
				;

			// Add application services.
			services.AddMyServices();

			//add cached
			services.AddMemoryCache();

			services.AddMvc();

			ServiceProvider = services.BuildServiceProvider();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
			loggerFactory.AddSerilog();

			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseIdentityServer();

			app.UseMvcWithDefaultRoute();


			//database auto migration
			var dbContext = ServiceProvider.GetService<ApplicationDbContext>();
			dbContext.Database.Migrate();

			InitializeDatabase(app);
		}

		private void InitializeDatabase(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
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
		}
	}
}
