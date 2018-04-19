using Auth.Core.Domains;
using Auth.Core.Identity;
using Auth.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace Auth.Web.Data
{
	public class AuthDbContext : IdentityDbContext<User, Role, string>
	{
		private readonly IHttpContextAccessor contextAccessor;

		public AuthDbContext(DbContextOptions<AuthDbContext> options, IHttpContextAccessor contextAccessor)
			: base(options)
		{
			this.contextAccessor = contextAccessor;
		}

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

		public override int SaveChanges()
		{
			ApplyConcepts();
			return base.SaveChanges();
		}

		private void ApplyConcepts()
		{
			var value = contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var entries = ChangeTracker.Entries();
			foreach (var entry in entries)
			{
				if (entry.Entity is IAudited e)
				{
					switch (entry.State)
					{
						case EntityState.Added:
							e.CreatorUserId = value;
							e.CreationTime = DateTime.Now;
							break;

						case EntityState.Modified:
							e.LastModifierUserId = value;
							e.LastModificationTime = DateTime.Now;
							break;
					}
				}
			}
		}
	}
}
