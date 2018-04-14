## DNIC的注册、登录与授权服务

* .NET CORE SDK >= 2.0
* Visual Studio 2017 Community or JetBrains Rider 2017.3

## 开发说明

* 设置 DNIC.AccountCenter 为启动项目
* 在 Package Manager Console 中运行如下命令

		add-migration InitApplicationDbMigration -c ApplicationDbContext
		add-migration InitID4PersistedGrantDbMigration -c PersistedGrantDbContext
		add-migration InitID4ConfigurationDbMigration -c ConfigurationDbContext

* 执行成功后再运行

		update-database -Context ApplicationDbContext
		update-database -Context PersistedGrantDbContext
		update-database -Context ConfigurationDbContext