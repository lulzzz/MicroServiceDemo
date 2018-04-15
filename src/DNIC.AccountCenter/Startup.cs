using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using DNIC.AccountCenter.Data;
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
using DNIC.IdentityServer4.Admin.EntityFrameworkCore;
using DNIC.AccountCenter.Models;
using DNIC.IdentityServer4.Admin.Core;

namespace DNIC.AccountCenter
{
	public class Startup
	{
		private readonly IConfiguration configuration;
		private readonly IHostingEnvironment env;

		public Startup(IConfiguration configuration, IHostingEnvironment env)
		{
			this.configuration = configuration;
			this.env = env;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
			var dbContextBuilder = env.IsDevelopment() ?
				new Action<DbContextOptionsBuilder>(options => options.UseSqlite(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly))) : new Action<DbContextOptionsBuilder>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

			services.AddDbContext<ApplicationDbContext>(dbContextBuilder);

			AddIdentity(services);

			AddIdentityServer(dbContextBuilder, services);

			services.AddAccountCenterServices();

			services.AddMemoryCache();

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IConfiguration configuration, IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(configuration.GetSection("Logging"));
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

			DbMigrate(app);
		}

		private void DbMigrate(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
				serviceScope.ServiceProvider.GetService<IRepositorySeedData>().EnsureSeedData();
			}
		}

		private void AddIdentity(IServiceCollection services)
		{
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
		}

		private void AddIdentityServer(Action<DbContextOptionsBuilder> dbContextOptionsBuilder, IServiceCollection services)
		{
			var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

			var identityServerBuilder = services.AddIdentityServer()
				.AddConfigurationStore(options => options.ConfigureDbContext = dbContextOptionsBuilder)
				.AddOperationalStore(options =>
				{
					options.ConfigureDbContext = dbContextOptionsBuilder;
					options.EnableTokenCleanup = true;
					options.TokenCleanupInterval = 30;
				})
				.AddAspNetIdentity<ApplicationUser>()
				.AddAdminEFProvider();

			if (env.IsDevelopment())
			{
				identityServerBuilder.AddDeveloperSigningCredential();
			}
			else
			{
				//RSA：证书长度2048以上，否则抛异常
				//配置AccessToken的加密证书
				var rsa = new RSACryptoServiceProvider();
				//从配置文件获取加密证书
				rsa.ImportCspBlob(Convert.FromBase64String(configuration["SigningCredential"]));
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
		}
	}
}
