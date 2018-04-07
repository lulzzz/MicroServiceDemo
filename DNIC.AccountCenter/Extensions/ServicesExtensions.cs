using DNIC.AccountCenter.Core.Cache;
using DNIC.AccountCenter.Services.Messages;
using DNIC.AccountCenter.Services.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNIC.AccountCenter.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddMyServices(this IServiceCollection services)
        {
            // Add application services.
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailAccountService, EmailAccountService>();
            services.AddScoped<ICacheManager, MemoryCacheManager>();
            services.AddScoped<ISettingService, SettingService>();
        }
    }
}
