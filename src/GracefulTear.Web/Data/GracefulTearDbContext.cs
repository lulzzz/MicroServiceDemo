using GracefulTear.Core.Models;
using GracefulTear.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GracefulTear.Web.Data
{
	public class GracefulTearDbContext : IdentityDbContext<User, Role, string>
	{
		public GracefulTearDbContext(DbContextOptions<GracefulTearDbContext> options)
			: base(options)
		{
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
	}
}
