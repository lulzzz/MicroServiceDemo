using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Service;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AccountService>();

	        services.AddMvc();

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

	        services.AddAuthentication(options =>
		        {
			        options.DefaultScheme = "Cookies";
			        options.DefaultChallengeScheme = "oidc";
		        })
		        .AddCookie("Cookies")
		        .AddOpenIdConnect("oidc", options =>
		        {
			        options.SignInScheme = "Cookies";

			        options.Authority = Configuration["OAuth:Authority"];
			        options.RequireHttpsMetadata = false;

			        options.ClientId = Configuration["OAuth:ClientId"];
			        options.ClientSecret = Configuration["OAuth:ClientSecret"];
					options.ResponseType = "code id_token";

			        options.SaveTokens = true;
			        options.GetClaimsFromUserInfoEndpoint = true;

			        options.Scope.Add(Configuration["OAuth:Scope"]);
			        options.Scope.Add("offline_access");
		        });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ConfigFile = "build/webpack.dev.conf.js"
                });
            }

			app.Use(async (context, next) =>
			{
				await next();

				if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
				{
					// 解决asp.net core HMR的bug
					context.Request.Path = env.IsDevelopment() ? "/dist/index.html" : "/index.html";

					await next();
				}
			}).UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });

			app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
          //业务系统的Area
          routes.MapRoute(
            name: "default",
            template: "{controller}/{action}",
            defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
