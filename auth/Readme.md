## MicroServiceDemo��ע�ᡢ��¼����Ȩ����

* .NET CORE SDK >= 2.0
* Visual Studio 2017 Community or JetBrains Rider 2017.3

## ����˵��

* ���� Auth Ϊ������Ŀ
* �� Package Manager Console ��������������

		add-migration Auth -c AuthDbContext
		add-migration Id4Configuration -c ConfigurationDbContext
		add-migration Id4PersistedGrants -c PersistedGrantDbContext

* ִ�гɹ���������

		update-database -Context AuthDbContext
		update-database -Context PersistedGrantDbContext
		update-database -Context ConfigurationDbContext

### Fork From https://github.com/dotnet-china/GracefulTear