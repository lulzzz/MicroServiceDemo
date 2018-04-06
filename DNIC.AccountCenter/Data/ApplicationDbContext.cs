using DNIC.AccountCenter.Core.Domain.Messages;
using DNIC.AccountCenter.Core.Domain.Users;
using DNIC.AccountCenter.Core.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DNIC.AccountCenter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmailAccount> EmailAccount { get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //#region 使用反射找到domain配置，实现自动生成数据库表

            //var typeFinder = new AppDomainTypeFinder();
            //var typesToRegister = typeFinder.FindClassesOfType(typeof(IEntityTypeConfiguration<>));

            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    builder.ApplyConfiguration(configurationInstance);
            //}
            //#endregion

            base.OnModelCreating(builder);
        }
    }
}
