using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Auth.Core;
using Auth.Core.Identity;
using Auth.EntityFrameworkCore;
using Auth.Web.Data;
using Auth.Web.Extensions;
using Auth.Web.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Auth.Web
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
			var connectionString = configuration.GetConnectionString("MySqlConnection");

			var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
			var dbContextBuilder = env.IsDevelopment() ?
				new Action<DbContextOptionsBuilder>(options => options.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly))) :
				new Action<DbContextOptionsBuilder>(options => options.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

			services.AddDbContext<AuthDbContext>(dbContextBuilder);

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
		}

		private void AddIdentity(IServiceCollection services)
		{
			services.AddIdentity<User, Role>(config =>
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
			.AddEntityFrameworkStores<AuthDbContext>()
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
				.AddAspNetIdentity<User>()
				.AddAdminEFProvider().AddProfileService<ProfileService>();

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

	    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                var claims = context.Subject.Claims.ToList();

                //set issued claims to return
                context.IssuedClaims = claims.ToList();
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
