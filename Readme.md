## DNIC的注册、登录与授权服务

* .NET CORE SDK >= 2.0
* Visual Studio 2017 Community or JetBrains Rider 2017.3

## 构建包含注册、登录并且拥有IdentityServer授权功能的ASP.NET CORE项目

* 创建一个 Asp.net core 项目, 选择用户授权模式
* 在 Nuget 管理器中添加: IdentityServer4.EntityFramework, IdentityServer4, IdentityServer4.AspNetIdentity 三个包
* 修改 public void ConfigureServices(IServiceCollection services) 方法中的内容如下：

		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddIdentityServer()
				.AddDeveloperSigningCredential()
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


			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddMvc();
		}
 注意AddIdentityServer一定要在AddIdentity后面

* 打开项目所在文件夹, 运行如下EF迁移命令

		dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
		dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb

* 在Nuget Package Manage Console中运行

		update-database -Context ApplicationDbContext
		update-database -Context PersistedGrantDbContext
		update-database -Context ConfigurationDbContext

* 项目构建完成, 如果有需要, 你可以自己再添加SeedData文件添加初始数据