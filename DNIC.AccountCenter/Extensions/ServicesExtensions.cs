using DNIC.AccountCenter.Services.Messages;
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
        }
    }
}
