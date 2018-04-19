using Auth.Core.Applications.Dtos;
using Auth.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Core.Domains.Repositories
{
	public class PaginationQueryResult<TEntity> where TEntity : IDto
	{
		public virtual long TotalCount { get; set; }
		public virtual int Page { get; set; }
		public virtual int PageSize { get; set; }

		public IEnumerable<TEntity> Data { get; set; }
	}
}
