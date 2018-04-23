using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
			var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
	        builder.SetBasePath(env.ContentRootPath)
		        .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
		        //add configuration.json
		        .AddJsonFile($"configuration.{env.EnvironmentName}.json")
		        .AddEnvironmentVariables();

	        Configuration = builder.Build();
		}

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot(Configuration);
	        services.AddAuthentication(opt => opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
	        services.AddMvc();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

	        //

			app.Use(async (context, next) =>
	        {
		        await next();

		        //if (context.Request.Path.Value == "/Account/Login2")
		        //{
			       // context.Response.StatusCode = 401;

		        //}

		        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
		        {
			        // 解决asp.net core HMR的bug
			        context.Request.Path = env.IsDevelopment() ? "/dist/index.html" : "/index.html";
			        context.Response.StatusCode = 200;
					await next();
		        }
	        }).UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });

	        app.UseAuthentication();

	        app.UseStaticFiles();

	        app.UseMvc();

			app.UseOcelot().Wait();

        }
    }
}
