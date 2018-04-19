using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Core.Domains.Repositories
{
	public class PaginationQuery : FilterQuery
	{
		public virtual int Page { get; set; }
		public virtual int PageSize { get; set; }
		public virtual string Sort { get; set; }

		public void Validate()
		{
			if (Page <= 0)
			{
				Page = 1;
			}

			if (PageSize > 60)
			{
				PageSize = 60;
			}

			if (PageSize <= 0)
			{
				PageSize = 40;
			}
		}
	}
}
