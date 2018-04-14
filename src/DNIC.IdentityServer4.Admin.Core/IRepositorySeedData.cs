using System;
using System.Collections.Generic;
using System.Text;

namespace DNIC.IdentityServer4.Admin.Core
{
	public interface IRepositorySeedData
	{
		void EnsureSeedData();
	}
}
