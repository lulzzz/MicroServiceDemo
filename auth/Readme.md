## DNIC��ע�ᡢ��¼����Ȩ����

* .NET CORE SDK >= 2.0
* Visual Studio 2017 Community or JetBrains Rider 2017.3

## ����˵��

* ���� DNIC.AccountCenter Ϊ������Ŀ
* �� Package Manager Console ��������������

		add-migration GracefulTear -c GracefulTearDbContext
		add-migration Id4Configuration -c ConfigurationDbContext
		add-migration Id4PersistedGrants -c PersistedGrantDbContext

* ִ�гɹ���������

		update-database -Context GracefulTearDbContext
		update-database -Context PersistedGrantDbMigration
		update-database -Context ConfigurationDbMigration